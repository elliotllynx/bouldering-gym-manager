using BoulderSetManager.Models.Entities;
using BoulderSetManager.Models.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using DAL.Enums;
using SkiaSharp;
using Microcharts;

namespace BoulderSetManager.ViewModels
{
    [QueryProperty(nameof(GymId), "gymId")]
    public partial class MainViewModel : ObservableObject
    {
        private readonly WallService _wallService = new();
        private readonly BoulderProblemService _boulderService = new();
        public List<BoulderStyle?> Styles { get; } = [null,  BoulderStyle.Overhang, BoulderStyle.Slab, BoulderStyle.Vertical];

        // page load: 
        [ObservableProperty] public partial int GymId { get; set; }
        [ObservableProperty] public partial GymDTO SelectedGym { get; set; }
        [ObservableProperty] public partial ObservableCollection<WallDTO> Walls { get; set; } = new();
        // because picker doesnt change for collection edits + for filtering its needed to have list of all walls stored somewhere
        [ObservableProperty] public partial ObservableCollection<WallDTO> AllWalls { get; set; } = new();

        private readonly System.Text.RegularExpressions.Regex _gradeRegex = new(@"^([4-9]|10)([A-C])(\+?)$");

        async partial void OnGymIdChanged(int value)
        {
            var gymService = new GymService();
            SelectedGym = await gymService.GetGym(value);
            await LoadWalls(value);
            UpdateDynamicProperties();
        }

        private async Task LoadWalls(int gymId)
        {
            Walls = new ObservableCollection<WallDTO>(await _wallService.GetGymWalls(gymId));
            AllWalls = new ObservableCollection<WallDTO>(Walls);
        }

        [RelayCommand]
        private async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        // filtering management:
        [ObservableProperty] public partial ObservableCollection<object> FilterWalls { get; set; } = new();
        [ObservableProperty] public partial string FilterGrade { get; set; } = string.Empty;
        [ObservableProperty] public partial BoulderStyle? FilterStyle { get; set; } = null;
        [ObservableProperty] public partial string FilterAuthor { get; set; } = string.Empty;
        [ObservableProperty] public partial string FilterRetireInDays { get; set; } = string.Empty;
        [ObservableProperty] public partial bool HasFilterError { get; set; } = false;
        [ObservableProperty] public partial string FilterErrorMessage { get; set; } = string.Empty;

        [RelayCommand]
        private async Task ApplyFilter()
        {
            HasFilterError = false;
            if (string.IsNullOrWhiteSpace(FilterGrade)
                && string.IsNullOrWhiteSpace(FilterAuthor)
                && string.IsNullOrWhiteSpace(FilterRetireInDays)
                && FilterWalls.Count == 0
                && FilterStyle is null)
            {
                HasFilterError = true;
                FilterErrorMessage = "Please input filtering constrain.";
                return;
            }

            if (!string.IsNullOrWhiteSpace(FilterGrade) && !IsValidGrade(FilterGrade))
            {
                HasFilterError = true;
                FilterErrorMessage = "Please input valid grade.";
                return;
            }

            int? retireDays = null;
            if (!string.IsNullOrWhiteSpace(FilterRetireInDays))
            {
                if (int.TryParse(FilterRetireInDays, out int parsed))
                    retireDays = parsed;
                else
                {
                    HasFilterError = true;
                    FilterErrorMessage = "Please input a valid number for days left.";
                    return;
                }
            }

            await LoadWalls(GymId);
            if (FilterWalls.Count > 0)
            {
                var filterIds = FilterWalls.Cast<WallDTO>().Select(fw => fw.Id).ToHashSet();
                Walls = new ObservableCollection<WallDTO>(Walls.Where(w => filterIds.Contains(w.Id)));
            }
            foreach (var wall in Walls)
            {
                wall.Boulders = new ObservableCollection<BoulderProblemDTO>(
                    wall.Boulders.Where(b =>
                        (string.IsNullOrEmpty(FilterGrade) || b.Grade == FilterGrade) &&
                        (FilterStyle is null || b.Style == FilterStyle) &&
                        (string.IsNullOrEmpty(FilterAuthor) || b.Author == FilterAuthor) &&
                        (retireDays is null || b.DaysLeft <= retireDays)
                    )
                );
            }
            UpdateDynamicProperties();
        }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPopUpVisible))]
        public partial bool IsWallListExpanded { get; set; } = false;

        [RelayCommand]
        private void ToggleWallList()
        {
            IsWallListExpanded = !IsWallListExpanded;
            OnPropertyChanged(nameof(SelectedWallsSummary));
        }
        public string SelectedWallsSummary =>
            FilterWalls.Count == 0 ? "Select walls" :
            FilterWalls.Count == 1 ? "1 wall selected" :
            $"{FilterWalls.Count} walls selected";

        [RelayCommand]
        private async Task ResetFilter()
        {
            HasFilterError = false;
            FilterWalls = new();
            FilterGrade = string.Empty;
            FilterStyle = null;
            FilterAuthor = string.Empty;
            FilterRetireInDays = string.Empty;
            await LoadWalls(GymId);
            UpdateDynamicProperties();
        }

        [RelayCommand]
        private async Task FilterRetiring()
        {
            FilterRetireInDays = "3";
            await ApplyFilter();
        }

        // dynamic displayed properties management:
        private void UpdateDynamicProperties()
        {
            OnPropertyChanged(nameof(AverageGrade));
            UpdateStyleChart();
        }

        public string AverageGrade
        {
            get
            {
                var allBoulders = Walls.SelectMany(w => w.Boulders).ToList();
                if (!allBoulders.Any()) return "N/A";

                var validGrades = allBoulders
                    .Select(b => ParseGrade(b.Grade))
                    .Where(g => g > 0)
                    .ToList();

                if (!validGrades.Any()) return "N/A";

                return FormatGrade((int)Math.Round(validGrades.Average()));
            }
        }

        private int ParseGrade(string grade)
        {
            if (string.IsNullOrWhiteSpace(grade)) return 0;
            var match = _gradeRegex.Match(grade);
            if (!match.Success) return 0;

            int number = int.Parse(match.Groups[1].Value);
            int letter = match.Groups[2].Value[0] - 'A';
            int plus = match.Groups[3].Value == "+" ? 1 : 0;

            return (number - 4) * 6 + letter * 2 + plus; // numeric score
        }

        private string FormatGrade(int score)
        {
            int number = score / 6 + 4;
            int remainder = score % 6;
            char letter = (char)('A' + remainder / 2);
            string plus = remainder % 2 == 1 ? "+" : "";
            return $"{number}{letter}{plus}";
        }

        [ObservableProperty] public partial PieChart? StyleChart { get; set; }
        private void UpdateStyleChart()
        {
            var data = Walls.SelectMany(w => w.Boulders).GroupBy(b => b.Style);
            var entries = new List<ChartEntry>();

            foreach (var group in data)
            {
                entries.Add(new ChartEntry(group.Count())
                {
                    Color = SKColor.Parse(GetColorForStyle(group.Key))
                });
            }

            if (!entries.Any())
            {
                StyleChart = null;
                return;
            }

            StyleChart = new PieChart
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent,
                LabelMode = LabelMode.None
            };
        }
        private string GetColorForStyle(BoulderStyle style) => style switch
        {
            BoulderStyle.Slab => "#00FFFF",     // Cyan
            BoulderStyle.Vertical => "#FF00FF", // Magenta
            BoulderStyle.Overhang => "#FFFF00", // Yellow
            _ => "#000000"           // Black
        };


        // wall CRUD management:
        public bool IsPopUpVisible => IsAddWallVisible || IsEditWallVisible || IsAddBoulderVisible || IsEditBoulderVisible || IsWallListExpanded;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPopUpVisible))]
        public partial bool IsAddWallVisible { get; set; } = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPopUpVisible))] 
        public partial bool IsEditWallVisible { get; set; } = false;

        [ObservableProperty] public partial string NewWallName { get; set; } = string.Empty;
        [ObservableProperty] public partial WallDTO? SelectedWall { get; set; } = null;
        [ObservableProperty] public partial bool HasWallInputError { get; set; } = false;
        [ObservableProperty] public partial string WallInputErrorMessage { get; set; } = string.Empty;

        [RelayCommand]
        private void ShowAddWall() => IsAddWallVisible = true;

        [RelayCommand]
        private void ShowEditWall(WallDTO wall)
        {
            SelectedWall = wall;
            IsEditWallVisible = true;
        }

        [RelayCommand]
        private void HideWallForm()
        {
            IsAddWallVisible = false;
            IsEditWallVisible = false;
            NewWallName = string.Empty;
            HasWallInputError = false;
            WallInputErrorMessage = string.Empty;
        }

        [RelayCommand]
        private async Task AddWall()
        {
            if (string.IsNullOrWhiteSpace(NewWallName))
            {
                HasWallInputError = true;
                WallInputErrorMessage = "Please input new name.";
                return;
            }

            var wall = new WallDTO()
            {
                Name = NewWallName,
                GymId = GymId
            };
            wall.Id = await _wallService.CreateWall(wall);
            Walls.Add(wall);
            AllWalls.Add(wall);
            HideWallForm();
        }

        [RelayCommand]
        private async Task EditWall()
        {
            if (string.IsNullOrWhiteSpace(NewWallName))
            {
                HasWallInputError = true;
                WallInputErrorMessage = "Please input new name.";
                return;
            }

            SelectedWall.Name = NewWallName;
            await _wallService.UpdateWall(SelectedWall.Id, NewWallName);
            var index = AllWalls.IndexOf(SelectedWall);
            AllWalls[index] = SelectedWall;
            HideWallForm();
        }

        [RelayCommand]
        private async Task DeleteWall(WallDTO wall)
        {
            await _wallService.DeleteWall(wall.Id);
            Walls.Remove(wall);
            AllWalls.Remove(wall);
        }

        // boulder CRUD management

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPopUpVisible))]
        public partial bool IsAddBoulderVisible { get; set; } = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPopUpVisible))]
        public partial bool IsEditBoulderVisible { get; set; } = false;

        [ObservableProperty] public partial BoulderProblemDTO SelectedBoulder { get; set; }
        [ObservableProperty] public partial string NewGrade { get; set; } = string.Empty;
        [ObservableProperty] public partial BoulderStyle? NewStyle { get; set; } = null;
        [ObservableProperty] public partial string NewAuthor { get; set; } = string.Empty;
        [ObservableProperty] public partial DateTime NewBuiltDate { get; set; } = DateTime.Today;
        [ObservableProperty] public partial DateTime NewRetireDate { get; set; } = DateTime.Today.AddMonths(1);
        [ObservableProperty] public partial bool HasBoulderInputError { get; set; } = false;
        [ObservableProperty] public partial string BoulderInputErrorMessage { get; set; } = string.Empty;

        [RelayCommand]
        private void ShowAddBoulder(WallDTO wall)
        {
            NewBuiltDate = DateTime.Today;
            NewRetireDate = DateTime.Today.AddMonths(1);
            SelectedWall = wall;
            IsAddBoulderVisible = true;
        }

        [RelayCommand]
        private void ShowEditBoulder(BoulderProblemDTO boulder)
        {
            NewBuiltDate = boulder.BuiltDate;
            NewRetireDate = boulder.RetireDate;
            NewStyle = boulder.Style;
            SelectedBoulder = boulder;
            IsEditBoulderVisible = true;
        }

        [RelayCommand]
        private void HideBoulderForm()
        {
            IsAddBoulderVisible = false;
            IsEditBoulderVisible = false;
            NewGrade = string.Empty;
            NewStyle = null;
            NewAuthor = string.Empty;
            HasBoulderInputError = false;
            BoulderInputErrorMessage = string.Empty;
        }

        [RelayCommand]
        private async Task AddBoulder()
        {
            if (string.IsNullOrWhiteSpace(NewGrade)
                || NewStyle is null
                || string.IsNullOrWhiteSpace(NewAuthor)  )
            {
                HasBoulderInputError = true;
                BoulderInputErrorMessage = "All attributes must be filled in.";
                return;
            }
            if (!string.IsNullOrWhiteSpace(NewGrade) && !IsValidGrade(NewGrade))
            {
                HasBoulderInputError = true;
                BoulderInputErrorMessage = "Grade must be in format e.g. 6A or 8B+";
                return;
            }
            if (NewBuiltDate >= NewRetireDate)
            {
                HasBoulderInputError = true;
                BoulderInputErrorMessage = "Built date cannot precede retire date.";
                return;
            }
            var boulder = new BoulderProblemDTO
            {
                Grade = NewGrade,
                Style = NewStyle.Value,
                Author = NewAuthor,
                BuiltDate = NewBuiltDate,
                RetireDate = NewRetireDate,
                WallId = SelectedWall.Id
            };

            SelectedWall.Boulders.Add(boulder);
            boulder.Id = await _boulderService.CreateBoulder(boulder);
            UpdateDynamicProperties();
            HideBoulderForm();
        }

        [RelayCommand]
        private async Task EditBoulder()
        {
            if (string.IsNullOrWhiteSpace(NewGrade)
                && NewStyle is null
                && string.IsNullOrWhiteSpace(NewAuthor)
                && NewBuiltDate == SelectedBoulder.BuiltDate
                && NewRetireDate == SelectedBoulder.RetireDate)
            {
                HasBoulderInputError = true;
                BoulderInputErrorMessage = "No changes were made.";
                return;
            }
            if (!string.IsNullOrWhiteSpace(NewGrade) && !IsValidGrade(NewGrade))
            {
                HasBoulderInputError = true;
                BoulderInputErrorMessage = "Grade must be in format e.g. 6A or 8B+";
                return;
            }
            if (NewBuiltDate >= NewRetireDate)
            {
                HasBoulderInputError = true;
                BoulderInputErrorMessage = "Built date cannot precede retire date.";
                return;
            }
            if (!string.IsNullOrWhiteSpace(NewGrade)) SelectedBoulder.Grade = NewGrade;
            if (NewStyle != null) SelectedBoulder.Style = NewStyle.Value;
            if (!string.IsNullOrWhiteSpace(NewAuthor)) SelectedBoulder.Author = NewAuthor;
            SelectedBoulder.BuiltDate = NewBuiltDate;
            SelectedBoulder.RetireDate = NewRetireDate;
            await _boulderService.UpdateBoulder(SelectedBoulder);
            UpdateDynamicProperties();
            HideBoulderForm();
        }

        [RelayCommand]
        private async Task DeleteBoulder(BoulderProblemDTO boulder)
        {
            await _boulderService.DeleteBoulder(boulder.Id);
            var wall = Walls.FirstOrDefault(w => w.Id == boulder.WallId);
            wall?.Boulders.Remove(boulder);
            UpdateDynamicProperties();
        }
        private bool IsValidGrade(string grade)
        {
            if (string.IsNullOrWhiteSpace(grade)) return false;
            return _gradeRegex.IsMatch(grade);
        }
    }
}

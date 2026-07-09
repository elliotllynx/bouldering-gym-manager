using BoulderSetManager.Models.Entities;
using BoulderSetManager.Models.Services;
using BoulderSetManager.Models.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using DAL.Enums;
using Microcharts;

namespace BoulderSetManager.ViewModels
{
    [QueryProperty(nameof(GymId), "gymId")]
    public partial class MainViewModel : ObservableObject
    {
        private readonly WallService _wallService = new();
        private readonly BoulderProblemService _boulderService = new();

        public List<BoulderStyle?> Styles { get; } =
            [null, BoulderStyle.Overhang, BoulderStyle.Slab, BoulderStyle.Vertical];

        // page load: 
        [ObservableProperty] public partial int GymId { get; set; }
        [ObservableProperty] public partial GymDTO SelectedGym { get; set; }
        [ObservableProperty] public partial ObservableCollection<WallDTO> Walls { get; set; } = new();
        [ObservableProperty] public partial ObservableCollection<WallDTO> AllWalls { get; set; } = new();

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

        // dynamic displayed properties management:
        private void UpdateDynamicProperties()
        {
            OnPropertyChanged(nameof(AverageGrade));
            UpdateStyleChart();
            OnPropertyChanged(nameof(SelectedWallsSummary));
        }

        public string AverageGrade
        {
            get
            {
                var allBoulders = Walls.SelectMany(w => w.Boulders).ToList();
                if (!allBoulders.Any()) return "N/A";

                var validGrades = allBoulders
                    .Select(b => GradeHelper.ParseGrade(b.Grade))
                    .Where(g => g > 0)
                    .ToList();

                if (!validGrades.Any()) return "N/A";

                return GradeHelper.FormatGrade((int)Math.Round(validGrades.Average()));
            }
        }

        [ObservableProperty] public partial PieChart? StyleChart { get; set; }

        private void UpdateStyleChart()
        {
            StyleChart = ChartBuilder.BuildStyleChart(Walls.SelectMany(w => w.Boulders));
        }

        public bool IsPopUpVisible => IsAddWallVisible || IsEditWallVisible || IsAddBoulderVisible ||
                                      IsEditBoulderVisible || IsWallListExpanded;
    }
}

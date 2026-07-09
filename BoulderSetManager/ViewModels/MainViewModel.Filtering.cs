using BoulderSetManager.Models.Entities;
using BoulderSetManager.Models.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DAL.Enums;
using System.Collections.ObjectModel;

namespace BoulderSetManager.ViewModels
{
    public partial class MainViewModel
    {
        [ObservableProperty] public partial ObservableCollection<object> FilterWalls { get; set; } = new();
        [ObservableProperty] public partial string FilterGrade { get; set; } = string.Empty;
        [ObservableProperty] public partial BoulderStyle? FilterStyle { get; set; } = null;
        [ObservableProperty] public partial string FilterAuthor { get; set; } = string.Empty;
        [ObservableProperty] public partial string FilterRetireInDays { get; set; } = string.Empty;
        [ObservableProperty] public partial bool HasFilterError { get; set; } = false;
        [ObservableProperty] public partial string FilterErrorMessage { get; set; } = string.Empty;
        public string SelectedWallsSummary =>
            FilterWalls.Count == 0 ? "Select walls" :
            FilterWalls.Count == 1 ? "1 wall selected" :
            $"{FilterWalls.Count} walls selected";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPopUpVisible))]
        public partial bool IsWallListExpanded { get; set; } = false;

        [RelayCommand]
        private void ToggleWallList()
        {
            IsWallListExpanded = !IsWallListExpanded;
            OnPropertyChanged(nameof(SelectedWallsSummary));
        }

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

            if (!string.IsNullOrWhiteSpace(FilterGrade) && !GradeHelper.IsValidGrade(FilterGrade))
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

    }
}

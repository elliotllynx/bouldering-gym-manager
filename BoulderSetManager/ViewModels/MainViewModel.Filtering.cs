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
        public List<BoulderStyle?> Styles { get; } =
            [null, BoulderStyle.Overhang, BoulderStyle.Slab, BoulderStyle.Vertical];
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
        private void ApplyFilter()
        {
            if (!ValidFilterCriteria(out int? retireDays)) return;

            if (FilterWalls.Count > 0)
            {
                var filterIds = FilterWalls.Cast<WallDTO>().Select(fw => fw.Id).ToHashSet();
                Walls = new ObservableCollection<WallDTO>(AllWalls.Where(w => filterIds.Contains(w.Id)));
            }

            ResetBoulderVisibility();
            foreach (var wall in Walls)
            {
                foreach (var boulder in wall.Boulders)
                {
                    if ((!string.IsNullOrEmpty(FilterGrade) && boulder.Grade != FilterGrade) ||
                        (FilterStyle is not null && boulder.Style != FilterStyle) ||
                        (!string.IsNullOrEmpty(FilterAuthor) && boulder.Author != FilterAuthor) ||
                        (retireDays is not null && boulder.DaysLeft > retireDays))
                    {
                        boulder.IsVisible = false;
                    }
                }
            }
            UpdateDynamicProperties();
        }

        private bool ValidFilterCriteria(out int? retireDays)
        {
            HasFilterError = false;
            retireDays = null;
            if (string.IsNullOrWhiteSpace(FilterGrade)
                && string.IsNullOrWhiteSpace(FilterAuthor)
                && string.IsNullOrWhiteSpace(FilterRetireInDays)
                && FilterWalls.Count == 0
                && FilterStyle is null)
            {
                HasFilterError = true;
                FilterErrorMessage = "Please input filtering constrain.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(FilterGrade) && !GradeHelper.IsValidGrade(FilterGrade))
            {
                HasFilterError = true;
                FilterErrorMessage = "Please input valid grade.";
                return false;
            }

            if (!string.IsNullOrWhiteSpace(FilterRetireInDays))
            {
                if (int.TryParse(FilterRetireInDays, out int parsed))
                    retireDays = parsed;
                else
                {
                    HasFilterError = true;
                    FilterErrorMessage = "Please input a valid number for days left.";
                    return false;
                }
            }
            return true;
        }

        private void ResetBoulderVisibility()
        {
            foreach (var boulder in AllWalls.SelectMany(w => w.Boulders))
                boulder.IsVisible = true;
        }

        [RelayCommand]
        private void ResetFilter()
        {
            HasFilterError = false;
            FilterWalls = new();
            FilterGrade = string.Empty;
            FilterStyle = null;
            FilterAuthor = string.Empty;
            FilterRetireInDays = string.Empty;
            Walls = AllWalls;
            UpdateDynamicProperties();
        }

        [RelayCommand]
        private void FilterRetiring()
        {
            FilterRetireInDays = "3";
            ApplyFilter();
        }
    }
}

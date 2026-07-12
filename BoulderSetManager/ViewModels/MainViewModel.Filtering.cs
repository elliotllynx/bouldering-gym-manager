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
        private int? RetireInDays { get; set; } = null;
        public List<BoulderStyle?> Styles { get; } =
            [null, BoulderStyle.Overhang, BoulderStyle.Slab, BoulderStyle.Vertical];

        private HashSet<Status> _visibleStatuses = [Status.Active, Status.Draft];
        [ObservableProperty] public partial bool ActiveVisible { get; set; } = true;
        [ObservableProperty] public partial bool DraftVisible { get; set; } = true;
        [ObservableProperty] public partial bool ArchivedVisible { get; set; } = false;
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
        private async Task Filter()
        {
            if (ArchivedVisible && !Loaded.Contains(Status.Archived))
            {
                Loaded.Add(Status.Archived);
                Walls = new ObservableCollection<WallDTO>(await _wallService.GetGymWalls(GymId, Loaded));
            }
            if (!UpdateSelectedStatuses() && !ValidFilterCriteria()) return;
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            if (FilterWalls.Count > 0)
            {
                var filterIds = FilterWalls.Cast<WallDTO>().Select(fw => fw.Id).ToHashSet();
                foreach (WallDTO w in Walls)
                {
                    w.IsVisible = filterIds.Contains(w.Id) && _visibleStatuses.Contains(w.Status);
                }
            }
            else
            {
                foreach (WallDTO w in Walls)
                {
                    w.IsVisible = _visibleStatuses.Contains(w.Status);
                }
            }

            foreach (var wall in Walls)
            {
                if (!wall.IsVisible) continue;
                foreach (var boulder in wall.Boulders)
                {
                    if (!_visibleStatuses.Contains(boulder.Status) ||
                        (!string.IsNullOrEmpty(FilterGrade) && boulder.Grade != FilterGrade) ||
                        (FilterStyle is not null && boulder.Style != FilterStyle) ||
                        (!string.IsNullOrEmpty(FilterAuthor) && boulder.Author != FilterAuthor) ||
                        (RetireInDays is not null && boulder.DaysLeft > RetireInDays))
                    {
                        boulder.IsVisible = false;
                    }
                    else
                    {
                        boulder.IsVisible = true;
                    }
                }
            }
            UpdateDynamicProperties();
        }

        private bool UpdateSelectedStatuses()
        {
            var currentStatuses = new HashSet<Status>();
            if (ActiveVisible) currentStatuses.Add(Status.Active);
            if (DraftVisible) currentStatuses.Add(Status.Draft);
            if (ArchivedVisible) currentStatuses.Add(Status.Archived);
            bool changed = !currentStatuses.SetEquals(_visibleStatuses);
            _visibleStatuses = currentStatuses;
            return changed;
        }

        private bool ValidFilterCriteria()
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
                    RetireInDays = parsed;
                else
                {
                    HasFilterError = true;
                    FilterErrorMessage = "Please input a valid number for days left.";
                    return false;
                }
            }
            return true;
        }

        [RelayCommand]
        private async Task ResetFilter()
        {
            if (ArchivedVisible && !Loaded.Contains(Status.Archived))
            {
                Loaded.Add(Status.Archived);
                Walls = new ObservableCollection<WallDTO>(await _wallService.GetGymWalls(GymId, Loaded));
            }
            HasFilterError = false;
            FilterWalls = new();
            FilterGrade = string.Empty;
            FilterStyle = null;
            FilterAuthor = string.Empty;
            FilterRetireInDays = string.Empty;
            UpdateSelectedStatuses();
            ApplyFilter();
            UpdateDynamicProperties();
        }

        [RelayCommand]
        private async Task FilterRetiring()
        {
            FilterRetireInDays = "3";
            await Filter();
        }
    }
}

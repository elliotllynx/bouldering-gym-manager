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
        // style picker
        public List<BoulderStyle?> Styles { get; } =
            [null, BoulderStyle.Overhang, BoulderStyle.Slab, BoulderStyle.Vertical];

        // filtering placeholders
        [ObservableProperty] public partial string FilterGrade { get; set; } = string.Empty;
        [ObservableProperty] public partial BoulderStyle? FilterStyle { get; set; } = null;
        [ObservableProperty] public partial string FilterAuthor { get; set; } = string.Empty;
        [ObservableProperty] public partial string FilterRetireInDays { get; set; } = string.Empty;

        // filtering error handling
        [ObservableProperty] public partial bool HasFilterError { get; set; } = false;
        [ObservableProperty] public partial string FilterErrorMessage { get; set; } = string.Empty;
        
        // status handling
        private HashSet<Status> _visibleStatuses = [Status.Active, Status.Draft];
        private List<Status> _loadedStatuses = [Status.Active, Status.Draft];
        [ObservableProperty] public partial bool ActiveVisible { get; set; } = true;
        [ObservableProperty] public partial bool DraftVisible { get; set; } = true;
        [ObservableProperty] public partial bool ArchivedVisible { get; set; } = false;

        // multiple wall selection
        [ObservableProperty] public partial ObservableCollection<object> FilterWalls { get; set; } = new(); // binds to ui collection view checker

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPopUpVisible))]
        public partial bool IsWallListExpanded { get; set; } = false;
        public string SelectedWallsSummary =>
            FilterWalls.Count == 0 ? "Select walls" :
            FilterWalls.Count == 1 ? "1 wall selected" :
            $"{FilterWalls.Count} walls selected";

        [RelayCommand]
        private void ToggleWallList()
        {
            IsWallListExpanded = !IsWallListExpanded;
            OnPropertyChanged(nameof(SelectedWallsSummary));
        }

        // actual filtering 
        [RelayCommand]
        public async Task Filter()
        {
            // needs to be like this because && is lazy
            bool statusesChanged = await CheckAndLoadVisibleStyles();
            bool validCriteria = ValidFilterCriteria();
            if (!statusesChanged && !validCriteria) return;
            ApplyFilter();
        }

        // returns true if statuses changed from last filtering
        private async Task<bool> CheckAndLoadVisibleStyles()
        {
            if (ArchivedVisible && !_loadedStatuses.Contains(Status.Archived))
            {
                _loadedStatuses.Add(Status.Archived);
                Walls = new ObservableCollection<WallDTO>(await _wallService.GetGymWalls(GymId, _loadedStatuses));
            }

            var newStatuses = new HashSet<Status>();
            if (ActiveVisible) newStatuses.Add(Status.Active);
            if (DraftVisible) newStatuses.Add(Status.Draft);
            if (ArchivedVisible) newStatuses.Add(Status.Archived);
            bool changed = !newStatuses.SetEquals(_visibleStatuses);
            _visibleStatuses = newStatuses;
            return changed;
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

            int? retireInDays = string.IsNullOrWhiteSpace(FilterRetireInDays) ? null : int.Parse(FilterRetireInDays);

            foreach (var wall in Walls)
            {
                if (!wall.IsVisible) continue;
                foreach (var boulder in wall.Boulders)
                {
                    if (!_visibleStatuses.Contains(boulder.Status) ||
                        (!string.IsNullOrEmpty(FilterGrade) && boulder.Grade != FilterGrade) ||
                        (FilterStyle is not null && boulder.Style != FilterStyle) ||
                        (!string.IsNullOrEmpty(FilterAuthor) && boulder.Author != FilterAuthor) ||
                        (retireInDays is not null && boulder.DaysLeft > retireInDays))
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

        // checks input criteria and sets RetireInDays if given
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

            if (!string.IsNullOrWhiteSpace(FilterRetireInDays) && !int.TryParse(FilterRetireInDays, out int _))
            {
                HasFilterError = true;
                FilterErrorMessage = "Please input a valid number for days left.";
                return false;
            }
            return true;
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

            await CheckAndLoadVisibleStyles();
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

using BoulderSetManager.Models.Entities;
using BoulderSetManager.Models.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BoulderSetManager.ViewModels
{
    public partial class MainViewModel
    {
        private readonly WallService _wallService = new();

        // represents exact state of database at all times, filtering with wall property IsVisible not by removal from this collection
        [ObservableProperty] public partial ObservableCollection<WallDTO> Walls { get; set; } = new();

        // for ui wall crud
        [ObservableProperty] public partial WallDTO? SelectedWall { get; set; } = null;
        [ObservableProperty] public partial string NewWallName { get; set; } = string.Empty;

        // error wall crud handling
        [ObservableProperty] public partial bool HasWallInputError { get; set; } = false;
        [ObservableProperty] public partial string WallInputErrorMessage { get; set; } = string.Empty;

        // popup handling
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPopUpVisible))]
        public partial bool IsAddWallVisible { get; set; } = false;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPopUpVisible))]
        public partial bool IsEditWallVisible { get; set; } = false;

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

        // wall crud
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
            // UpdateDynamicProperties(); not needed (yet) because adding wall doesnt influence boulders
            HideWallForm();
        }

        [RelayCommand]
        private async Task EditWall()
        {
            if (SelectedWall is null) return; // should never happen, because ShowEditWall() always sets SelectedWall

            if (string.IsNullOrWhiteSpace(NewWallName))
            {
                HasWallInputError = true;
                WallInputErrorMessage = "Please input new name.";
                return;
            }

            SelectedWall.Name = NewWallName;
            HideWallForm();
            // UpdateDynamicProperties(); not needed (yet) because editing wall doesnt influence boulders
            await _wallService.UpdateWall(SelectedWall);
        }

        [RelayCommand]
        private async Task DeleteWall(WallDTO wall)
        {
            await _wallService.DeleteWall(wall.Id);
            Walls.Remove(wall);
            UpdateDynamicProperties();
        }
    }
}

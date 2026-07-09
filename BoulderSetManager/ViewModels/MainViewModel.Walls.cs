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
        [ObservableProperty] public partial string NewWallName { get; set; } = string.Empty;
        [ObservableProperty] public partial WallDTO? SelectedWall { get; set; } = null;
        [ObservableProperty] public partial bool HasWallInputError { get; set; } = false;
        [ObservableProperty] public partial string WallInputErrorMessage { get; set; } = string.Empty;

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
            var filterItem = FilterWalls.Cast<WallDTO>().FirstOrDefault(fw => fw.Id == wall.Id);
            if (filterItem != null)
                FilterWalls.Remove(filterItem);
            UpdateDynamicProperties();
        }
    }
}

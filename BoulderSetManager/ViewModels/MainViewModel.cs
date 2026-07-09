using BoulderSetManager.Models.Entities;
using BoulderSetManager.Models.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace BoulderSetManager.ViewModels
{
    [QueryProperty(nameof(GymId), "gymId")]
    public partial class MainViewModel : ObservableObject
    {
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

        public bool IsPopUpVisible => IsAddWallVisible || IsEditWallVisible ||
                                      IsAddBoulderVisible || IsEditBoulderVisible ||
                                      IsWallListExpanded;
    }
}

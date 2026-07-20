using BoulderSetManager.Models.Entities;
using BoulderSetManager.Models.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using DAL.Enums;

namespace BoulderSetManager.ViewModels
{
    [QueryProperty(nameof(GymId), "gymId")]
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] public partial int GymId { get; set; }
        [ObservableProperty] public partial GymDTO? SelectedGym { get; set; }
        public List<Status> Loaded { get; set; } = [Status.Active, Status.Draft];
        async partial void OnGymIdChanged(int value)
        {
            var gymService = new GymService();
            SelectedGym = await gymService.GetGym(value);
            if (SelectedGym is null)
            {
                await GoBack();
                return;
            }

            await InitLoadWalls(value);
            UpdateDynamicProperties();
        }

        private async Task InitLoadWalls(int gymId)
        {
            Walls = new ObservableCollection<WallDTO>(await _wallService.GetGymWalls(gymId, Loaded));
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

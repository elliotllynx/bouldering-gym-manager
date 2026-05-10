using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoulderSetManager.ViewModels
{
    [QueryProperty(nameof(GymId), "gymId")]
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        public partial int GymId { get; set; }

        partial void OnGymIdChanged(int value)
        {
            // called automatically when gymId arrives, load your data here
            _ = LoadData(value);
        }

        private async Task LoadData(int gymId)
        {
            // fetch walls, boulders etc for this gym
        }
    }
}

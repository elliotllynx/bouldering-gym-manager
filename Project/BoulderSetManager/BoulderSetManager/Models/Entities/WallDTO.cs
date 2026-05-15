using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoulderSetManager.Models.Entities
{
    public partial class WallDTO : ObservableObject
    {
        public int Id { get; set; }
        public int GymId { get; set; }
        [ObservableProperty] public partial string Name { get; set; }
        [ObservableProperty] public partial ObservableCollection<BoulderingProblemDTO> Boulders { get; set; } = new();
    }
}

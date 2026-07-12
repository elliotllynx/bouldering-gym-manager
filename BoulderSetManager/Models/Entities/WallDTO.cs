using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using DAL.Enums;

namespace BoulderSetManager.Models.Entities
{
    public partial class WallDTO : ObservableObject
    {
        public int Id { get; set; }
        public int GymId { get; set; }
        [ObservableProperty] public partial string Name { get; set; }
        public Status Status { get; set; }
        [ObservableProperty] public partial bool IsVisible { get; set; }
        [ObservableProperty] public partial ObservableCollection<BoulderProblemDTO> Boulders { get; set; } = new();
    }
}

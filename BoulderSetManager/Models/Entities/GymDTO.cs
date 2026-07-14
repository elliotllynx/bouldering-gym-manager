using CommunityToolkit.Mvvm.ComponentModel;
using DAL.Enums;

namespace BoulderSetManager.Models.Entities
{ 
    public partial class GymDTO : ObservableObject
    {
        // ============================================================
        // Data — persist to database
        // ============================================================
        public int Id { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayName))]
        public partial string Name { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayName))]
        public partial string Location { get; set; }

        public Status Status { get; set; }

        // ============================================================
        // Info - Additional computed info/property for UI
        // ============================================================

        public string DisplayName => $"{Name} ({Location})";
    }
}

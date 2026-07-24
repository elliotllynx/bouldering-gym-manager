using CommunityToolkit.Mvvm.ComponentModel;
using DAL.Enums;

namespace BoulderSetManager.Models.Entities
{
    public partial class BoulderProblemDTO : ObservableObject
    {
        // ============================================================
        // Data — persist to database
        // ============================================================

        public int Id { get; set; }

        public int WallId { get; set; }

        [ObservableProperty] public partial string Grade { get; set; }

        [ObservableProperty] public partial string Author { get; set; }

        [ObservableProperty] public partial BoulderStyle Style { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DaysLeft))]
        public partial DateTime BuiltDate { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DaysLeft))]
        [NotifyPropertyChangedFor(nameof(RowBackgroundColor))]
        public partial DateTime RetireDate { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RowBackgroundColor))]
        [NotifyPropertyChangedFor(nameof(ArchiveButtonText))]
        public partial Status Status { get; set; }

        // ============================================================
        // Info - Additional computed info/property for UI
        // ============================================================

        public int DaysLeft => (RetireDate - DateTime.Today).Days;

        // ============================================================
        // Visual - properties computed purely for visualization
        // ============================================================

        // state property deciding whether boulder is shown in the ui or not, used for filtering
        [ObservableProperty] public partial bool IsVisible { get; set; } = true;

        public Color RowBackgroundColor => Status switch
        {
            Status.Archived => ((Color)Application.Current!.Resources["Tertiary"]).WithAlpha(0.2f),
            Status.Draft => ((Color)Application.Current!.Resources["Secondary"]).WithAlpha(0.2f),
            _ => DaysLeft <= 3 ? Colors.DarkRed.WithAlpha(0.2f) : Colors.Transparent
        };
        public string ArchiveButtonText => Status == Status.Archived ? "Unarchive" : "Archive";
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using DAL.Enums;

namespace BoulderSetManager.Models.Entities
{
    public partial class BoulderProblemDTO : ObservableObject
    {
        public int Id { get; set; }
        public int WallId { get; set; }
        [ObservableProperty] public partial string Grade { get; set; }
        [ObservableProperty] public partial BoulderStyle Style { get; set; }
        [ObservableProperty] public partial string Author { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DaysLeft))]
        public partial DateTime BuiltDate { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DaysLeft))]
        [NotifyPropertyChangedFor(nameof(RowColor))]
        public partial DateTime RetireDate { get; set; }
        public int DaysLeft => (RetireDate - DateTime.Today).Days;

        public Color RowColor => Status switch
        {
            Status.Archived => Colors.Yellow.WithAlpha(0.1f),
            Status.Draft => Colors.DarkCyan.WithAlpha(0.2f),
            _ => DaysLeft <= 3 ? Colors.DarkRed.WithAlpha(0.2f) : Colors.Transparent
        };
        [ObservableProperty] public partial bool IsVisible { get; set; } = true;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(RowColor))]
        [NotifyPropertyChangedFor(nameof(ArchivedButtonText))]
        public partial Status Status { get; set; }
        public string ArchivedButtonText => Status == Status.Archived ? "Unarchive" : "Archive";
    }
}

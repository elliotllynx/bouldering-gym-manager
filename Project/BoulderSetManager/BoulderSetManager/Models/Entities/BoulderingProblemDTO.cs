using CommunityToolkit.Mvvm.ComponentModel;

namespace BoulderSetManager.Models.Entities
{
    public partial class BoulderingProblemDTO : ObservableObject
    {
        public int Id { get; set; }
        public int WallId { get; set; }
        [ObservableProperty] public partial string Grade { get; set; }
        [ObservableProperty] public partial string Style { get; set; }
        [ObservableProperty] public partial string Author { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DaysLeft))]
        public partial DateTime BuiltDate { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DaysLeft))]
        [NotifyPropertyChangedFor(nameof(RowColor))]
        public partial DateTime RetireDate { get; set; }
        public int DaysLeft => (RetireDate - DateTime.Today).Days;
        public Color RowColor => DaysLeft <= 3 ? Color.FromArgb("#1Fdc2626") : Colors.Transparent;
    }
}

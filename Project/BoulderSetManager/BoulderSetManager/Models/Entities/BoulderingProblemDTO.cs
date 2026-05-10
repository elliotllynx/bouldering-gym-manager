using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BoulderSetManager.Models.Entities
{
    public partial class BoulderingProblemDTO : ObservableObject
    {
        public int Id { get; set; }
        public int WallId { get; set; }

        [ObservableProperty] public partial string Grade { get; set; }
        [ObservableProperty] public partial string Type { get; set; }
        [ObservableProperty] public partial string Author { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DaysLeft))]
        public partial DateTime BuiltDate { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DaysLeft))]
        public partial DateTime RetireDate { get; set; }

        public int DaysLeft => (RetireDate - DateTime.Today).Days;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BoulderSetManager.Models.Entities
{ 
    public partial class GymDTO : ObservableObject
    {
        public int Id { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayName))]
        public partial string Name { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(DisplayName))]
        public partial string Location { get; set; }
        public string DisplayName => $"{Name} ({Location})";
    }
}

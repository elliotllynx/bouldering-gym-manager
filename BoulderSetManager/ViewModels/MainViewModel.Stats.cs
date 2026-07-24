using BoulderSetManager.Models.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using Microcharts;

namespace BoulderSetManager.ViewModels
{
    public partial class MainViewModel
    {
        [ObservableProperty] public partial PieChart? StyleChart { get; set; }
        private readonly ChartBuilder _chartBuilder = new();
        public string AverageGrade
        {
            get
            {
                var allBoulders = Walls.SelectMany(w => w.Boulders).ToList();
                if (!allBoulders.Any()) return "N/A";

                var validGrades = allBoulders
                    .Select(b => GradeHelper.ParseGrade(b.Grade))
                    .Where(g => g > 0)
                    .ToList();

                if (!validGrades.Any()) return "N/A";

                return GradeHelper.FormatGrade((int)Math.Round(validGrades.Average()));
            }
        }

        // called from everywhere where any of currently visible boulders was edited or changed
        public void UpdateDynamicProperties()
        {
            OnPropertyChanged(nameof(AverageGrade));
            UpdateStyleChart();
            OnPropertyChanged(nameof(SelectedWallsSummary));
        }

        private void UpdateStyleChart()
        {
            StyleChart = _chartBuilder.BuildStyleChart(Walls.SelectMany(w => w.Boulders));
        }
    }
}

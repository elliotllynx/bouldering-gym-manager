using BoulderSetManager.Models.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using Microcharts;

namespace BoulderSetManager.ViewModels
{
    public partial class MainViewModel
    {
        [ObservableProperty] public partial PieChart? StyleChart { get; set; }
        private void UpdateDynamicProperties()
        {
            OnPropertyChanged(nameof(AverageGrade));
            UpdateStyleChart();
            OnPropertyChanged(nameof(SelectedWallsSummary));
        }
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
        private void UpdateStyleChart()
        {
            StyleChart = ChartBuilder.BuildStyleChart(Walls.SelectMany(w => w.Boulders));
        }
    }
}

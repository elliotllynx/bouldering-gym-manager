using BoulderSetManager.Models.Entities;
using DAL.Enums;
using Microcharts;
using SkiaSharp;

namespace BoulderSetManager.Models.Helpers
{
    public static class ChartBuilder
    {
        public static PieChart? BuildStyleChart(IEnumerable<BoulderProblemDTO> boulders)
        {
            var data = boulders.GroupBy(b => b.Style);
            var entries = new List<ChartEntry>();

            foreach (var group in data)
            {
                entries.Add(new ChartEntry(group.Count())
                {
                    Color = SKColor.Parse(GetColorForStyle(group.Key))
                });
            }

            if (!entries.Any())
            {
                return null;
            }

            return new PieChart
            {
                Entries = entries,
                BackgroundColor = SKColors.Transparent,
                LabelMode = LabelMode.None
            };
        }

        private static string GetColorForStyle(BoulderStyle style) => style switch
        {
            BoulderStyle.Slab => "#40E0D0",
            BoulderStyle.Vertical => "#B8860B",
            BoulderStyle.Overhang => "#c0392b",
            _ => "#000000"           // Black
        };
    }
}
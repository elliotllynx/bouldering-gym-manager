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
            BoulderStyle.Slab => "#00FFFF",     // Cyan
            BoulderStyle.Vertical => "#FF00FF", // Magenta
            BoulderStyle.Overhang => "#FFFF00", // Yellow
            _ => "#000000"           // Black
        };
    }
}
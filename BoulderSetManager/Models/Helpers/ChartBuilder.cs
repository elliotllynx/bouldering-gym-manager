using BoulderSetManager.Models.Entities;
using DAL.Enums;
using Microcharts;
using SkiaSharp;
using SkiaSharp.Views.Maui;

namespace BoulderSetManager.Models.Helpers
{
    public class ChartBuilder
    {
        private readonly Color _primary = (Color)Application.Current!.Resources["Primary"];
        private readonly Color _secondary = (Color)Application.Current!.Resources["Secondary"];
        private readonly Color _tertiary = (Color)Application.Current!.Resources["Tertiary"];

        public PieChart? BuildStyleChart(IEnumerable<BoulderProblemDTO> boulders)
        {
            var data = boulders.GroupBy(b => b.Style);
            var entries = new List<ChartEntry>();

            foreach (var group in data)
            {
                entries.Add(new ChartEntry(group.Count())
                {
                    Color = GetColorForStyle(group.Key).ToSKColor()
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

        private Color GetColorForStyle(BoulderStyle style) => style switch
        {
            BoulderStyle.Slab => _primary,
            BoulderStyle.Vertical => _tertiary,
            BoulderStyle.Overhang => _secondary,
            _ => Colors.Black
        };
    }
}
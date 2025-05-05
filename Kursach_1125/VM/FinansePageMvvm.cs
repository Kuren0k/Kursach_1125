using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveChartsCore.Kernel;
using LiveChartsCore.SkiaSharpView.Extensions;
using LiveChartsCore.SkiaSharpView.VisualElements;

namespace Kursach_1125.VM
{
    internal class FinansePageMvvm
    {
        public ISeries[] Series { get; set; } = [
        new LineSeries<int> { Values = [10, 55, 45, 68, 60, 70, 75, 78] }
    ];

        public ICartesianAxis[] YAxes { get; set; } = [
            new Axis
        {
            CustomSeparators = [0, 10, 25, 50, 100],
            MinLimit = 0,
            MaxLimit = 100,
            SeparatorsPaint = new SolidColorPaint(SKColors.Black.WithAlpha(100))
        }
        ];

        public ISeries[] Series1 { get; set; }
            = new ISeries[]
            {
                new PieSeries<double> { Values = new double[] { 2 } },
                new PieSeries<double> { Values = new double[] { 4 } },
                new PieSeries<double> { Values = new double[] { 1 } },
                new PieSeries<double> { Values = new double[] { 4 } },
                new PieSeries<double> { Values = new double[] { 3 } }
            };

        public LabelVisual Title { get; set; } =
            new LabelVisual
            {
                Text = "My chart title",
                TextSize = 25,
                Padding = new LiveChartsCore.Drawing.Padding(15)
            };
    }
}


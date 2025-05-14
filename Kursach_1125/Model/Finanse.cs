using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;

namespace Kursach_1125.Model
{
    public class FinansePieChartDataModel
    {
        public ChartValues<int> ConsumptionDataSeries { get; set; }
        public ChartValues<int> IncomeDataSeries { get; set; }

        public FinansePieChartDataModel(int consumption, int income)
        {
            ConsumptionDataSeries = new ChartValues<int> { consumption };
            IncomeDataSeries = new ChartValues<int> { income };
        }
    }
}

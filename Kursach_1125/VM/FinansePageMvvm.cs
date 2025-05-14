using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursach_1125.Model;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace Kursach_1125.VM
{
    internal class FinansePageMvvm : BaseVM
    {
        public FinansePieChartDataModel FinansePieChartModel { get; set; }
        public Func<ChartPoint, string> PointLabel { get; set; }

        
        public FinansePageMvvm()
        {
            SelectAll();
            PointLabel = chartPoint => string.Format("{0}({1:P})", chartPoint.Y, chartPoint.Participation);
            
        }
        private void SelectAll()
        {
            List<Employee> allEmployee = EmployeeDB.GetDB().SelectAll();
            List<Expenses> allExpenses = ExpensesDB.GetDB().SelectAll();
            List<Agreement> allAgreement  = AgreementDB.GetDB().SelectAll();
            int totalCost = allExpenses.Sum(e => e.Cost);
            totalCost += allEmployee.Sum(e => e.JobTitles.Wages);
            int totalIncome = allAgreement.Sum(e => e.RentalRate);
            FinansePieChartModel = new FinansePieChartDataModel(totalCost, totalIncome);
        }
    }
}


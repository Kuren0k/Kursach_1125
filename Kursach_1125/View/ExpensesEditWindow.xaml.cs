using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Kursach_1125.Model;
using Kursach_1125.VM;

namespace Kursach_1125.View
{
    /// <summary>
    /// Логика взаимодействия для ExpensesEditWindow.xaml
    /// </summary>
    public partial class ExpensesEditWindow : Window
    {
        public ExpensesEditWindow(Expenses expenses)
        {
            InitializeComponent();
            var vm = new ExpensesEditWindowMvvm();
            vm.NewExpenses = expenses;
            vm.SetClose(() => this.Close());

            DataContext = vm;
            ((ExpensesEditWindowMvvm)this.DataContext).SetClose(Close);
        }
    }
}

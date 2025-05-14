using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursach_1125.Model;
using Kursach_1125.View;

namespace Kursach_1125.VM
{
    internal class ExpensesPageMvvm :  BaseVM
    {
        private Expenses selectedExpenses;
        private ObservableCollection<Expenses> expenses;

        public ObservableCollection<Expenses> Expenses
        {
            get => expenses;
            set
            {
                expenses = value;
                Signal();
            }
        }
        public Expenses SelectedExpenses
        {
            get => selectedExpenses;
            set
            {
                selectedExpenses = value;
                Signal();
            }
        }
        public CommandMvvm UpdateExpenses { get; set; }
        public CommandMvvm RemoveExpenses { get; set; }
        public CommandMvvm AddExpenses { get; set; }

        public ExpensesPageMvvm()
        {
            SelectAll();

            UpdateExpenses = new CommandMvvm(() =>
            {
                new ExpensesEditWindow(SelectedExpenses).ShowDialog();
                SelectAll();
            }, () => SelectedExpenses != null);

            RemoveExpenses = new CommandMvvm(() =>
            {
                ExpensesDB.GetDB().Remove(SelectedExpenses);
                SelectAll();
            }, () => SelectedExpenses != null);

            AddExpenses = new CommandMvvm(() =>
            {
                new ExpensesEditWindow(new Expenses()).ShowDialog();
                SelectAll();
            }, () => true);
        }

        private void SelectAll()
        {
            Expenses = new ObservableCollection<Expenses>(ExpensesDB.GetDB().SelectAll());

        }
    }
}

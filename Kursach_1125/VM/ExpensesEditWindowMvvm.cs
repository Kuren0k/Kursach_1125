using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursach_1125.Model;

namespace Kursach_1125.VM
{
    internal class ExpensesEditWindowMvvm : BaseVM
    {
        private Expenses newExpenses = new();

        public Expenses NewExpenses
        {
            get => newExpenses;
            set
            {
                newExpenses = value;
                Signal();
            }
        }

        public CommandMvvm InsertExpenses { get; set; }

        public ExpensesEditWindowMvvm()
        {

            InsertExpenses = new CommandMvvm(() =>
            {
                if (newExpenses.Id == 0)
                    ExpensesDB.GetDB().Insert(NewExpenses);
                else
                {
                    ExpensesDB.GetDB().Update(NewExpenses);
                }
                close?.Invoke();
            },
            () =>
            !string.IsNullOrEmpty(NewExpenses.Title) && NewExpenses.Cost > 0);

        }
        public void SetZone(Expenses selectedExpenses)
        {
            NewExpenses = selectedExpenses;
        }

        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }
    }
}

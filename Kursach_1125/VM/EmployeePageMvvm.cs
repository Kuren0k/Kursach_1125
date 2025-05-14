using Kursach_1125.Model;
using Kursach_1125.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_1125.VM
{
    internal class EmployeePageMvvm : BaseVM
    {
        private Employee selectedEmployee;
        private ObservableCollection<Employee> employees;

        public ObservableCollection<Employee> Employees
        {
            get => employees;
            set
            {
                employees = value;
                Signal();
            }
        }
        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = value;
                Signal();
            }
        }
        public CommandMvvm UpdateEmployee { get; set; }
        public CommandMvvm RemoveEmployee { get; set; }
        public CommandMvvm AddEmployee { get; set; }

        public EmployeePageMvvm()
        {
            SelectAll();

            UpdateEmployee = new CommandMvvm(() =>
            {
                new EmployeeEditWindow(SelectedEmployee).ShowDialog();
                SelectAll();
            }, () => SelectedEmployee != null);

            RemoveEmployee = new CommandMvvm(() =>
            {
                EmployeeDB.GetDB().Remove(SelectedEmployee);
                SelectAll();
            }, () => SelectedEmployee != null);

            AddEmployee = new CommandMvvm(() =>
            {
                new EmployeeEditWindow(new Employee()).ShowDialog();
                SelectAll();
            }, () => true);
        }

        private void SelectAll()
        {
            Employees = new ObservableCollection<Employee>(EmployeeDB.GetDB().SelectAll());
        }
    }
}

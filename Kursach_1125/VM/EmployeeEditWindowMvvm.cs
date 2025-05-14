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
    internal class EmployeeEditWindowMvvm : BaseVM
    {

        private Employee newEmployee = new();

        public Employee NewEmployee
        {
            get => newEmployee;
            set
            {
                newEmployee = value;
                Signal();
            }
        }

        public ObservableCollection<TPKZone> TPKZonesList { get; set; }
        public ObservableCollection<JobTitle> JobTitlesList { get; set; }

        public CommandMvvm InsertEmployee { get; set; }

        public EmployeeEditWindowMvvm()
        {
            TPKZonesList = new ObservableCollection<TPKZone>(TPKZoneDB.GetDB().SelectAll());
            JobTitlesList = new ObservableCollection<JobTitle>(JobTitleDB.GetDB().SelectAll());

            InsertEmployee = new CommandMvvm(() =>
            {
                NewEmployee.JobTitleID = NewEmployee.JobTitles.Id;
                NewEmployee.TPKZoneID = NewEmployee.TPKZones.Id;
                if (newEmployee.Id == 0)
                    EmployeeDB.GetDB().Insert(NewEmployee);
                else
                {
                    EmployeeDB.GetDB().Update(NewEmployee);
                }
                close?.Invoke();
            },
                () =>
                NewEmployee.JobTitles != null &&
                NewEmployee.TPKZones != null &&
                !string.IsNullOrEmpty(NewEmployee.PhoneNumber) &&
                !string.IsNullOrEmpty(NewEmployee.FIO));

        }
        public void SetZone(Employee selectedEmployee)
        {
            NewEmployee = selectedEmployee;
        }

        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }
    }
}
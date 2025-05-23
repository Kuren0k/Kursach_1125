﻿using Kursach_1125.Model;
using Kursach_1125.VM;
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

namespace Kursach_1125.View
{
    /// <summary>
    /// Логика взаимодействия для EmployeeEditWindow.xaml
    /// </summary>
    public partial class EmployeeEditWindow : Window
    {
        public EmployeeEditWindow(Employee employee)
        {
            InitializeComponent();
            var vm = new EmployeeEditWindowMvvm();
            vm.NewEmployee = employee;
            vm.SetClose(() => this.Close());

            DataContext = vm;
            ((EmployeeEditWindowMvvm)this.DataContext).SetClose(Close);
        }
    }
}

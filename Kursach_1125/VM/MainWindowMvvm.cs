using Kursach_1125.Model;
using Kursach_1125.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Kursach_1125.VM
{
    internal class MainWindowMvvm : BaseVM
    {
        public CommandMvvm PageNavigation { get; set; }
        public CommandMvvm PageNavigationEm { get; set; }
        public CommandMvvm PageNavigationF { get; set; }
        public CommandMvvm PageNavigationAgreement { get; set; }
        public CommandMvvm PageNavigationTentant { get; set; }
        public CommandMvvm PageNavigationTPK { get; set; }
        private Frame frame;
        FinansePage finanse;
        HomePage home;
        EmployeePage employee;
        AgreementPage agreement;
        TentantPage tentant;
        TPKZonePage tPKZone;
        public MainWindowMvvm()
        { 
       
            PageNavigationF = new CommandMvvm(() =>
            {
                if (finanse == null)
                {
                    finanse =new FinansePage();
                }
                frame.Navigate(finanse);
            },
            ()=> true);

            PageNavigationEm = new CommandMvvm(() =>
            {
                if (employee == null)
                {
                    employee = new EmployeePage();
                }
                frame.Navigate(employee);
            },
            () => true);

            PageNavigationAgreement = new CommandMvvm(() =>
            {
                if (agreement == null)
                {
                    agreement = new AgreementPage();
                }
                frame.Navigate(agreement);
            },
            () => true);

            PageNavigationTentant = new CommandMvvm(() =>
            {
                if (tentant == null)
                {
                    tentant = new TentantPage();
                }
                frame.Navigate(tentant);
            },
            () => true);

            PageNavigationTPK = new CommandMvvm(() =>
            {
                if (tPKZone == null)
                {
                    tPKZone = new TPKZonePage();
                }
                frame.Navigate(tPKZone);
            },
            () => true);

            PageNavigation = new CommandMvvm(() =>
            {
                frame.Navigate(home);
            },
            ()=> true);
        }

        internal void SetFrame(Frame frame)
        {
            this.frame = frame;

            home = new HomePage();
            frame.Navigate(home);
        }
    }
}

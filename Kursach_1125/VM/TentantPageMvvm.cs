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
    internal class TentantPageMvvm : BaseVM
    {
        private Tentant selectedTentant;
        private ObservableCollection<Tentant> tentants;

        public ObservableCollection<Tentant> Tentants
        {
            get => tentants;
            set
            {
                tentants = value;
                Signal();
            }
        }
        public Tentant SelectedTentant
        {
            get => selectedTentant;
            set
            {
                selectedTentant = value;
                Signal();
            }
        }
        public CommandMvvm UpdateTentant { get; set; }
        public CommandMvvm RemoveTentant { get; set; }
        public CommandMvvm AddTentant { get; set; }

        public TentantPageMvvm()
        {
            SelectAll();

            UpdateTentant = new CommandMvvm(() =>
            {
                new TentantEditWindow(SelectedTentant).ShowDialog();
                SelectAll();
            }, () => SelectedTentant != null);

            RemoveTentant = new CommandMvvm(() =>
            {
                TentantDB.GetDB().Remove(SelectedTentant);
                SelectAll();
            }, () => SelectedTentant != null);

            AddTentant = new CommandMvvm(() =>
            {
                new TentantEditWindow(new Tentant()).ShowDialog();
                SelectAll();
            }, () => true);
        }

        private void SelectAll()
        {
            Tentants = new ObservableCollection<Tentant>(TentantDB.GetDB().SelectAll());
        }
    }
}

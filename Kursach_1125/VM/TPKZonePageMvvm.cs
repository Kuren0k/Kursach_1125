using Kursach_1125.Model;
using Kursach_1125.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Kursach_1125.VM
{
    internal class TPKZonePageMvvm : BaseVM
    {
        private TPKZone selectedZone;
        private ObservableCollection<TPKZone> zones;

        public ObservableCollection<TPKZone> Zones
        {
            get => zones;
            set
            {
                zones = value;
                Signal();
            }
        }
        public TPKZone SelectedZone
        {
            get => selectedZone;
            set
            {
                selectedZone = value;
                Signal();
            }
        }
        public CommandMvvm UpdateZone {  get; set; }
        public CommandMvvm RemoveZone { get; set; }
        public CommandMvvm AddZone { get; set;}

        public TPKZonePageMvvm()
        {
            SelectAll();

            UpdateZone = new CommandMvvm(() =>
            {
                new TPKZoneEditWindow(SelectedZone).ShowDialog();
                SelectAll();
            }, () => SelectedZone != null);

            RemoveZone = new CommandMvvm(() =>
            {
                TPKZoneDB.GetDB().Remove(SelectedZone);
                SelectAll();
            }, () => SelectedZone != null);

            AddZone = new CommandMvvm(() =>
            {
                new TPKZoneEditWindow(new TPKZone()).ShowDialog();
                SelectAll();
            }, () => true);
        }

        private void SelectAll()
        {
            Zones = new ObservableCollection<TPKZone>(TPKZoneDB.GetDB().SelectAll());
        }
    }
}

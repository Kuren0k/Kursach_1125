using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows;
using Kursach_1125.Model;
using Kursach_1125.View;

namespace Kursach_1125.VM
{
    internal class AgreementPageMvvm : BaseVM
    {
        private Agreement selectedAgreement;
        private ObservableCollection<Agreement> agreements;

        public ObservableCollection<Agreement> Agreements
        {
            get => agreements;
            set
            {
                agreements = value;
                Signal();
            }
        }
        public Agreement SelectedAgreement
        {
            get => selectedAgreement;
            set
            {
                selectedAgreement = value;
                Signal();
            }
        }
        public CommandMvvm UpdateAgreement { get; set; }
        public CommandMvvm RemoveAgreement { get; set; }
        public CommandMvvm AddAgreement { get; set; }
        public CommandMvvm CreateDocAgreement { get; set; }

        public AgreementPageMvvm()
        {
            SelectAll();

            UpdateAgreement = new CommandMvvm(() =>
            {
                new AgreementEditWindow(SelectedAgreement).ShowDialog();
                SelectAll();
            }, () => SelectedAgreement != null);

            RemoveAgreement = new CommandMvvm(() =>
            {
                AgreementDB.GetDB().Remove(SelectedAgreement);
                SelectAll();
            }, () => SelectedAgreement != null);

            AddAgreement = new CommandMvvm(() =>
            {
                new AgreementEditWindow(new Agreement()).ShowDialog();
                SelectAll();
            }, () => true);
            CreateDocAgreement = new CommandMvvm(() =>
            {
                bool result = AgreementDB.GetDB().GenerateAgreementDocAndSaveToDb(SelectedAgreement);

                if (result)
                    MessageBox.Show("Договор успешно создан и сохранён в базе!");
                else
                    MessageBox.Show("Ошибка при создании договора.");
            }, () => SelectedAgreement != null);
        }

        private void SelectAll()
        {
            Agreements = new ObservableCollection<Agreement>(AgreementDB.GetDB().SelectAll());
        }
    }
}

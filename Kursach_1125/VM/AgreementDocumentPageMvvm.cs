using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursach_1125.Model;
using Kursach_1125.View;
using System.Windows;

namespace Kursach_1125.VM
{
    internal class AgreementDocumentPageMvvm : BaseVM
    {
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
        private AgreementDocument selectedDocument;
        private ObservableCollection<AgreementDocument> documents;

        public ObservableCollection<AgreementDocument> Documents
        {
            get => documents;
            set
            {
                documents = value;
                Signal();
            }
        }
        public AgreementDocument SelectedDocument
        {
            get => selectedDocument;
            set
            {
                selectedDocument = value;
                Signal();
            }
        }
        public CommandMvvm RemoveDocument { get; set; }
        public CommandMvvm OpenDocument { get; set; }
        public AgreementDocumentPageMvvm()
        {
            SelectAll();

            RemoveDocument = new CommandMvvm(() =>
            {
                AgreementDocumentDB.GetDB().Remove(SelectedDocument);
                SelectAll();
            }, () => SelectedDocument != null);

            OpenDocument = new CommandMvvm(() =>
            {
                AgreementDocumentDB.GetDB().OpenDocument(SelectedDocument);
                SelectAll();
            }, () => SelectedDocument != null);
        }

        private void SelectAll()
        {
            Agreements = new ObservableCollection<Agreement>(AgreementDB.GetDB().SelectAll());
            Documents = new ObservableCollection<AgreementDocument>(AgreementDocumentDB.GetDB().SelectAll());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursach_1125.Model;

namespace Kursach_1125.VM
{
    internal class AgreementEditWindowMvvm : BaseVM
    {
        private Agreement newAgreement = new();

        public Agreement NewAgreement
        {
            get => newAgreement;
            set
            {
                newAgreement = value;
                Signal();
            }
        }

        public CommandMvvm InsertAgreement { get; set; }

        public AgreementEditWindowMvvm()
        {
            InsertAgreement = new CommandMvvm(() =>
            {
                if (newAgreement.Id == 0)
                    AgreementDB.GetDB().Insert(NewAgreement);
                else
                {
                    AgreementDB.GetDB().Update(NewAgreement);
                }
                close?.Invoke();
            },
                () =>
                NewAgreement.TentantID != 0 &&
                NewAgreement.TPKZoneID != 0 &&
                NewAgreement.DateOfString != DateTime.Now &&
                NewAgreement.EndDate != DateTime.Now &&
                NewAgreement.RentalRate > 0 &&
                NewAgreement.Status != null);

        }
        public void SetZone(Agreement selectedAgreement)
        {
            NewAgreement = selectedAgreement;
        }

        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }
    }
}

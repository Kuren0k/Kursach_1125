using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kursach_1125.Model;

namespace Kursach_1125.VM
{
    internal class TentantEditWindowMvvm : BaseVM
    {
        private Tentant newTentant = new();

        public Tentant NewTentant
        {
            get => newTentant;
            set
            {
                newTentant = value;
                Signal();
            }
        }

        public CommandMvvm InsertTentant { get; set; }

        public TentantEditWindowMvvm()
        {
            
            InsertTentant = new CommandMvvm(() =>
        {
            if (newTentant.Id == 0)
                TentantDB.GetDB().Insert(NewTentant);
            else
            {
                TentantDB.GetDB().Update(NewTentant);
            }
            close?.Invoke();
        },
            () =>
            !string.IsNullOrEmpty(NewTentant.Title) &&
            !string.IsNullOrEmpty(NewTentant.ContactPerson) &&
            !string.IsNullOrEmpty(NewTentant.PhoneNumber) &&
            !string.IsNullOrEmpty(NewTentant.Email) &&
            NewTentant.RentalStartDate != DateTime.Now);

        }
        public void SetZone(Tentant selectedTentant)
        {
            NewTentant = selectedTentant;
        }

        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }
    }
}

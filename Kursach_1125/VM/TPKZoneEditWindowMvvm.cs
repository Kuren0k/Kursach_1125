using Kursach_1125.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Kursach_1125.VM
{
    internal class TPKZoneEditWindowMvvm : BaseVM
    {
        private TPKZone newZone = new();

        public TPKZone NewZone 
        { 
            get => newZone;
            set
            {
                newZone = value;
                Signal();
            }
        }

        public CommandMvvm InsertZone {  get; set; }

        public TPKZoneEditWindowMvvm()
        {
            InsertZone = new CommandMvvm(() =>
            {
                if (newZone.Id == 0)
                    TPKZoneDB.GetDB().Insert(NewZone);
                else
                {
                    TPKZoneDB.GetDB().Update(NewZone);
                }
                close?.Invoke();
            },
                () =>
                !string.IsNullOrEmpty(NewZone.Title) &&
                NewZone.Floor > 0 &&
                NewZone.Square > 0);

        }
        public void SetZone(TPKZone selectedZone)
        {
            NewZone = selectedZone;
        }

        Action close;
        internal void SetClose(Action close)
        {
            this.close = close;
        }
    }
}

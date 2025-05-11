using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_1125.Model
{
    public class Agreement
    {
        public int Id { get; set; }
        public int TentantID { get; set; }
        public int TPKZoneID { get; set; }
        public DateTime DateOfString { get; set; }
        public DateTime EndDate { get; set; }
        public int RentalRate { get; set; }
        public bool Status { get; set; }
        public Tentant Tentants { get; set; }
        public TPKZone TPKZones { get; set; }
    }
}

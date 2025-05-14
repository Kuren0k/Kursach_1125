using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_1125.Model
{
    public class Employee
    {
        public int Id { get; set; }
        public int JobTitleID { get; set; }
        public int TPKZoneID { get; set; }
        public string FIO { get; set; }
        public string PhoneNumber { get; set; }
        public TPKZone TPKZones { get; set; }
        public JobTitle JobTitles { get; set; }
    }
}

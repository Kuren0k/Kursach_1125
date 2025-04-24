using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_1125.Model
{
    public class JobTitle
    {
        public int Id { get; set; }
        public int TaskJobId { get; set; }
        public string Title { get; set; }
        public int Wages { get; set; }
    }
}

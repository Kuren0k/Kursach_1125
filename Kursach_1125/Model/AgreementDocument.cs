using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_1125.Model
{
    public class AgreementDocument
    {
        public int Id { get; set; }
        public int AgreementId { get; set; }
        public string FilePath { get; set; }
        public DateTime CreateDate { get; set; }
        public Agreement Agreements { get; set; }
    }
}

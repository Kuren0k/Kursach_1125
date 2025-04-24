using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kursach_1125.Model
{
    public class Payment
    {
        public int Id { get; set; }
        public int AgreementId { get; set; }
        public int Sum { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool Status { get; set; }
    }
}

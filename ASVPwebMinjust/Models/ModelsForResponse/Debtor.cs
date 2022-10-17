using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASVPwebMinjust.Models.ModelsForResponse
{
    public class Debtor
    {
        public object debtorOfVdID { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public DateTime? birthDate { get; set; }
        public string personSubType { get; set; }
        public string personSubTypeString { get; set; }
    }
}

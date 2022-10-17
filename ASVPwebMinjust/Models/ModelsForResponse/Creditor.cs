using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASVPwebMinjust.Models.ModelsForResponse
{
    public class Creditor
    {
        public object creditorOfVdID { get; set; }
        public string name { get; set; }
        public string? edrpou { get; set; }
        public string personSubType { get; set; }
        public string personSubTypeString { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASVPwebMinjust.Models.ModelsForPostRequest
{
    public class Filter
    {
        public string VPNum { get; set; }
        public object? vpOpenFrom { get; set; }
        public object? vpOpenTo { get; set; }
        public DebtFilter debtFilter { get; set; }
        public CreditFilter creditFilter { get; set; }
    }
}

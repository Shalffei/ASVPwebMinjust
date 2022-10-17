using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASVPwebMinjust.Models.ModelsForResponse
{
    public class DepAccount
    {
        public string bankName { get; set; }
        public string iban { get; set; }
        public bool isOnlinePayment { get; set; }
    }
}

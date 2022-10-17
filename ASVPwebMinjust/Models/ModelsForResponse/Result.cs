using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASVPwebMinjust.Models.ModelsForResponse
{
    public class Result
    {
        public object vdID { get; set; }
        public string orderNum { get; set; }
        public string mi_wfStateWithError { get; set; }
        public int depID { get; set; }
        public string depStr { get; set; }
        public DateTime? beginDate { get; set; }
        public string? depEdrpou { get; set; }
        public string? depEmail { get; set; }
        public string? depPhone { get; set; }
        public List<DepAccount> depAccounts { get; set; }
        public List<Debtor> debtors { get; set; }
        public List<Creditor> creditors { get; set; }
        public bool hasContacts { get; set; }
    }
}

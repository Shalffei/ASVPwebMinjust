using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASVPwebMinjust.Models.ModelsForResponse
{
    public class RootResponse
    {
        public bool isSuccess { get; set; }
        public List<Result> results { get; set; }
        public DateTime requestDate { get; set; }
        public bool isOverflow { get; set; }
    }
}

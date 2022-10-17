using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASVPwebMinjust.Models.ModelsForPostRequest
{
    public class Root
    {
        public string searchType  { get; set; }
        public Filter filter { get; set; }
        public string reCaptchaToken { get; set; }
        public string reCaptchaAction { get; set; }
    }
}

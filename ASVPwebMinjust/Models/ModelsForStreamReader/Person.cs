using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASVPwebMinjust.Models.ModelsForStreamReader
{
    public class Person
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int Ipn5First { get; set; }
    }
}

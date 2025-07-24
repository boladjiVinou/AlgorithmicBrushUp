using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter4
{
    public record PhoneData
    {
        public int id { get; set; }
        public int price { get; set; }
    }
    public record PhoneCheck 
    {
        public int phoneId { get; set; }
        public int amount { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace skiena.Chapter4
{
    public record BookMetadata
    {
        public string title { get; set; }
        public string author { get; set; }
        public string callNumber { get; set; }
        public string publisher { get; set; }
    }
}

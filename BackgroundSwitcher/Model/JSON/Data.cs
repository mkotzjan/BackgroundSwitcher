using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackgroundSwitcher.Model
{
    class Data
    {
        public string modhash { get; set; }
        public List<Child> children { get; set; }
        public object after { get; set; }
        public object before { get; set; }
    }
}

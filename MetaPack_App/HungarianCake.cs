using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPack_App
{
    class HungarianCake : Item
    {
        public HungarianCake()
        {
            price = 27;
        }
        public override string ToString()
        {
            return "Placek po węgiersku - " + price + "zł";
        }
    }
}

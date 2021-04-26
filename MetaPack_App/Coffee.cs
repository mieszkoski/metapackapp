using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPack_App
{
    class Coffee : Item
    {
        public Coffee()
        {
            price = 5;
        }
        public override string ToString()
        {
            return "Kawa - " + price + "zł";
        }
    }
}

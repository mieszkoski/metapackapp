using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPack_App
{
    class Cola : Item
    {
        public Cola()
        {
            price = 5;
        }
        public override string ToString()
        {
            return "Cola - " + price + "zł";
        }
    }
}

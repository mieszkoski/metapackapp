using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPack_App
{
    class ChickenSoup : Item
    {
        public ChickenSoup()
        {
            price = 10;
        }
        public override string ToString()
        {
            return "Rosół - " + price + "zł";
        }
    }
}

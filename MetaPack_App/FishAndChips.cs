using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPack_App
{
    class FishAndChips : Item
    {
        public FishAndChips()
        {
            price = 28;
        }
        public override string ToString()
        {
            return "Ryba z frytkami - " + price + "zł";
        }
    }
}

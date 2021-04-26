using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaPack_App
{
    class Margheritta : Pizza
    {
        public Margheritta(bool cheese = false, bool salami = false, bool ham = false, bool mushroom = false)
        {
            extraCheese = cheese;
            extraSalami = salami;
            extraHam = ham;
            extraMushroom = mushroom;
            price = 20;
            if (extraCheese) price += 2;
            if (extraSalami) price += 2;
            if (extraHam) price += 2;
            if (extraMushroom) price += 2;
        }
        public override string ToString()
        {
            string textExtra = "";
            if (extraCheese) textExtra += " + podwójny ser";
            if (extraSalami) textExtra += " + extra salami";
            if (extraHam) textExtra += " + extra szynka";
            if (extraMushroom) textExtra += " + extra pieczarki";
            return "Margheritta" + textExtra + " - " + price + "zł";
        }
    }
}

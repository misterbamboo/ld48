using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Ressources
{
    interface IRessource
    {
        string Name { get; }

        int SellPrice { get; }

        int BuyPrice { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Ressources
{
    public interface IRessource
    {
        string Name { get; }

        int SellPrice { get; }

        int BuyPrice { get; }
        int SpawnX { get; set; }
        int SpawnY { get; set; }

        bool IsConsume();

        void Consume(bool value);
    }
}

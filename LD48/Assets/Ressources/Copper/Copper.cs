using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Ressources
{
    public class Copper : MonoBehaviour, IRessource
    {
        public string Name => nameof(Copper);

        [SerializeField] private int sellPrice = 10;
        public int SellPrice => sellPrice;

        [SerializeField] private int buyPrice = 15;
        public int BuyPrice => buyPrice;

        public bool IsConsume()
        {
            return !enabled;
        }

        public void Consume(bool value)
        {
            enabled = !value;
            gameObject.SetActive(!value);
        }
    }
}

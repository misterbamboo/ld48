using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public interface ISubmarine
    {
        int Deepness { get; }
    }

    public class Submarine : MonoBehaviour, ISubmarine, IShopCustomer
    {

        public static ISubmarine Instance { get; private set; }

        public int Deepness => (int)(-Mathf.Clamp(transform.position.y, float.MinValue, 0));

        private Rigidbody rb;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            //transform.position = new Vector3(rb.velocity.x, rb.velocity.y, 0);
        }

        public void BoughtUpgrade(Upgrade.UpgradeType upgradeType)
        {
          //  print("asdfasdf" + upgradeType);
            
            Debug.Log("Bought item : " + upgradeType);
        }
    }
}

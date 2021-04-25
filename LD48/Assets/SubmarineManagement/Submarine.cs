using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Submarine
{
    public interface ISubmarine
    {
        int Deepness { get; }
    }

    public class Submarine : MonoBehaviour, ISubmarine
    {
        public static ISubmarine Instance { get; private set; }

        public int Deepness => (int)(-Mathf.Clamp(transform.position.y, float.MinValue, 0));

        private void Awake()
        {
            Instance = this;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    // Start is called before the first frame update
    Light myLight;

    void Start()
    {
        myLight = GetComponent<Light>();
    }

    void Update()
    {
        
        if (Submarine.Instance.LightUpgradeBought)
        {
            myLight.range += 5;
            Submarine.Instance.LightUpgradeBought = false;
        }

        
    }
}

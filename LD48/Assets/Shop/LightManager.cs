using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;


public class LightManager : MonoBehaviour
{
    UnityEngine.Rendering.Universal.Light2D myLight;

    void Start()
    {
        myLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
    }

    void Update()
    {
        
        if (Submarine.Instance.LightUpgradeBought)
        {
            myLight.pointLightInnerRadius += myLight.pointLightInnerRadius;
            myLight.pointLightOuterRadius += myLight.pointLightOuterRadius;
            Submarine.Instance.LightUpgradeBought = false;
        }

        AdjustLight();
    }

    private void AdjustLight()
    {
        if (Game.Instance.GameOver)
        {
            myLight.intensity = 0.0f;
            return;
        }

        if (Submarine.Instance.Deepness <= 200)
        {
            myLight.intensity = 0.00f;
        }
        else if (Submarine.Instance.Deepness <= 300)
        {
            myLight.intensity = 0.50f;
        }
        else
        {
            myLight.intensity = 0.75f;
        }
    }
}

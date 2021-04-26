using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class DeepnessLighting : MonoBehaviour
{
    [SerializeField]
    Light2D globalLight;

    [SerializeField]
    Light2D pointLight;

    public static Color whiteLight = HexToColor("#ffffff");
    public static Color surfaceLight = HexToColor("#96fff5");
    public static Color lowInWater = HexToColor("#7ed1de");
    public static Color mildInWater = HexToColor("#4e82c7");
    public static Color farInWater = HexToColor("#2b2d8f");
    public static Color deepInWater = HexToColor("#251769");
    public static Color reallyDeepInWater = HexToColor("#130a3d");
    public static Color sooooooDeepInWater = HexToColor("#140430");
    public static Color deeperAndDeaperInWater = HexToColor("#070014");

    public static Dictionary<int, Color> waterColorByDeepness = new Dictionary<int, Color>()
    {
        { 0, surfaceLight },
        { 50, lowInWater },
        { 100, mildInWater },
        { 150, farInWater },
        { 200, deepInWater },
        { 250, reallyDeepInWater },
        { 300, sooooooDeepInWater },
        { 350, deeperAndDeaperInWater },
    };

    public static Dictionary<int, Color> submarineColorByDeepness = new Dictionary<int, Color>()
    {
        { 0, whiteLight },
        { 50, surfaceLight },
        { 100, lowInWater },
        { 150, mildInWater },
        { 200, farInWater },
        { 250, deepInWater },
        { 300, HexToColor("#3a298c") },
    };

    public static Color HexToColor(string hexString)
    {
        //replace # occurences
        if (hexString.IndexOf('#') != -1)
            hexString = hexString.Replace("#", "");

        float r, g, b = 0f;

        r = int.Parse(hexString.Substring(0, 2), NumberStyles.AllowHexSpecifier);
        g = int.Parse(hexString.Substring(2, 2), NumberStyles.AllowHexSpecifier);
        b = int.Parse(hexString.Substring(4, 2), NumberStyles.AllowHexSpecifier);

        return new Color(r / 255f, g / 255f, b / 255f);
    }

    [SerializeField] private Material subarineMaterial;
    private int lastDeepness = 0;

    void Start()
    {
        /*
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
        RenderSettings.ambientSkyColor = Color.white;
        RenderSettings.ambientEquatorColor = Color.white;
        RenderSettings.ambientGroundColor = Color.white;
        RenderSettings.ambientIntensity = 1;      
        Camera.main.backgroundColor = surfaceLight;
        if (subarineMaterial != null)
        {
            subarineMaterial.color = whiteLight;
        }
        */
    }

    void Update()
    {        
        /*
        var deepness = Submarine.Instance.Deepness;
        if (lastDeepness != deepness)
        {
            DeepnessChanged(deepness);
            lastDeepness = deepness;
        }
        */
    }

    private void DeepnessChanged(int deepness)
    {
        var color = GetColorForDeepness(waterColorByDeepness, deepness);
        RenderSettings.ambientSkyColor = color;
        RenderSettings.ambientEquatorColor = color;
        RenderSettings.ambientGroundColor = color;
        Camera.main.backgroundColor = color;

        //globalLight.color = color;
        //pointLight.color = color;

        if (subarineMaterial != null)
        {
            subarineMaterial.color = GetColorForDeepness(submarineColorByDeepness, deepness);
        }
    }

    private static Color GetColorForDeepness(Dictionary<int, Color> deepnessColors, int deepness)
    {
        int previousDeepnessStage, nextDeepnessStage;
        GetPreviousAndNextDeepness(deepnessColors, deepness, out previousDeepnessStage, out nextDeepnessStage);
        float progressionInStage = GetProgression(deepness, previousDeepnessStage, nextDeepnessStage);
        Color currentColor = FindTransitionColor(deepnessColors, previousDeepnessStage, nextDeepnessStage, progressionInStage);
        return currentColor;
    }

    private static void GetPreviousAndNextDeepness(Dictionary<int, Color> deepnessColors, int deepness, out int previousDeepnessStage, out int nextDeepnessStage)
    {
        previousDeepnessStage = 0;
        nextDeepnessStage = deepnessColors.Keys.Max();
        foreach (var key in deepnessColors.Keys.OrderBy(k => k))
        {
            if (deepness < key)
            {
                nextDeepnessStage = key;
                break;
            }
            previousDeepnessStage = key;
        }
    }

    private static float GetProgression(int deepness, int previousDeepnessStage, int nextDeepnessStage)
    {
        float sinceLastStage = (deepness - previousDeepnessStage);
        float stageLength = (nextDeepnessStage - previousDeepnessStage);

        float progressionInStage = 1;
        if (stageLength > 0)
        {
            progressionInStage = sinceLastStage / stageLength;
        }

        return progressionInStage;
    }

    private static Color FindTransitionColor(Dictionary<int, Color> deepnessColors, int previousDeepnessStage, int nextDeepnessStage, float progressionInStage)
    {
        Color previousStageColor = deepnessColors[previousDeepnessStage];
        Color nextStageColor = deepnessColors[nextDeepnessStage];
        var currentColor = Color.Lerp(previousStageColor, nextStageColor, progressionInStage);
        return currentColor;
    }
}

using Assets;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class MusicDeepRange
{
    public MusicName musicName;

    public int minValue;

    public int maxValue;
}

public class DeepnessMusic : MonoBehaviour
{
    [SerializeField]
    List<MusicDeepRange> musics;

    void Start()
    {
        DeepnessChanged(Submarine.Instance.Deepness);
    }

    void FixedUpdate()
    {
        DeepnessChanged(Submarine.Instance.Deepness);
    }

    private void DeepnessChanged(int deepness)
    { 
        var music = musics.FirstOrDefault(m => DeepnessIsInRange(m, deepness));
        if (music != null)
        {
            AudioManager.Instance.PlayMusic(music.musicName);
        }
    }

    public bool DeepnessIsInRange(MusicDeepRange music, int deepness)
    {
        return music.minValue <= deepness && deepness < music.maxValue;
    }
}

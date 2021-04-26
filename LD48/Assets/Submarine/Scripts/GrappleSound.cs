using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSound : MonoBehaviour
{
    [SerializeField]
    List<SoundName> soundNames;

    public void PlayRandomSound()
    {
        var soundPos = Random.Range(0, soundNames.Count);
        var soundName = soundNames[soundPos];
        AudioManager.Instance.PlaySound(soundName);
    }
}

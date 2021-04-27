using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDeepSong : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic(MusicName.deep);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

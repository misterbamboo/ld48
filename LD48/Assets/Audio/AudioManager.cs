using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    [SerializeField]
    Sound[] sounds;

    [SerializeField]
    Music[] musics;

    AudioSource musicSource;

    MusicName currentMusic;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        UnityEngine.Random.InitState(DateTime.Now.Millisecond);

        musicSource = gameObject.AddComponent<AudioSource>();
        PlayMusic(currentMusic);

        foreach (var s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void PlayMusic(MusicName name)
    {
        if (currentMusic == name)
        {
            return;
        }

        if (name == MusicName.none)
        {
            musicSource.Stop();
            return;
        }

        Music music = musics.First(m => m.name == name);
        SetMusicSettings(music);
        musicSource.Play();

        currentMusic = music.name;
    }

    private void SetMusicSettings(Music music)
    {
        musicSource.clip = music.clip;
        musicSource.volume = music.volume;
        musicSource.pitch = music.pitch;
    }

    public void PlaySound(SoundName name)
    {
        Sound sound = sounds.First(s => s.name == name);
        sound.source.Play();
    }
}

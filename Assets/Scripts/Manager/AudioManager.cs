using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject("AudioManager");
                    instance = go.AddComponent<AudioManager>();
                }
            }
            return instance;
        }
    }

    private List<AudioSource> audioSources = new List<AudioSource>();
    public AudioListener listener;
    private bool isMuted = false;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        RefreshAudioSources();
    }

    public void RefreshAudioSources()
    {
        audioSources.Clear();
        AudioSource[] sources = FindObjectsOfType<AudioSource>();
        audioSources.AddRange(sources);
    }

    public void MuteAll()
    {
        isMuted = true;
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
            {
                source.mute = true;
            }
        }
        listener.enabled = false;
    }

    public void UnmuteAll()
    {
        isMuted = false;
        foreach (AudioSource source in audioSources)
        {
            if (source != null)
            {
                source.mute = false;
            }
        }
        listener.enabled = true;
    }

    public void ToggleMute()
    {
        if (isMuted)
        {
            UnmuteAll();
        }
        else
        {
            MuteAll();
        }
    }

    public void AddAudioSource(AudioSource source)
    {
        if (!audioSources.Contains(source))
        {
            audioSources.Add(source);
            source.mute = isMuted;
        }
    }

    public void RemoveAudioSource(AudioSource source)
    {
        if (audioSources.Contains(source))
        {
            audioSources.Remove(source);
        }
    }

    public bool IsMuted()
    {
        return isMuted;
    }
} 
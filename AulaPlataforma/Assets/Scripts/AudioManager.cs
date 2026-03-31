using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource systemSource;
    private List<AudioManager> activeSoucers;

    #region Singleton
    public static AudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            systemSource = GetComponent<AudioSource>();
            activeSoucers = new List<AudioManager>();
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    #endregion

    #region AudioControls

    public void Play(AudioClip clip)
    {
        systemSource.Stop();
        systemSource.clip = clip;
        systemSource.Play();
    }

    public void PlayOneShot(AudioClip clip)
    {
        systemSource.PlayOneShot(clip);
    }

    #endregion
    
    public void Play(AudioClip clip, AudioSource source)
    {
        if(!activeSoucers.Contains(source))
            activeSoucers.Add(source);
        source.Stop();
        source.clip = clip;
        source.Play();
    }

    public void Stop (AudioClip clip)
    {
        if(activeSoucers.Contains(source)
           source.Stop();
        activeSoucercs(clip);
    }
}
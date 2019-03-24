using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour
{
    private AudioManager() { }
    public static AudioManager Instance { get; private set; }

    public string ResourceDir = "Audio";
    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
        audioSource.playOnAwake = true;
        BSound = true;
    }

    public bool BSound
    { get; set; }

    #region BGM
    public bool BMusic
    {
        get { return !audioSource.mute; }
        set
        {
            audioSource.mute = !value;
        }
    }
 

    public float BGMVolume //0-1
    {
        get { return audioSource.volume; }
        set { audioSource.volume = value; }
    }

    public void PlayBGM(string name)
    {
        string path = "Audio/" +name;
        AudioClip ac = Resources.Load<AudioClip>(path);
        audioSource.clip = ac;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.clip = null;
        audioSource.Stop();

    }
    #endregion

    //音效
    public void PlaySound(string name)
    {
        string path = ResourceDir + "/" + name;
        AudioClip ac = Resources.Load<AudioClip>(path);
        AudioSource.PlayClipAtPoint(ac, Vector2.zero);
    }

}

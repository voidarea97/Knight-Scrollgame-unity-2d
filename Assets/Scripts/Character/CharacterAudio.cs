using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CharacterAudio : MonoBehaviour {

    public AudioSource audioSource;
    public Dictionary<string,AudioClip> audioClipDic;
    string charactorName;

    private void Awake()
    {
        audioClipDic = new Dictionary<string, AudioClip>();
    }
    private void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
        if(GetComponent<Character>()!=null)
        {
            charactorName = GetComponent<Character>().CharacterName;
            LoadAllAudio();
        }
	}

	public void PlayAudio(string name)
    {
        if (audioClipDic.ContainsKey(name))
        {
            audioSource.clip = audioClipDic[name];
            audioSource.Play();
        }
    }

    public void LoadAllAudio()
    {
        string path;
        path = Application.dataPath + "/Resources/Audio/" + charactorName;
        DirectoryInfo folder = new DirectoryInfo(path);
        foreach (FileInfo file in folder.GetFiles("*.*"))
        {
            int index = file.Name.LastIndexOf(".");
            string audioName = file.Name.Substring(0, index);
            string audioPath = charactorName + "/" + audioName;
            AudioClip audioClip = Resources.Load<AudioClip>("Audio/"+audioPath);
            audioClipDic.Add(audioName, audioClip);
        }
    }

}

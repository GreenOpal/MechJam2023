using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioController : SingletonMonoBehaviour<AudioController>
{
    [SerializeField] private AudioItem[] AllItems;
    public enum AudioKeys
    {
        //Please don't reorder this list! add new items to the bottom
        SFX_UI_Back,
        SFX_UI_Confirm,
        SFX_UI_Select,
        SFX_UI_Move,
        SFX_Attack_Stabby,
        SFX_Attack_Shooty,
        SFX_Attack_Smashy,
        SFX_Ambience_Cheer,
        SFX_Ambience_Boo,
        SFX_Ambience_Laugh,
        Music_Title,
        Music_Scavenge,
        Music_Battle,
        Music_Boss

    }
    [System.Serializable]
    public struct AudioItem
    {
        public AudioKeys key;
        public AudioClip clip;
        [Range(0, 1)] public float volume;
    }

    private AudioSource currentMusicSource;
    private Dictionary<AudioKeys, AudioSource> AllSources;

    private void Start()
    {
        AllSources = new Dictionary<AudioKeys, AudioSource>();

        foreach (var item in AllItems)
        {
            var newSource = new GameObject($"Source_{item.key}", typeof(AudioSource)).GetComponent<AudioSource>();
            newSource.transform.SetParent(this.transform);
            newSource.playOnAwake = false;
            newSource.clip = item.clip;
            newSource.volume = item.volume;
            AllSources.Add(item.key, newSource);
        }
    }

    public void PlaySFX(AudioKeys key)
    {
        AllSources[key].Play();
    }

    public void PlayMusic(AudioKeys key)
    {
        if (currentMusicSource)
        {
            currentMusicSource.Stop();
        }
        AllSources[key].Play();
        AllSources[key].loop = true;
        currentMusicSource = AllSources[key];
    }

    public void Stop(AudioKeys key)
    {
        AllSources[key].Stop();
    }
}

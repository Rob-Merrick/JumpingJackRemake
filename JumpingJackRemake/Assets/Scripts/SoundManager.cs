using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Manager<SoundManager>
{
    private IDictionary<string, AudioSource> _soundEffectsLookup = new Dictionary<string, AudioSource>();

    private void Start()
    {
        AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();

        foreach(AudioSource audioSource in audioSources)
		{
            _soundEffectsLookup.Add(audioSource.gameObject.name, audioSource);
		}
    }

    public AudioSource GetAudioSourceByName(string name)
	{
        return _soundEffectsLookup[name];
	}
}

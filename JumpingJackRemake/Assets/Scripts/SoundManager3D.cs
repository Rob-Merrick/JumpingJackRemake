using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager3D : Manager<SoundManager3D>
{
	private readonly IDictionary<string, AudioSource> _soundsLookup = new Dictionary<string, AudioSource>();

	private void Start()
	{
		AudioSource[] audioSources = GetComponentsInChildren<AudioSource>();

		for(int i = 0; i < audioSources.Length; i++)
		{
			AudioSource audioSource = audioSources[i];
			_soundsLookup.Add(audioSource.name, audioSource);
		}
	}

	public void PlaySound(string soundName)
	{
		_soundsLookup[soundName].Play();
	}

	public void PlaySoundWithAudioSource(string soundName, AudioSource audioSource)
	{
		AudioClip audioClip = _soundsLookup[soundName].clip;
		audioSource.clip = audioClip;
		audioSource.Play();
	}
}

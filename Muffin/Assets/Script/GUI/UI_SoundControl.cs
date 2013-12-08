using UnityEngine;
using System.Collections;


[RequireComponent (typeof(AudioSource))]
public class UI_SoundControl : MonoBehaviour {
	public AudioClip sound;
	public bool playOnStart;

	void Start()
	{
		if (playOnStart)
			PlayTheSound();
	}

	void PlayTheSound()
	{
		audio.PlayOneShot(sound);
	}
}

using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance;

	public AudioClip[] sound;

	public enum SoundType
	{
		SetCheese,
		SetPoison,
		PoisonBreak,
		MouseEating,
		MouseDie,
		MuffinDie
	}
	
	void Awake()
	{
		Instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PlaySound(SoundType t)
	{
		int i = (int)t;
		audio.PlayOneShot(sound[i]);
	}
}

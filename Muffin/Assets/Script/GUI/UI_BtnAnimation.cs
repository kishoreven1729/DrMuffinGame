using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class UI_BtnAnimation : MonoBehaviour {

	public float delay;
	public string animationName;
	public AudioClip sound;
	// Use this for initialization
	void Start () {
		StartCoroutine("PlayMoveIn");
	}

	IEnumerator PlayMoveIn ()
	{
		yield return new WaitForSeconds(delay);
		animation.Play(animationName);
		audio.PlayOneShot(sound);
	}

	// Update is called once per frame
	void Update () {
	
	}
}

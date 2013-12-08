using UnityEngine;
using System.Collections;

public class MovieControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
		/*MovieTexture m = renderer.material.mainTexture as MovieTexture;
		m.Play ();*/
		Handheld.PlayFullScreenMovie ("Muffin Morphosis_new.mov", Color.black, FullScreenMovieControlMode.CancelOnInput);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

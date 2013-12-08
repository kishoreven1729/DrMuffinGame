using UnityEngine;
using System.Collections;

public class GoNextSceneControl : MonoBehaviour {


	public int t;
	// Use this for initialization
	void Start () {
		StartCoroutine("Go");
	}

	IEnumerator Go()
	{
		yield return new WaitForSeconds(t);
		Application.LoadLevel(0);
	}
	// Update is called once per frame
	void Update () {
	
	}
}

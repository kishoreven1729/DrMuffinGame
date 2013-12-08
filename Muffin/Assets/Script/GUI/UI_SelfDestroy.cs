using UnityEngine;
using System.Collections;

public class UI_SelfDestroy : MonoBehaviour {

	public float lifespan;
	// Use this for initialization
	void Start () {
		Destroy(gameObject, lifespan);
	}
}

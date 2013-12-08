using UnityEngine;
using System.Collections;

public class UI_BtnMovement : MonoBehaviour {
	
	public Vector3 from;
	public Vector3 to;
	public float duration;
	private float ratio;
	public float timer;
	private bool isMoving;


	void Start()
	{
		ratio = 0f;
		timer = 0f;
		isMoving = true;
	}

	void Update()
	{
		if (isMoving)
		{
			timer += Time.deltaTime;
			ratio = timer / duration;
			transform.localPosition = Vector3.Lerp(from, to, ratio);
//			if (ratio >= 1f)
//				isMoving = true;
		}
	}

}

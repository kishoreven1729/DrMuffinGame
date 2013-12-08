using UnityEngine;
using System.Collections;

public class UI_3dCameraControl : MonoBehaviour {

	public Transform from;
	public Transform to;

	public float duration;

	void Focus(bool b)
	{
		StartCoroutine("_Focus", b);
	}

	IEnumerator _Focus(bool b)
	{
		float timer = 0f;
		bool isZooming = true;
		float ratio = 0f;
		
		while (isZooming)
		{
			timer += Time.deltaTime;
			ratio = timer / duration;

			if (b)
			{
				transform.position = Vector3.Slerp(from.position, to.position, ratio);
				transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, ratio);
				yield return null;
				if (ratio >= 1f)
					isZooming = false;
			}
			else
			{
				transform.position = Vector3.Slerp(from.position, to.position, 1 - ratio);
				transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, 1 - ratio);
				yield return null;
				if (ratio >= 1f)
					isZooming = false;
			}
		}
	}
}

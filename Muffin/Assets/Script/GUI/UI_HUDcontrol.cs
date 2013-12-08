using UnityEngine;
using System.Collections;

public class UI_HUDcontrol : MonoBehaviour {
	
	public Transform[] prefab;
	
	
	public void CreateNumber(int i)
	{
		Transform t = Instantiate(prefab[i], transform.position, transform.rotation) as Transform;
		t.parent = transform;
	}
	
	public IEnumerator BackCount()
	{
		for (int i = 0; i < prefab.Length; i++)
		{
			StartCoroutine("CreateNumber", i);
			yield return new WaitForSeconds(1f);
		}
		
		GameDirector.gameInstance.SwitchState(States.InGame);
	}
}

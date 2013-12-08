#region References
using UnityEngine;
using System.Collections;
#endregion

public class Food : MonoBehaviour 
{
	#region Private Variables
	#endregion

	#region Public Variables
	public float foodVanishTime;
	public bool istriggered;
	#endregion

	#region Constructor
	void Start() 
	{
		istriggered = false;
	}
	#endregion
	
	#region Loop
	void Update () 
	{

	}

	/*void OnTriggerEnter (Collider hit)
	{
		if(hit.gameObject.tag == "Enemy")
		{
			SoundManager.Instance.PlaySound(SoundManager.SoundType.MouseEating);

			StartCoroutine(destroyFood(foodVanishTime));
		}

	}*/

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Enemy")
		{
			SoundManager.Instance.PlaySound(SoundManager.SoundType.MouseEating);
			
			StartCoroutine(destroyFood(foodVanishTime));
		}
	}
	#endregion

	#region Methods
	IEnumerator destroyFood(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);

		MainCharLogic.Instance.foodNo += 1;

		GameDirector.gameInstance.RemoveFood(gameObject.name);
	}
	#endregion
}

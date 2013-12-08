#region References
using UnityEngine;
using System.Collections;
#endregion

public class Traps : MonoBehaviour 
{
	#region Private Variables
	private int _killCount;
	private int _totalLevel;
	#endregion

	#region Public Variables
	public float startTime;
	public float age; 
	public float vanishTime;
	public float explodeRadius;
	public bool isTriggered;
	#endregion


	#region Constructor
	void Start() 
	{
		_killCount = 0;
		_totalLevel = 0;

		startTime = Time.time;
	}
	#endregion
	
	#region Loop
	void Update() 
	{
		age = Time.time - startTime;
	}

	/*void OnTriggerEnter(Collider hit)
	{
		if (age >= 1) 
		{
			if(hit.gameObject.tag == "Enemy")
			{
				_killCount++;

				_totalLevel += hit.gameObject.GetComponent<EnemyScript>().enemyLevel;

				SoundManager.Instance.PlaySound(SoundManager.SoundType.PoisonBreak);

				GameDirector.gameInstance.RemoveEnemy(hit.gameObject.name);
			}


			if (hit.gameObject.tag == "Enemy" & isTriggered == false) //only do the corotin once
			{
				SoundManager.Instance.PlaySound(SoundManager.SoundType.MouseDie);

				StartCoroutine(destroyTrap(vanishTime));

			}

			if(hit.gameObject.tag == "Player" & isTriggered == false)
			{
				SoundManager.Instance.PlaySound(SoundManager.SoundType.MuffinDie);

				StartCoroutine(destroyTrap(vanishTime));
			}
		}
	}*/

	void OnCollisionEnter(Collision collision)
	{
		if (age >= 1) 
		{
			if(collision.gameObject.tag == "Enemy")
			{
				_killCount++;
				
				_totalLevel += collision.gameObject.GetComponent<EnemyScript>().enemyLevel;
				
				SoundManager.Instance.PlaySound(SoundManager.SoundType.PoisonBreak);
				
				GameDirector.gameInstance.RemoveEnemy(collision.gameObject.name);
			}
			
			
			if (collision.gameObject.tag == "Enemy" & isTriggered == false) //only do the corotin once
			{
				SoundManager.Instance.PlaySound(SoundManager.SoundType.MouseDie);
				
				StartCoroutine(DestroyTrap(vanishTime));
				
			}
			
			if(collision.gameObject.tag == "Player" & isTriggered == false)
			{
				SoundManager.Instance.PlaySound(SoundManager.SoundType.MuffinDie);
				
				StartCoroutine(DestroyTrap(vanishTime));
			}
		}
	}
	#endregion

	#region Methods
	IEnumerator DestroyTrap(float waitTime)
	{
		CapsuleCollider trapCollider = collider as CapsuleCollider;

		if (trapCollider != null) 
		{
			trapCollider.radius = explodeRadius;	
			isTriggered=true;
		}

		yield return new WaitForSeconds(waitTime);

		MainCharLogic.Instance.trapNo+=1;

		GameDirector.gameInstance.RemoveTrap(gameObject.name,_killCount, _totalLevel);
	}
	#endregion

}

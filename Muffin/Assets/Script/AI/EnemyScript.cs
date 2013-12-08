#region References
using UnityEngine;
using System.Collections;
#endregion

public class EnemyScript : MonoBehaviour 
{
	#region Private Variables
	private int 			_foodCollected;
	private int 			_foodForUpgrade;
	private int				_maxEnemyLevel;

	private Vector3 		_enemyPosition;

	private Transform		_character;
	#endregion

	#region Public Variables
	public int 				enemyLevel;
	public Vector3 			enemyDirection;
	public float			enemySpeed;
	public Rect				enemyBoundary;

	public Rect				characterBoundary;
	#endregion

	#region Constructor
	void Start() 
	{
		enemyLevel 			= 0;

		_foodCollected 		= 0;

		_foodForUpgrade 	= 1;

		_maxEnemyLevel		= 4;

		_enemyPosition 		= new Vector3(0, 0, 0);

		try
		{
			_character			= GameObject.Find("Character").transform;
		}
		catch(System.Exception ex)
		{
			Debug.Log(ex.Message);
			_character 			= null;
		}

		//SpawnEnemy();
	}
	#endregion

	#region Loop
	void Update() 
	{
		enemyDirection = FindDirection();

		enemyDirection.Normalize();

		enemyDirection = enemyDirection * enemySpeed;

		rigidbody.velocity = enemyDirection;

		//transform.LookAt (enemyDirection);
	}

	/*void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "Food")
		{
			_foodCollected ++;

			UpgradeEnemy();
		}

		if(collider.gameObject.tag == "Trap")
		{

		}

		if(collider.gameObject.tag == "Player")
		{

		}
	}*/

	void OnCollisionEnter(Collision collision)
	{
		if(collision.gameObject.tag == "Food")
		{
			_foodCollected++;

			UpgradeEnemy();
		}
	}
	#endregion

	#region Methods
	Vector3 FindDirection()
	{
		Vector3 enemyMovementDirection = Vector3.zero;

		if(_character == null)
		{
			return enemyMovementDirection;
		}

		Vector3 characterDirection = (_character.localPosition - transform.localPosition);

		float distance = characterDirection.magnitude;

		enemyMovementDirection = characterDirection;

		foreach(Transform foodTransform in GameDirector.gameInstance.FoodCollection.Values)
		{
			if(foodTransform != null)
			{
				Vector3 foodDirection = (foodTransform.localPosition - transform.localPosition);

				if(foodDirection.magnitude <= distance)
				{
					distance = foodDirection.magnitude;

					enemyMovementDirection = foodDirection;
				}
			}
		}

		return enemyMovementDirection;
	}

	void UpgradeEnemy()
	{
		if(_foodCollected >= _foodForUpgrade && enemyLevel < _maxEnemyLevel)
		{
			_foodCollected = 0;

			_foodForUpgrade = _foodForUpgrade * 2;

			enemySpeed = enemySpeed - (enemySpeed / (_maxEnemyLevel - enemyLevel));

			enemyLevel++;

			/* Level Up Code */
		}
	}
	#endregion
}

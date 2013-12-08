#region References
using UnityEngine;
using System.Collections;
#endregion

public class PowerupScript : MonoBehaviour 
{
	#region Private Variables
	private float			_expiryTime;

	private Vector3			_powerupVelocity;
	#endregion

	#region Public Variables
	public float			powerupLifespan;
	public string			powerupType;

	public float			maxPowerupVelocity;
	#endregion

	#region Constructor
	void Start() 
	{
		_expiryTime = Time.time + powerupLifespan;

		_powerupVelocity = Vector3.zero;

		GenerateRandomVelocity(-maxPowerupVelocity, maxPowerupVelocity);
	}
	#endregion

	#region Loop
	void Update() 
	{
		if(Time.time > _expiryTime)
		{
			/* Powerup Remove Animation */

			GameDirector.gameInstance.RemovePowerup(gameObject.name);
		}

		/* Powerup Movement */
		if(transform.position.x < GameDirector.gameInstance.gameArea.x)
		{
			GenerateRandomVelocity(0.0f, maxPowerupVelocity);
		}
		else if(transform.position.x > GameDirector.gameInstance.gameArea.xMax)
		{
			GenerateRandomVelocity(-maxPowerupVelocity, 0.0f);
		}

		if(transform.position.z < GameDirector.gameInstance.gameArea.y)
		{
			GenerateRandomVelocity(0.0f, maxPowerupVelocity);
		}
		else if(transform.position.z > GameDirector.gameInstance.gameArea.yMax)
		{
			GenerateRandomVelocity(-maxPowerupVelocity, 0.0f);
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			/*Powerup Remove Animation */

			GameDirector.gameInstance.RemovePowerup(gameObject.name);
		}
	}
	#endregion

	#region Methods
	void GenerateRandomVelocity(float min, float max)
	{
		_powerupVelocity.x = Random.Range(min, max);
		_powerupVelocity.z = Random.Range(min, max);


		transform.rigidbody.velocity = _powerupVelocity;
	}
	#endregion
}

using UnityEngine;
using System.Collections;

public class PowerupGeneratorScript : MonoBehaviour 
{
	#region Private Variables
	private bool			_canSpawnPowerup;
	private float			_nextPowerupSpawn;
	#endregion
	
	#region Public Variables
	public float			_powerupSpawnInterval;	

	public int				powerupGeneratorID;
	#endregion
	
	#region Constructor
	void Start() 
	{
		_canSpawnPowerup = true;

		_nextPowerupSpawn = Time.time + _powerupSpawnInterval * 2;
	}
	#endregion
	
	#region Loop
	void Update() 
	{
		GenerateRandomLocation();

		if(Time.time > _nextPowerupSpawn)
		{
			if(_canSpawnPowerup == true)
			{
				GameDirector.gameInstance.GeneratePowerup(powerupGeneratorID);
			}

			_nextPowerupSpawn = Time.time + _powerupSpawnInterval;
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.name == "Trap")
		{
			_canSpawnPowerup = false;

			GenerateRandomLocation();
		}
	}

	void OnTriggerExit(Collider collider)
	{
		if(collider.gameObject.name == "Trap")
		{
			_canSpawnPowerup = true;
		}
	}

	#endregion
	
	#region Methods
	void GenerateRandomLocation()
	{
		Rect gameScreen = GameDirector.gameInstance.gameArea;

		Vector3 spawnLocation = Vector3.zero;

		spawnLocation.x = Random.Range(gameScreen.x, gameScreen.xMax);
		spawnLocation.z = Random.Range(gameScreen.y, gameScreen.yMax);
		spawnLocation.y = transform.position.y;

		transform.position = spawnLocation;
	}
	#endregion
}

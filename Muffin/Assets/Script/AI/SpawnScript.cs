#region References
using UnityEngine;
using System.Collections;
#endregion

public class SpawnScript : MonoBehaviour 
{
	#region Private Variables
	private float	_nextSpawnTime;

	private int		_spawnProbability;
	#endregion

	#region Public Variables
	public float 	spawnTimeInterval;
	public int		spawnID;
	#endregion

	#region Constructor
	void Start() 
	{
		spawnTimeInterval = 5.0f;

		ResetSpawnProbability(3);

		_nextSpawnTime = 0.0f;
	}
	#endregion
	
	#region Loop
	void Update() 
	{
		if(Time.time >= _nextSpawnTime)
		{
			int rollDice = Random.Range(0, 6);
			
			if(rollDice >= _spawnProbability)
			{
				GameDirector.gameInstance.AddEnemy(transform.position, spawnID);
			}
			
			_nextSpawnTime = Time.time + spawnTimeInterval;
		}
	}

	void FixedUpdate()
	{

	}
	#endregion

	#region Methods
	public void SetSpawnTimeInterval(float time)
	{
		spawnTimeInterval = time;
	}

	public void ResetSpawnProbability(int maxDiceVal)
	{
		_spawnProbability = Random.Range(0, maxDiceVal + 1);
	}
	#endregion
}

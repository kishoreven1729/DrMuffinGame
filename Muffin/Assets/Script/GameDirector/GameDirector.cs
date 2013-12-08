#region References
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
#endregion

public class GameDirector : MonoBehaviour 
{
	#region Private Variables
	private Dictionary<string, Transform> 		_trapCollection;
	private Dictionary<string, Transform> 		_foodCollection;
	private Dictionary<string, Transform>		_powerupCollection;
	private Dictionary<string, Transform>		_enemyCollection;
	#endregion
	
	#region Public Variables
	public States								currentGameState;

	public static GameDirector 					gameInstance;
	
	public Rect 								gameArea;
	
	public Transform 							trapPrefab;
	public Transform							foodPrefab;
	public Transform[]							powerupPrefabs;
	
	public Transform							powerupGenerator;
	
	public Transform							enemyPrefab;
	
	public Transform							character;
	
	public Transform							spawnPoints;
	
	public int									currentScore;
	public int									timeFactor;
	public int									highScore;
	public int									survivalTime;

	public Transform							firePos;

	#endregion
	
	#region Private Storage Variables
	private Transform							_initialPowerupGenerator;
	private Vector3								_initialCharacterPosition;
	private Quaternion							_initialCharacterRotation;
	#endregion
	
	#region Properties
	public Dictionary<string, Transform> TrapCollection
	{
		get
		{
			return _trapCollection;
		}
	}
	
	public Dictionary<string, Transform> FoodCollection
	{
		get
		{
			return _foodCollection;
		}
	}
	
	public Dictionary<string, Transform> PowerupCollection
	{
		get
		{
			return _powerupCollection;
		}
	}
	
	public Dictionary<string, Transform> EnemyCollection
	{
		get
		{
			return _enemyCollection;
		}
	}
	#endregion
	
	#region Constructor
	void Awake()
	{
		gameInstance = this;
	}
	
	void Start() 
	{
		_trapCollection 		= new Dictionary<string, Transform>();
		_foodCollection 		= new Dictionary<string, Transform>();
		_powerupCollection		= new Dictionary<string, Transform>();
		
		_enemyCollection		= new Dictionary<string, Transform>();
		
		_initialPowerupGenerator = powerupGenerator;
		
		_initialCharacterPosition = character.position;
		_initialCharacterRotation = character.rotation;
		
		survivalTime = 0;
		
		SwitchState(States.Start);
	}
	#endregion
	
	#region Loop
	void Update() 
	{
		
	}
	#endregion
	
	#region Methods
	public void AddTrap()
	{
		try
		{
			string name = "Trap" + Time.time;
			
			Transform trap = Instantiate(trapPrefab, firePos.position, character.rotation) as Transform;
			trap.gameObject.name = name;
			
			_trapCollection.Add(name, trap);
		}
		catch(System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}
	
	public void RemoveTrap(string name, int killCount, int totalLevel)
	{
		try
		{
			Transform deleted = _trapCollection[name];
			
			_trapCollection.Remove(name);
			
			Destroy(deleted.gameObject);
			
			ComputeScore(killCount, totalLevel);
		}
		catch (System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}
	
	public void AddFood()
	{
		try
		{
			string name = "Food" + Time.time;
			
			Transform food = Instantiate(foodPrefab, firePos.position, character.rotation) as Transform;
			food.gameObject.name = name;
			
			_foodCollection.Add(name, food);
		}
		catch(System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}
	
	public void RemoveFood(string name)
	{
		try
		{
			Transform deleted = _foodCollection[name];
			
			_foodCollection.Remove(name);
			
			Destroy(deleted.gameObject);
		}
		catch (System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}
	
	public void AddEnemy(Vector3 spawnPosition, int spawnID)
	{
		try
		{
			string name = "Enemy" + spawnID + "_" + Time.time;
			
			Transform enemy = Instantiate(enemyPrefab) as Transform;
			enemy.gameObject.name = name;
			enemy.localPosition = spawnPosition;
			
			_enemyCollection.Add(name, enemy);
		}
		catch(System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}
	
	public void RemoveEnemy(string name)
	{
		try
		{
			Transform deleted = _enemyCollection[name];
			
			_enemyCollection.Remove(name);
			
			Destroy(deleted.gameObject);
		}
		catch (System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}
	
	public void GeneratePowerup(int powerupGeneratorID)
	{
		try
		{
			int powerupIndex = Random.Range(0, powerupPrefabs.Length);
			
			Transform powerup = Instantiate(powerupPrefabs[powerupIndex], powerupGenerator.position, powerupGenerator.rotation) as Transform;
			string name = "Powerup" + powerupGeneratorID + "_" + powerup.gameObject.GetComponent<PowerupScript>().powerupType + "_" + Time.time;
			
			powerup.gameObject.name = name;
			
			_powerupCollection.Add(name, powerup);
		}
		catch(System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}
	
	public void RemovePowerup(string name)
	{
		try
		{
			Transform deleted = _powerupCollection[name];
			
			_powerupCollection.Remove(name);
			
			Destroy(deleted.gameObject);
		}
		catch (System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}
	
	public void KillCharacter()
	{
		try
		{
			survivalTime = (int) Time.time;

			ClearCollections();

			//Destroy(character.gameObject);
			SpawnCharacter(false);
			SwitchState(States.GameOver);
		}
		catch (System.Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}
	
	public void SwitchState(States newState)
	{
		currentGameState = newState;
		
		switch(currentGameState)
		{
		case States.Start:
			SpawnCharacter(true);
			
			spawnPoints.gameObject.SetActive(false);
			MainCharLogic.Instance.ActivateControl(false);
			break;
		case States.InGame:
			ResetDirector();
			
			spawnPoints.gameObject.SetActive(true);
			MainCharLogic.Instance.ActivateControl(true);
			break;
		case States.GameOver:
			spawnPoints.gameObject.SetActive(false);
			MainCharLogic.Instance.ActivateControl(false);
			UI_Manager.Instance.ActivateGameOverPanel();
			break;
		}
	}
	
	private void ComputeScore(int killCount, int totalLevel)// bug here!
	{
		Debug.Log("Score: " + killCount + ", " + totalLevel);

		currentScore = (int) Time.time / timeFactor;
		
		currentScore = currentScore * totalLevel * killCount;
	}

	private void ClearCollections()
	{
		if(_foodCollection.Count != 0)
		{
			string[] foodLeft = _foodCollection.Keys.ToArray();
			
			foreach(string food in foodLeft)
			{
				RemoveFood(food);
			}
		}
		
		if(_trapCollection.Count != 0)
		{
			string[] trapLeft = _trapCollection.Keys.ToArray();
			
			foreach(string trap in trapLeft)
			{
				RemoveTrap(trap, 0, 0);
			}
		}
		
		if(_enemyCollection.Count != 0)
		{
			string[] enemyLeft = _enemyCollection.Keys.ToArray();
			
			foreach(string enemy in enemyLeft)
			{
				RemoveEnemy(enemy);
			}
		}
		
		if(_powerupCollection.Count != 0)
		{
			string[] powerupLeft = _powerupCollection.Keys.ToArray();
			
			foreach(string powerup in powerupLeft)
			{
				RemovePowerup(powerup);
			}
		}
	}
	
	private void ResetDirector()
	{
		powerupGenerator.position = _initialPowerupGenerator.position;
		
		currentScore = 0;
		
		survivalTime = 0;
		
		//ClearCollections();
	}
	
	
	
	public void SpawnCharacter(bool isCharacterAlive)
	{
		character.gameObject.SetActive(isCharacterAlive);
		
		MainCharLogic.Instance.ResetCharacter(_initialCharacterPosition, _initialCharacterRotation);
	}
	#endregion
}

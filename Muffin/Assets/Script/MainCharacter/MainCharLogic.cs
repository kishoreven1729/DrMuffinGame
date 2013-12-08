#region References
using UnityEngine;
using System.Collections;
#endregion

public class MainCharLogic : MonoBehaviour
{
	#region Private Variables
	private float _foodNextFire;
	private float _trapNextFire;
	private int	_initialTrapNo;
	private int	_initialFoodNo;

	protected Animator _animator;
	#endregion

	#region Public Variables
	public Vector3 direction;
	public int speed;

	public float reloadTime;

	public bool isDie;
	public int trapNo;
	public int foodNo;
	public bool isShield;
	public float shieldVanishTime;
	public static MainCharLogic Instance;//make the main character into a object
	public bool activateControl;
	public float regernateTime;
	#endregion

	#region Constructor
	void Awake ()
	{
		Instance = this;//make the main character into a object
	}
		
	void Start ()
	{	
		direction = Vector3.zero;

		_animator = gameObject.GetComponent<Animator>(); 
		isDie = false;
		isShield = false;
		activateControl = false;

		_initialTrapNo = trapNo;
		_initialFoodNo = foodNo;
	}
	#endregion
	
	#region Loop
	void Update()
	{
		if (activateControl == true) 
		{
			if (!isDie) 
			{
				Control ();	
			} 
			else 
			{
				GameDirector.gameInstance.KillCharacter ();
			}
		}
	}

	/*void OnTriggerEnter (Collider hit)
	{
		if (isShield && hit.gameObject.tag == "Enemy") 
		{
			StartCoroutine (ShieldDestroy (shieldVanishTime));
		} 
		else if (hit.gameObject.name.Contains ("Shield")) 
		{
			ShieldOn ();
		} 
		else if (hit.gameObject.tag == "Enemy") 
		{
			isDie = true;	
		} 
		else if (hit.gameObject.tag == "Trap" && hit.gameObject.GetComponent<Traps>().age >= 1) 
		{
			isDie = true;
			_animator.SetBool("die",false);
		}
	}*/

	void OnCollisionEnter(Collision collision)
	{
		if (isShield && collision.gameObject.tag == "Enemy") 
		{
			StartCoroutine (ShieldDestroy (shieldVanishTime));
		} 
		else if (collision.gameObject.name.Contains ("Shield")) 
		{
			ShieldOn ();
		} 
		else if (collision.gameObject.tag == "Enemy") 
		{
			isDie = true;	
		} 
		else if (collision.gameObject.tag == "Trap" && collision.gameObject.GetComponent<Traps>().age >= 1) 
		{
			isDie = true;
			_animator.SetBool("die",false);
		}
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, 1000 * direction);
	}
	#endregion

	#region Methods
	void Control ()
	{
		Vector3 dirTo = new Vector3 (Input.acceleration.x, 0, Input.acceleration.y);

		direction = Vector3.Slerp(direction, dirTo, 0.5f);
		rigidbody.velocity = direction * speed;

		transform.LookAt (1000 * direction);

		if(direction == Vector3.zero)
		{
			//idle animation
			_animator.SetBool("isWalking",false);
			_animator.SetBool("dropping",false);
			_animator.SetBool("die",false);
			_animator.SetBool("idling",false);
			_animator.SetBool("isBlinking",true);
		}
		else
		{
			//walking animation
			_animator.SetBool("isWalking",true);
			_animator.SetBool("dropping",false);
			_animator.SetBool("die",false);
			_animator.SetBool("idling",false);
			_animator.SetBool("isBlinking",false);
		}
	}

	public void DropFood()
	{
		if ((Time.time > _foodNextFire) && (foodNo > 0) && (isDie == false)) 
		{
		
			GameDirector.gameInstance.AddFood ();
			_foodNextFire = Time.time + reloadTime;
			foodNo -= 1;

			_animator.SetBool("isWalking",false);
			_animator.SetBool("dropping",true);
			_animator.SetBool("die",false);
			_animator.SetBool("idling",false);
			_animator.SetBool("isBlinking",false);
		}
	}

	public void DropTrap()
	{
		if (Time.time > _trapNextFire && trapNo > 0 && !isDie) 
		{
			GameDirector.gameInstance.AddTrap();

			_trapNextFire = Time.time + reloadTime;
			trapNo -= 1;

			_animator.SetBool("isWalking",false);
			_animator.SetBool("dropping",true);
			_animator.SetBool("die",false);
			_animator.SetBool("idling",false);
			_animator.SetBool("isBlinking",false);
		}
	}

	void ShieldOn()
	{
		isShield = true;
	}

	IEnumerator ShieldDestroy(float waitTime)
	{
		yield return new WaitForSeconds (waitTime);
		isShield = false;
	}

	IEnumerator foodRegernate(float waitTime)///////////////////////////////////////////
	{
		yield return new WaitForSeconds (waitTime);
		if(foodNo<3)
			foodNo += 1;
	}
	IEnumerator trapRegernate(float waitTime)///////////////////////////////////////////
	{
		yield return new WaitForSeconds (waitTime);
		if(trapNo<3)
			trapNo += 1;
	}


	public void ActivateControl (bool turnOn)
	{
		activateControl = turnOn;
	}

	public void ResetCharacter (Vector3 initialTransformPosition, Quaternion initialCharacterRotation)
	{
		transform.position = initialTransformPosition;
		transform.rotation = initialCharacterRotation;

		transform.rigidbody.velocity = Vector3.zero;

		trapNo = _initialTrapNo;
		foodNo = _initialFoodNo;

		isDie = false;
		isShield = false;

		activateControl = false;

		_animator.SetBool("isWalking",false);
		_animator.SetBool("dropping",false);
		_animator.SetBool("die",false);
		_animator.SetBool("idling",false);
		_animator.SetBool("isBlinking",false);
	}
	#endregion
}

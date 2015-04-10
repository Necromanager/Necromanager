using UnityEngine;
using System.Collections;

public abstract class ZombieScript : MonoBehaviour 
{
	protected Color NORMAL_COLOR = Color.white;
	protected Color STUNNED_COLOR = Color.blue;
	protected Color BOSS_COLOR = Color.red;

	protected float speed;
	protected float totalDelta;
	protected float stunTime;
	protected float curStunTime;
	
	protected int totalHealth;
	protected int curHealth;
	
	protected bool isBoss;
	protected bool canMove;
	protected bool isSpotlight;
	protected bool escaped;

	protected string myName;
	
	protected Vector3 startPos;
	protected Vector3 goalPos;
	
	protected ZombieStatus zomStat;
	
	protected Animator animator;
	protected AudioSource audioSource;
	
	protected Grave homeGrave;

	void Start () 
	{
		Init();
	}
	
	public void Init()
	{
		startPos = transform.position;
		goalPos = transform.position;
		isBoss = false;
		canMove = true;
		isSpotlight = false;
		escaped = false;
		speed = 0.7f;
		totalDelta = 0;
		stunTime = 5;
		curStunTime = 0;
		totalHealth = 100;
		curHealth = totalHealth;
		zomStat = ZombieStatus.NORMAL;
		GenerateName();
		
		animator = GetComponentInChildren<Animator>();
		animator.transform.eulerAngles = new Vector3(30,0,0);
        animator.transform.localScale = new Vector3(0, .65f, 1);
		
		CreateAudioSource();
		
		GameObject[] spotLights = GameObject.FindGameObjectsWithTag("Spotlight");
		if (spotLights.Length > 0)
		{
			zomStat = ZombieStatus.STUNNED;
			isSpotlight = true;
		}
		//gameObject.GetComponent<AudioSource>().PlayOneShot(GetSpawnSound());
		//GameObject.FindGameObjectWithTag("Main").GetComponent<SoundManager>().PlaySound(GetSpawnSound());
	}
	
	protected void CreateAudioSource()
	{
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.maxDistance = 2000;
		audioSource.volume = 50;
		audioSource.rolloffMode = AudioRolloffMode.Linear;
		audioSource.clip = GetSpawnSound();
		audioSource.Play();
	}
	
	protected virtual void Update () 
	{
		totalDelta += Time.deltaTime;
		if (escaped)
		{
			zomStat = ZombieStatus.NORMAL;
			MoveOutside();
			return;
		}
		UpdateStatus();
		UpdateColor ();
		if (zomStat == ZombieStatus.NORMAL)
		{
			if (canMove)
			{
				UpdateMovement();
			}
		}
		else if (zomStat == ZombieStatus.DEAD)
		{
			Destroy (gameObject);
		}
		
		//Vector3 cameraPos = Camera.main.transform.position;
		//animator.transform.rotation = Quaternion.LookRotation(cameraPos);
	}
	
	protected void GenerateName()
	{
		myName = "Bob Roberts";
	}
	
	public void SetName(string newName)
	{
		myName = newName;
	}
	
	public string GetName()
	{
		return myName;
	}
	
	public void LoseHealth(int healthLost)
	{
		curHealth -= healthLost;
		if (curHealth <= 0)
		{
			GlobalValues.zombiesKilled++;
			zomStat = ZombieStatus.DEAD;
			curHealth = 0;
		}
		else if (curHealth > totalHealth)
		{
			curHealth = totalHealth;
		}
	}
	
	public void SetHomeGrave(Grave spawnGrave)
	{
		homeGrave = spawnGrave;
	}
	
	public void Bury()
	{
		try
		{
			GlobalValues.zombiesReturned++;
			Destroy (gameObject);
		}
		catch
		{
		}
	}
	
	protected void UpdateColor()
	{
		if (isBoss)
		{
			animator.GetComponent<Renderer>().material.color = BOSS_COLOR;
		}
		else if (zomStat == ZombieStatus.NORMAL)
		{
			animator.GetComponent<Renderer>().material.color = NORMAL_COLOR;
		}
		else if (zomStat == ZombieStatus.STUNNED)
		{
			animator.GetComponent<Renderer>().material.color = STUNNED_COLOR;
		}
		else if (zomStat == ZombieStatus.GRABBED)
		{
			animator.GetComponent<Renderer>().material.color = NORMAL_COLOR;
		}
	}
	
	protected void UpdateStatus()
	{
		if (zomStat == ZombieStatus.DEAD)
		{
			return;
		}
		
		if (zomStat == ZombieStatus.STUNNED)
		{
			if (isSpotlight)
			{
				return;
			}
			curStunTime += Time.deltaTime;
			//animator.speed = 0;
			if (curStunTime >= stunTime)
			{
				curStunTime = 0;
				zomStat = ZombieStatus.NORMAL;
			}
		}
		
		if (zomStat == ZombieStatus.NORMAL)
		{
			animator.speed = 0.5f;
		}
	}
	
	public void ResetMovement()
	{
		try
		{
			startPos = transform.position;
			goalPos = startPos;
		}
		catch
		{
		}
	}
	
	public void SetPosition(Vector3 newPos)
	{
		try
		{
			transform.position = newPos;
		}
		catch
		{
		}
	}
	
	public void SetStatus(ZombieStatus newStat)
	{
		zomStat = newStat;
	}
	
	public void AlterRotation(Quaternion newRot)
	{
		try
		{
			transform.rotation *= newRot;
		}
		catch
		{
		}
	}
	
	public ZombieStatus GetStatus()
	{
		return zomStat;
	}
	
	public void SetCanMove(bool canZombieMove)
	{
		canMove = canZombieMove;
	}
	
	protected void UpdateMovement()
	{
		transform.position = Vector3.Lerp (startPos,goalPos,speed*totalDelta);
		
		if (goalPos.x > startPos.x)
		{
			//animator.transform.localScale = new Vector3(-1,.65f,1);
            animator.transform.localScale = new Vector3(-0, .65f, 1);
		}
		else if (goalPos.x < startPos.x)
		{
			//animator.transform.localScale = new Vector3(1,.65f,1);
            animator.transform.localScale = new Vector3(0, .65f, 1);
		}
		
		if (ReachedGoal())
		{
			transform.position = goalPos;
			totalDelta = 0;
			
			if (CheckEscaped())
			{
				escaped = true;
				CheckZombiesGone();
				GlobalValues.zombiesEscaped++;
				PopUpFactory.ZombieEscapeMessage();
				GlobalValues.AddMoney(-GlobalValues.escapeMoney);
				MoneyUI.spawnSadMoneyText(-GlobalValues.escapeMoney);
				//Destroy (gameObject);
			}
			else
			{
				Tile nextTile = Pathfinding.GetNextTile(GetCurrentTile(),GetGoalTile());
				Vector3 tilePos = nextTile.transform.position;
				startPos = transform.position;
				goalPos = new Vector3(tilePos.x,startPos.y,tilePos.z);
			}
		}
	}
	
	private void CheckZombiesGone()
	{
		//int numZombies = 0;
		GameObject[] zombies = GameObject.FindGameObjectsWithTag("Zombie");
		GameObject[] graves = GameObject.FindGameObjectsWithTag("Grave");
		
		if (zombies.Length-1 != 0)
		{
			return;
		}
	
		foreach (GameObject grave in graves)
		{
			Grave graveScript = grave.GetComponent<Grave>();
			if (graveScript.GetNumZombies() != 0)
			{
				return;
			}
		}
		
		GlobalValues.EndTime();
	}
	
	protected bool ReachedGoal()
	{
		float posDiff = 0.05f;
		
		bool xEqual = Mathf.Abs(goalPos.x-transform.position.x) <= posDiff;
		bool zEqual = Mathf.Abs(goalPos.z-transform.position.z) <= posDiff;
		
		return (xEqual && zEqual);
	}
	
	protected bool CheckEscaped()
	{
		Vector3 curPos = GetCurrentTile().transform.position;
		Vector3 goalPos = GetGoalTile().transform.position;
		
		if (!GetGoalTile().IsGateSpot())
		{
			return false;
		}
		
		return (curPos.Equals(goalPos));
	}
	
	protected void MoveOutside()
	{
		Vector3 gateSpot = GetGoalTile ().transform.position;
		Vector3 outPos = new Vector3(gateSpot.x, transform.position.y, gateSpot.z-5);
		
		transform.position = Vector3.Lerp (transform.position,outPos,speed*totalDelta);
		
		Vector3 pos = transform.position;
		
		if (Vector3.Distance(outPos, pos) <= 0.2f)
		{
			Destroy (gameObject);
		}
		//return (curPos.Equals(goalPos));
	}
	
	protected Tile GetCurrentTile()
	{
		Vector3 spherePos = new Vector3(transform.position.x,transform.position.y-1.5f,transform.position.z);
		Collider[] below = Physics.OverlapSphere(spherePos,0.4f);
		foreach (Collider ob in below)
		{
			if ((ob.tag == "Floor"))
			{
				return ob.GetComponent<Tile>();
			}
		}
		return null;
	}
	
	protected Tile GetRandomTile()
	{
		GameObject[] floors = GameObject.FindGameObjectsWithTag("Floor");
		Tile randTile;
		
		do
		{
			randTile = floors[Random.Range(0,floors.Length)].GetComponent<Tile>();
		}while (!randTile.IsOpen());
		
		return randTile;
	}
	
	public void PlayHitSound()
	{
		audioSource.PlayOneShot(GetHitSound());
	}
	
	/*void OnTriggerEnter(Collider col)
	{
		Debug.Log(col.gameObject.tag);
		if (col.gameObject.tag == "Gum")
		{
			Debug.Log("Stepped in gum");
			Gum gum = col.gameObject.GetComponent<Gum>();
			if (!gum.HasZombie())
			{
				gum.StickZombie(this);
			}
		}
	}*/
	
	public abstract Tile GetGoalTile();
	public abstract void Stun();
	public abstract void Shoot();
	public abstract bool Grab();
	protected abstract AudioClip GetSpawnSound();
	public abstract AudioClip GetHitSound();
}

using UnityEngine;
using System.Collections;

public class Shovel : Item
{
	private const float RANGE = 1.4f;
	private const float OFFSET = .75f;

	private Object[] shovelSounds;
	private GameObject hitArea;

	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	public override void Init()
	{
		itemName = "Shovel";
		itemDesc = "Temporarily stun zombies";
		//soundPath = "miss";
		LoadData ();
		//price = 0;
		itemPic = Resources.Load<Sprite>("Sprites/Inventory/ItemShovel");
		if (!itemPic)
		{
			Debug.Log("Shovel not loaded");
		}
		storePic = Resources.Load(SOLD_OUT_TEXTURE) as Texture;
		//cooldownTime = 0.4f;
		curCooldown = cooldownTime;
		
		//shovelSounds = Resources.LoadAll("Sounds/Effects/Shovel/");
	}
	
	public override bool IsUsable(PlayerScript player)
	{
		return true;
	}
	
	public override void Activate (PlayerScript player)
	{
		ZombieScript tempZombie;
		Vector3 pos = player.transform.position;
		//Quaternion rot = player.GetComponentInChildren<Animator>().transform.rotation;
		Vector3 forward = player.GetComponentInChildren<Animator>().transform.forward;
		forward *= OFFSET;
		Vector3 spherePos = new Vector3(pos.x+forward.x, pos.y, pos.z+forward.z);
		
		/*if (player.GetDirection() == Direction.LEFT)
		{
			spherePos = new Vector3(pos.x-OFFSET,pos.y,pos.z);
		}
		else
		{
			spherePos = new Vector3(pos.x+OFFSET,pos.y,pos.z);
		}*/
		
		
		Collider[] around = Physics.OverlapSphere(spherePos,RANGE/2.0f);
		bool hitZombie = false;
		
		foreach (Collider ob in around)
		{
			if (ob.tag == "Zombie")
			{
				tempZombie = ob.GetComponent<ZombieScript>();
				tempZombie.Stun();
				tempZombie.PlayHitSound();
				hitZombie = true;
				ObjectiveEvents.hitZombieObjective();
			}
		}
		
		if (!hitZombie)
		{
			GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.useShovel);
		}
		else
		{
			PlaySoundEffect();
			PlayerCamera playerCam = Camera.main.GetComponent<PlayerCamera>();
			playerCam.ShakeCamera(10f,0.2f,0.2f);
		}
	}
	
	public override void Buy()
	{
		soldOut = true;	
	}
	
	private Color GetHitAreaColor()
	{
		if (CanUse())
		{
			return new Color(ACTIVE_COLOR.r,ACTIVE_COLOR.g,ACTIVE_COLOR.b,USE_AREA_ALPHA);
		}
		else
		{
			return new Color(INACTIVE_COLOR.r,INACTIVE_COLOR.g,INACTIVE_COLOR.b,USE_AREA_ALPHA);
		}
	}
	
	protected override void UpdateSpecial()
	{
		/*if (!hitArea)
		{
			hitArea = GameObject.Instantiate(Resources.Load("Prefabs/ItemHitArea")) as GameObject;
			hitArea.GetComponent<Renderer>().material.color = GetHitAreaColor();
			hitArea.transform.localScale = new Vector3(RANGE,0.2f,RANGE);
		}
		
		hitArea.GetComponent<Renderer>().enabled = selected;
		if (hitArea.GetComponent<Renderer>().enabled)
		{
			PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
			Vector3 playerPos = player.transform.position;
			Direction playerDirection = player.GetDirection();
			float posOffset = OFFSET;
			if (playerDirection == Direction.LEFT)
			{
				posOffset = -posOffset;
			}
			
			
			hitArea.GetComponent<Renderer>().material.color = GetHitAreaColor();
			hitArea.transform.position = new Vector3(playerPos.x+posOffset,0.5f,playerPos.z);
		}*/
	}
	
	public override void SpecialReset()
	{	
	}

	protected override void PlaySoundEffect ()
	{
		int soundIndex = Random.Range(0,SoundEffectLibrary.shovelHit.Count);
		GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.shovelHit[soundIndex]);
	}
}

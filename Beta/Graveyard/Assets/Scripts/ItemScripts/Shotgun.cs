using UnityEngine;
using System.Collections;

public class Shotgun : Item
{	
	private const float RANGE = 2f;
	private const float OFFSET = 1.2f;
	
	private GameObject hitArea;

	public override void Init()
	{
		itemName = "Shotgun";
		itemDesc = "Kill zombies";
		soundPath = "shotgun";
		price = 30;
		itemPic = Resources.Load<Sprite>("Sprites/Inventory/ItemShotgun");
		storePic = Resources.Load("Textures/Items/StoreShotgun") as Texture;
		cooldownTime = 5;
		curCooldown = cooldownTime;
	}
	
	public override bool IsUsable(PlayerScript player)
	{
		return true;
	}
	
	public override void Activate (PlayerScript player)
	{
		ZombieScript tempZombie;
		Vector3 pos = player.transform.position;
		//Vector3 spherePos = new Vector3(pos.x,pos.y,pos.z);
		Vector3 spherePos;
		if (player.GetDirection() == Direction.LEFT)
		{
			spherePos = new Vector3(pos.x-OFFSET,pos.y,pos.z);
		}
		else
		{
			spherePos = new Vector3(pos.x+OFFSET,pos.y,pos.z);
		}
		
		Collider[] around = Physics.OverlapSphere(spherePos,RANGE/2.0f);
		
		foreach (Collider ob in around)
		{
			if (ob.tag == "Zombie")
			{
				tempZombie = ob.GetComponent<ZombieScript>();
				tempZombie.LoseHealth(100);
			}
		}
		
		PlaySoundEffect();
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
		if (!hitArea)
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
		}
	}
	
	public override void SpecialReset()
	{	
	}
}

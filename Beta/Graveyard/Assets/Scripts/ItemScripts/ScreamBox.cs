using UnityEngine;
using System.Collections;

public class ScreamBox : Item 
{
	private const int MAX_BOX = 1;
	
	private bool CanMakeBox()
	{
		GameObject[] boxes = GameObject.FindGameObjectsWithTag("ScreamBox");
		
		return boxes.Length < MAX_BOX;
	}

	protected override void UpdateSpecial()
	{
	}
	
	public override void SpecialReset()
	{
		
	}
	
	public override void Init()
	{
		itemName = "Scream Box";
		itemDesc = "Attract nearby zombies";
		LoadData ();
		
		itemPic = Resources.Load<Sprite>("Sprites/Store/New/ScreamBoxIcon");
		storePic = Resources.Load("Textures/Items/StoreGum") as Texture;
		
		curCooldown = cooldownTime;
	}
	
	public override bool IsUsable(PlayerScript player)
	{
		return CanMakeBox();
	}
	
	public override void Activate (PlayerScript player)
	{
		Vector3 playerPos = player.transform.position;
		
		GameObject box = GameObject.Instantiate(Resources.Load("Prefabs/ScreamBox")) as GameObject;
		box.transform.position = new Vector3(playerPos.x, playerPos.y-0.8f, playerPos.z);
		
		PlaySoundEffect ();
	}
	
	public override void Buy()
	{
		soldOut = true;
	}
	
	protected override void PlaySoundEffect ()
	{
		GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.useGum);
	}
}

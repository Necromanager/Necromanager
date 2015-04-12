using UnityEngine;
using System.Collections;

public class ChewingGum : Item 
{
	private const int MAX_GUM = 3;
	
	private bool CanMakeGum()
	{
		GameObject[] gums = GameObject.FindGameObjectsWithTag("Gum");
		
		return gums.Length < MAX_GUM;
	}
	
	protected override void UpdateSpecial()
	{
	}
	
	public override void SpecialReset()
	{
		
	}
	
	public override void Init()
	{
		itemName = "Chewing Gum";
		itemDesc = "Stop zombies in their tracks";
		LoadData ();
		
		itemPic = Resources.Load<Sprite>("Sprites/Store/New/GumIcon");
		storePic = Resources.Load("Textures/Items/StoreGum") as Texture;

		curCooldown = cooldownTime;
	}
	
	public override bool IsUsable(PlayerScript player)
	{
		return CanMakeGum();
	}
	
	public override void Activate (PlayerScript player)
	{
		Vector3 playerPos = player.transform.position;
	
		GameObject gum = GameObject.Instantiate(Resources.Load("Prefabs/Gum")) as GameObject;
		gum.transform.position = new Vector3(playerPos.x, playerPos.y-1, playerPos.z);
		
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

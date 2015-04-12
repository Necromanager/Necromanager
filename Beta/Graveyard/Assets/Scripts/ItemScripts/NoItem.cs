using UnityEngine;
using System.Collections;

public class NoItem : Item
{
	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	public override void Init()
	{
		itemName = "N/A";
		itemDesc = "N/A";
		price = 0;
		itemPic = Resources.Load<Sprite>("Sprites/BlankPixel");
		storePic = Resources.Load("Textures/Items/StoreSoldOut") as Texture;
		cooldownTime = 1;
		curCooldown = cooldownTime;
		soldOut = true;
	}
	
	public override bool IsUsable(PlayerScript player)
	{
		return false;
	}
	
	public override void Activate (PlayerScript player)
	{
	}
	
	public override void Buy()
	{
	}
	
	protected override void UpdateSpecial()
	{
	}
	
	public override void SpecialReset()
	{	
	}
}

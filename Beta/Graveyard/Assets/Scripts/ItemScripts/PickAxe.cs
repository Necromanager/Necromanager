using UnityEngine;
using System.Collections;

public class PickAxe : Item
{
	//private Object[] pickAxeSounds;

	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	public override void Init()
	{
		itemName = "Pick Axe";
		itemDesc = "Deepen graves";
		soundPath = "miss";
		LoadData ();
		//price = 100;
		itemPic = Resources.Load<Sprite>("Sprites/Inventory/ItemPickAxe");
		storePic = Resources.Load("Textures/Items/StorePickAxe") as Texture;
		//cooldownTime = 100;
		//maxCooldown = 5;
		curCooldown = cooldownTime;
		//pickAxeSounds = Resources.LoadAll("Sounds/Effects/PickAxe/");
	}
	
	public override bool IsUsable(PlayerScript player)
	{
		bool onGrave = player.IsOnGrave();
		bool hasRoom = false;
		
		if (onGrave)
		{
			Grave grave = player.GetGrave();
			hasRoom = grave.CanGetDeeper(1);
		}
		
		if (!onGrave || !hasRoom)
		{
			PopUpFactory.NoTargetMessage();
		}
		
		return onGrave && hasRoom;
	}
	
	public override void Activate (PlayerScript player)
	{
		Grave grave = player.GetGrave();
		grave.ChangeDepth(1);
		PlaySoundEffect ();
	}
	
	public override void Buy()
	{
		soldOut = true;	
	}

	protected override void PlaySoundEffect ()
	{
		int soundIndex = Random.Range(0, SoundEffectLibrary.usePickaxe.Count);
		GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.usePickaxe[soundIndex]);
	}

	protected override void UpdateSpecial()
	{
	}
	
	public override void SpecialReset()
	{	
	}
}

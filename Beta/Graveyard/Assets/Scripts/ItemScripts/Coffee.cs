using UnityEngine;
using System.Collections;

public class Coffee : Item 
{
	private const float SPEED_MOD = 3f;
	private const float DURATION = 10f;

	private float currentDuration;
	private PlayerScript playerScript;

	void Start () 
	{
	}

	void Update () 
	{
	}
	
	protected override void UpdateSpecial()
	{
		if (currentDuration > 0)
		{
			currentDuration -= Time.deltaTime;
			if (currentDuration <= 0)
			{
				currentDuration = 0;
				playerScript.SetSpeed(playerScript.GetOriginalSpeed());
			}
		}
	}
	
	public override void SpecialReset()
	{
	}
	
	public override void Init()
	{
		itemName = "Coffee";
		itemDesc = "Get a small burst of energy";
		LoadData ();
		//price = 25;
		itemPic = Resources.Load<Sprite>("Sprites/Store/New/CoffeeIcon");
		storePic = Resources.Load("Textures/Items/StoreCoffee") as Texture;
		//cooldownTime = 40;
		curCooldown = cooldownTime;
		currentDuration = 0;
	}
	
	public override bool IsUsable(PlayerScript player)
	{
		return true;
	}
	
	public override void Activate (PlayerScript player)
	{
		playerScript = player;
		playerScript.SetSpeed(player.GetSpeed()*SPEED_MOD);
		currentDuration = DURATION;
		PlaySoundEffect ();
		//player.LoseItem(this);
	}
	
	public override void Buy()
	{
		soldOut = true;
	}

	protected override void PlaySoundEffect ()
	{
		GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.useCoffee);
	}
}

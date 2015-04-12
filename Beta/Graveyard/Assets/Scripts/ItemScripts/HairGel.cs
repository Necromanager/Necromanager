using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HairGel : Item
{
	private const int MAX_GEL = 3;
	
	private List<GameObject> createdGel;

	/*private bool CanMakeGel()
	{
		GameObject[] gels = GameObject.FindGameObjectsWithTag("Gel");
		
		return gels.Length < MAX_GEL;
	}*/
	
	public void RemoveFromList(Gel gel)
	{
		createdGel.Remove(gel.gameObject);
	}
	
	protected override void UpdateSpecial()
	{
	}
	
	public override void SpecialReset()
	{
		createdGel = new List<GameObject>();
	}
	
	public override void Init()
	{
		itemName = "Hair Gel";
		itemDesc = "Trip up zombies";
		LoadData ();
		
		itemPic = Resources.Load<Sprite>("Sprites/Store/New/HairGelIcon");
		storePic = Resources.Load("Textures/Items/StoreHairGel") as Texture;
		
		createdGel = new List<GameObject>();
		curCooldown = cooldownTime;
	}
	
	public override bool IsUsable(PlayerScript player)
	{
		return true;
	}
	
	public override void Activate (PlayerScript player)
	{
		Vector3 playerPos = player.transform.position;
		
		GameObject gel = GameObject.Instantiate(Resources.Load("Prefabs/Gel")) as GameObject;
		gel.transform.position = new Vector3(playerPos.x, playerPos.y-1, playerPos.z);
		gel.GetComponent<Gel>().SetCreator(this);
		
		if (createdGel.Count >= MAX_GEL)
		{
			GameObject.Destroy(createdGel[0]);
			createdGel.RemoveAt(0);
		}
		createdGel.Add(gel);
		
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

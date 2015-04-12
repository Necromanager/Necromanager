using UnityEngine;
using System.Collections;

public class GraveBell : Building 
{
	public GraveBell()
	{
		name = "Grave Bell";
		desc = "Alerts you to escaping zombies.";
		
		LoadData ();
		menuTex = Resources.Load<Sprite>("Sprites/Buildings/New/BellIcon");
		createObject = Resources.Load("Prefabs/Bell") as GameObject;
	}

	public override bool IsSpaceUsable(Tile currentTile)
	{
		//return GetPathfindingWorks(currentTile) && GetEnoughTunnels();
		return currentTile.IsGraveSpot ();
	}
	
	public override bool ShouldCloseSpace()
	{
		return false;
	}
	
	public override AudioClip GetBuildSound()
	{
		return SoundEffectLibrary.placeBell;
	}
}

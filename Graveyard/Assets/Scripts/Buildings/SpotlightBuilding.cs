using UnityEngine;
using System.Collections;

public class SpotlightBuilding : Building 
{
	public SpotlightBuilding()
	{
		name = "Spotlight";
		desc = "Stuns zombies indefinitely.";
		
		LoadData ();
		menuTex = Resources.Load<Sprite>("Sprites/Buildings/spotlight");
		createObject = Resources.Load("Prefabs/SpotLight") as GameObject;
	}

	private bool GetPathfindingWorks(Tile currentTile)
	{
		bool usable = true;
		currentTile.SetOpen(false);
		
		if (!TestPathfinding(currentTile.GetAdjacent()))
		{
			usable = false;
		}
		
		currentTile.SetOpen(true);
		
		return usable;
	}

	private bool GetEnoughSpotlights()
	{
		GameObject[] spotlights = GameObject.FindGameObjectsWithTag("Spotlight");
		
		return spotlights.Length < 1;
	}

	public override bool IsSpaceUsable(Tile currentTile)
	{
		return GetPathfindingWorks (currentTile) && GetEnoughSpotlights();
	}
	
	public override bool ShouldCloseSpace()
	{
		return true;
	}
	
	public override AudioClip GetBuildSound()
	{
		return SoundEffectLibrary.placeSpotlight;
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barricade : Building
{
	
	public Barricade()
	{
		name = "Flower bed";
		desc = "Blocks the zombies' path.";
		//cost = 10;
		LoadData ();
		menuTex = Resources.Load<Sprite>("Sprites/Buildings/New/FlowerIcon");
		createObject = Resources.Load("Prefabs/Barricade") as GameObject;
		//createObject = Resources.Load("Prefabs/Tree") as GameObject;
		
	}
	
	/*private bool TestPathfinding(List<Tile> testTiles)
	{
		//GetCurrentTile();
		GameObject[] tiles = GameObject.FindGameObjectsWithTag("Floor");
		Tile start = null;
		Tile temp;
		
		foreach (GameObject tile in tiles)
		{
			temp = tile.GetComponent<Tile>();
			if (temp.IsOpen())
			{
				start = temp;
				break;
			}
		}
		
		foreach (Tile tile in testTiles)
		{
			if (tile.IsOpen() || tile.IsGraveSpot())
			{
				if (!Pathfinding.IsPathfindingPossible(start,tile))
				{
					return false;
				}
			}
		}
		
		return true;
	}*/
	
	public override bool IsSpaceUsable(Tile currentTile)
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
	
	public override bool ShouldCloseSpace()
	{
		return true;
	}
	
	public override AudioClip GetBuildSound()
	{
		return SoundEffectLibrary.placeFlower;
	}
}

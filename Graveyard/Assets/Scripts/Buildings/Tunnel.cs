using UnityEngine;
using System.Collections;

public class Tunnel : Building 
{
	public Tunnel()
	{
		name = "Tunnel";
		desc = "Create a shortcut. Needs two tunnels to work.";
		
		LoadData ();
		menuTex = Resources.Load<Sprite>("Sprites/Buildings/tunnel");
		createObject = Resources.Load("Prefabs/Tunnel") as GameObject;
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
	
	private bool GetEnoughTunnels()
	{
		GameObject[] tunnels = GameObject.FindGameObjectsWithTag("Tunnel");
		
		return tunnels.Length < 2;
	}
	
	public override bool IsSpaceUsable(Tile currentTile)
	{
		return GetPathfindingWorks(currentTile) && GetEnoughTunnels();
	}
	
	public override bool ShouldCloseSpace()
	{
		return false;
	}
	
	public override AudioClip GetBuildSound()
	{
		return SoundEffectLibrary.placeTunnel;
	}
}

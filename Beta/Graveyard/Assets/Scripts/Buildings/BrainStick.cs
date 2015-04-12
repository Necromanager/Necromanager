using UnityEngine;
using System.Collections;

public class BrainStick : Building
{
	public BrainStick()
	{
		name = "Brain Stick";
		desc = "Draws in nearby zombies.";
		//cost = 50;
		LoadData ();
		menuTex = Resources.Load<Sprite>("Sprites/Buildings/New/BrainIcon");
		createObject = Resources.Load("Prefabs/BrainStick") as GameObject;
	}

	public override bool IsSpaceUsable(Tile currentTile)
	{
		return true;
	}
	
	public override bool ShouldCloseSpace()
	{
		return false;
	}
	
	public override AudioClip GetBuildSound()
	{
		return SoundEffectLibrary.placeBrain;
	}
}

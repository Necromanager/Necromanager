using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Building 
{
	protected int FONT_SIZE = 20;

	protected GameObject createObject;
	protected Tile attachedTile;
	protected string name;
	protected string desc;
	protected float cost;
	protected float health;
	protected int max;
	protected Sprite menuTex;
	
	protected GUIStyle myStyle;
	
	protected void SetupFont(int fontSize)
	{
		myStyle = new GUIStyle();
		myStyle.fontSize = fontSize;
		GUI.skin.label.fontSize = fontSize;
	}
	
	public void SetAttachedTile(Tile tile)
	{
		attachedTile = tile;
	}
	
	public Tile GetAttachedTile()
	{
		return attachedTile;
	}

	public string GetName()
	{
		return name;
	}
	
	public string GetDesc()
	{
		return desc;
	}

	public float GetCost()
	{
		return cost;
	}
	
	public Sprite GetTexture()
	{
		return menuTex;
	}
	
/*	public Vector2 GetTextureSize()
	{
		return new Vector2(menuTex.width,menuTex.height);
	}*/
	
	public GameObject GetCreateObject()
	{
		return createObject;
	}
	
	protected bool TestPathfinding(List<Tile> testTiles)
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
	}
	
	/*public bool DrawAndCheckButton(Vector2 drawPos)
	{
		SetupFont(FONT_SIZE);
		Vector2 buttonSize = GetTextureSize();
		Rect drawRect = new Rect(drawPos.x,drawPos.y,buttonSize.x,buttonSize.y);
		
		string buttonDesc = name+" - $"+cost+"\n"+desc;
		Vector2 textSize = myStyle.CalcSize(new GUIContent(buttonDesc));
		GUI.Label(new Rect(drawRect.x,drawRect.y-textSize.y*1.2f,textSize.x,textSize.y*1.2f),buttonDesc);
		
		return GUI.Button(drawRect,menuTex);
	}*/

	protected void LoadData()
	{
		GameObject main = GameObject.FindGameObjectWithTag ("Main");
		BuildingValues buildingVals = main.GetComponent<BuildingValues> ();
		BuildingData buildingData = buildingVals.GetData (name);
		
		cost = buildingData.cost;
		health = buildingData.health;
		max = buildingData.max;
	}
	
	public abstract bool IsSpaceUsable(Tile currentTile);
	public abstract bool ShouldCloseSpace();
	public abstract AudioClip GetBuildSound();
}

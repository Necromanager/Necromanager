using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory
{
	private const int BOX_WIDTH = 64;
	private const int BOX_HEIGHT = 64;
	//private const float TEXT_TIME = 1.2f;
	private const int COOLDOWN_SIZE = 30;
	
	private InventoryUI inventoryUI = null;
	
	List<Item> itemList = new List<Item>();
	//private float textAlpha = 1;
	//private float curTextTime = TEXT_TIME;
	//private bool drawItem = true;
	
	private int curIndex = 0;
	
	public void Reset()
	{
		foreach (Item item in itemList)
		{
			item.Reset();
		}
		
		//drawItem = true;
	}
	
	public void SetVisible(bool isVisible)
	{
		//drawItem = isVisible;
		GameObject[] hitAreas = GameObject.FindGameObjectsWithTag("HitArea");
		foreach (GameObject hitArea in hitAreas)
		{
			hitArea.GetComponent<Renderer>().enabled = isVisible;
		}
	}
	
	private void LoadInventoryUI()
	{
		if (inventoryUI == null)
		{
			GameObject canvas = GameObject.FindGameObjectWithTag("UIManager");
			inventoryUI = canvas.GetComponentInChildren<InventoryUI>();
		}
	}
	
	public void Draw()
	{	
		LoadInventoryUI();
		
		inventoryUI.SetItems(GetCurItem(),GetNextItem(),GetPrevItem());
	}
	
	/*private void DrawItem(Rect drawPos,Item item,bool selected)
	{
		GUI.color = item.GetHudColor(selected);
		//GUI.DrawTexture(drawPos,item.GetTexture());
		if (!item.CanUse())
		{
			GUI.color = Color.white;
			GUI.skin.label.fontSize = COOLDOWN_SIZE;
			
			GUIStyle myStyle = new GUIStyle();
			myStyle.fontSize = COOLDOWN_SIZE;
			Vector2 textSize = myStyle.CalcSize(new GUIContent(item.GetCooldownString()));
			
			GUI.Label(new Rect(drawPos.x,drawPos.y,textSize.x,textSize.y*1.2f),item.GetCooldownString());
		}
	}*/
	
	public void UpdateItems()
	{
		for (int i=0; i<itemList.Count; i++)
		{
			//bool selected = (i == curIndex);
			
			//If iventory is made invisible, the items will not be drawn
			/*if (!drawItem)
			{
				selected = false;
			}*/
			
			itemList[i].UpdateItem(i == curIndex);
		}
	}
	
	private void ResetText()
	{
		LoadInventoryUI();
		inventoryUI.DisplayDescription();
	}
	
	public void AddItem(Item newItem)
	{
		curIndex = 0;
		itemList.Add(newItem);
		ResetText();
	}
	
	public void LoseItem(Item lostItem)
	{
		itemList.Remove(lostItem);
		curIndex = 0;
		ResetText();
	}
	
	public Item GetCurItem()
	{
		if (itemList.Count > 0)
		{
			return itemList[curIndex];
		}
		
		return GetNullItem();
	}
	
	public Item GetNextItem()
	{
		if (itemList.Count > 1)
		{
			if (curIndex < itemList.Count-1)
			{
				return itemList[curIndex+1];
			}
			else
			{
				return itemList[0];
			}
		}
		
		return GetNullItem();
	}
	
	public Item GetPrevItem()
	{
		if (itemList.Count > 1)
		{
			if (curIndex > 0)
			{
				return itemList[curIndex-1];
			}
			else
			{
				return itemList[itemList.Count-1];
			}
		}
		
		return GetNullItem();
	}
	
	private Item GetNullItem()
	{
		Item nullItem = new NoItem();
		nullItem.Init();
		return nullItem;
	}
	
	public void CycleItem(bool cycleForward)
	{
		if (itemList.Count <= 1)
		{
			return;
		}
	
		if (cycleForward)
		{
			curIndex++;
			if (curIndex > itemList.Count-1)
			{
				curIndex = 0;
			}
		}
		else
		{
			curIndex--;
			if (curIndex < 0)
			{
				curIndex = itemList.Count-1;
			}
		}
		
		ResetText();
	}
}

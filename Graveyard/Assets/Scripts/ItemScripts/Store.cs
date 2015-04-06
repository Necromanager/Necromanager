using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Store : MonoBehaviour 
{
	private const string BUY_SOUND = "Sounds/Effects/buy";

	private const float X_SPACING = 80f;
	private const float Y_SPACING = 70f;
	private const int TEXT_SIZE = 20;
	private const int ITEMS_PER_ROW = 5;

	private List<Item> itemList = new List<Item>();
	private GUIStyle myStyle;
	
	void Start () 
	{
		FillList();
	}
	
	void Update () 
	{
	
	}
	
	private void SetupFont(int fontSize)
	{
		myStyle = new GUIStyle();
		myStyle.fontSize = fontSize;
		GUI.skin.label.fontSize = fontSize;
	}
	
	private void FillList()
	{
		//itemList.Add(new Shotgun());
		itemList.Add(new Coffee());
		itemList.Add(new PickAxe());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		itemList.Add(new NoItem());
		
		foreach (Item item in itemList)
		{
			item.Init();
		}
	}
	
	private Vector2 GetImageSize()
	{
		if (itemList.Count < 1)
		{
			return new Vector2(0,0);
		}
		
		Texture tempImage = itemList[0].GetStoreTexture();
		return new Vector2(tempImage.width,tempImage.height);
	}
	
	private float GetRowWidth(int column)
	{
		int itemNum = itemList.Count-(ITEMS_PER_ROW*column);
		if (itemNum > ITEMS_PER_ROW)
		{
			itemNum = ITEMS_PER_ROW;
		}
		
		float width = itemNum*GetImageSize().x;
		width += (itemNum-1)*X_SPACING;
		return width;
	}
	
	public void Draw(float startY)
	{
		SetupFont(TEXT_SIZE);
		
		int column = 0;	
		float startX = (Screen.width/2)-(GetRowWidth(column)/2);
		float drawX = startX;
		float drawY = startY;
		int itemsDrawn = 0;
		string description;
		
		foreach (Item item in itemList)
		{
			if (itemsDrawn == ITEMS_PER_ROW)
			{
				column++;
				startX = (Screen.width/2)-(GetRowWidth(column)/2);
				drawX = startX;
				drawY += Y_SPACING+GetImageSize().y;
			}
			
			if (GUI.Button(new Rect(drawX,drawY,GetImageSize().x,GetImageSize().y),item.GetStoreTexture()))
			{
				BuyItem(item);
			}
			
			description = item.GetStoreText();
			Vector2 textSize = myStyle.CalcSize(new GUIContent(description));
			float textX = drawX+(GetImageSize().x/2)-(textSize.x/2);
			float textY = drawY+GetImageSize().y;
			GUI.Label(new Rect(textX,textY,textSize.x,textSize.y*1.5f),description);
			
			description = item.GetDesc();
		    textSize = myStyle.CalcSize(new GUIContent(description));
			textX = drawX+(GetImageSize().x/2)-(textSize.x/2);
			textY = drawY+GetImageSize().y+textSize.y*1.5f;
			GUI.Label(new Rect(textX,textY,textSize.x,textSize.y*1.5f),description);
			
			drawX += X_SPACING+GetImageSize().x;
			itemsDrawn++;
		}
	}
	
	private void BuyItem(Item boughtItem)
	{
		if (boughtItem.IsSoldOut())
		{
			return;
		}
		
		if (GlobalValues.CanSpendMoney(boughtItem.GetCost()))
		{
			Debug.Log("Bought "+boughtItem.GetName());
			GlobalValues.SpendMoney(boughtItem.GetCost());
			PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
			player.GetItem(boughtItem);
			boughtItem.Buy();
			//GameObject.FindGameObjectWithTag("Main").GetComponent<SoundManager>().PlaySound(BUY_SOUND);
			GlobalFunctions.PlaySoundEffect(BUY_SOUND);
		}
		else
		{
			Debug.Log("Not enough money");
		}
	}
}

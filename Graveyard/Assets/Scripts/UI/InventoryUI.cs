using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryUI : UIElement
{
	private const float TEXT_TIME = 2.4f;

	[SerializeField] private Image nextItem;
	[SerializeField] private Image prevItem;
	[SerializeField] private Text description;
	
	private Image curItem;
	
	private float textAlpha;
	private float curTextTime;

	void Start () 
	{
		textAlpha = 1;
		curTextTime = TEXT_TIME;
		
		curItem = GetComponent<Image>();
	}
	 
	void Update () 
	{
		if (curTextTime < TEXT_TIME)
		{
			curTextTime += Time.deltaTime;
		}
		
		if ((curTextTime >= TEXT_TIME) && (textAlpha > 0))
		{
			textAlpha -= Time.deltaTime;
		}
		
		description.color = new Color(1,1,1,textAlpha);
	}
	
	public override void LoadElements()
	{
		if (curItem == null)
		{
			curItem = GetComponent<Image>();
		}
	}
	
	public override void SetDraw(bool shouldDraw)
	{
		//GetComponent<Image>().enabled = shouldDraw;
		curItem.enabled = shouldDraw;
		nextItem.enabled = shouldDraw;
		prevItem.enabled = shouldDraw;
		description.enabled = shouldDraw;
	}
	
	public void DisplayDescription()
	{
		textAlpha = 1;
		curTextTime = TEXT_TIME;
	}
	
	public void SetItems(Item current, Item next, Item previous)
	{
		curItem.sprite = current.GetSprite();
		curItem.color = current.GetHudColor(true);
		
		nextItem.sprite = next.GetSprite();
		nextItem.color = next.GetHudColor(false);
		
		prevItem.sprite = previous.GetSprite();
		prevItem.color = previous.GetHudColor(false);
		
		description.text = "-"+current.GetName()+"-\n"+current.GetDesc();
	}
}

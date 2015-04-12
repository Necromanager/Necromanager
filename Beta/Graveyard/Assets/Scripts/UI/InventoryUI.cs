using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryUI : UIElement
{
	private const float TEXT_TIME = 2.4f;
	[SerializeField] private Text description;


	[Header("Current")]
	[SerializeField] private Image currentItem;
	[SerializeField] private RectTransform currentBar;

	[Header("Next")]
	[SerializeField] private Image nextItem;
	[SerializeField] private RectTransform nextBar;

	[Header("Previous")]
	[SerializeField] private Image prevItem;
	[SerializeField] private RectTransform prevBar;

	private float textAlpha;
	private float curTextTime;

	void Start () 
	{
		textAlpha = 1;
		curTextTime = TEXT_TIME;
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

	}
	
	public override void SetDraw(bool shouldDraw)
	{
		//GetComponent<Image>().enabled = shouldDraw;
		currentItem.enabled = shouldDraw;
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
		currentItem.sprite = current.GetSprite();
		currentItem.color = current.GetHudColor(true);
		currentBar.localScale = new Vector3(currentBar.localScale.x,
		                                      current.GetCooldownPercent (),
		                                      currentBar.localScale.z);
		
		nextItem.sprite = next.GetSprite();
		nextItem.color = next.GetHudColor(false);
		nextBar.localScale = new Vector3(nextBar.localScale.x,
		                                   next.GetCooldownPercent (),
		                                   nextBar.localScale.z);

		prevItem.sprite = previous.GetSprite();
		prevItem.color = previous.GetHudColor(false);
		prevBar.localScale = new Vector3(prevBar.localScale.x,
		                                   previous.GetCooldownPercent (),
		                                   prevBar.localScale.z);

		description.text = "-"+current.GetName()+"-\n"+current.GetDesc();
	}
}

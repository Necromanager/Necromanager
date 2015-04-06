using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyUI : UIElement
{
	Image moneyBackground;
	Text moneyText;

	void Start () 
	{
		moneyBackground = GetComponent<Image>();
		moneyText = GetComponentInChildren<Text>();
	}
	
	void FixedUpdate () 
	{
		moneyText.text = GlobalValues.GetMoneyString();
	}
	
	public override void LoadElements()
	{
		if (moneyText == null)
		{
			moneyBackground = GetComponent<Image>();
			moneyText = GetComponentInChildren<Text>();
		}
	}
	
	public override void SetDraw(bool shouldDraw)
	{
		moneyText.enabled = shouldDraw;
		moneyBackground.enabled = shouldDraw;
	}
}

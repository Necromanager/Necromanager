using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NightUI : UIElement
{
	private Text nightText;

	void Start()
	{
		LoadElements();
	}
	
	void Update()
	{
		nightText.text = "Night "+GlobalValues.day;
	}
	
	public override void LoadElements()
	{
		nightText = GetComponent<Text>();
	}
	
	public override void SetDraw(bool shouldDraw)
	{
		nightText.enabled = shouldDraw;
	}
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BackgroundUI : UIElement 
{
	[SerializeField] List<Image> childImages;
	
	private Image background = null;

	void Start()
	{
		LoadElements();
	}

	public override void LoadElements()
	{
		background = GetComponent<Image>();
	}
	
	public override void SetDraw(bool shouldDraw)
	{
		Debug.Log("Background: "+shouldDraw);
		background.enabled = shouldDraw;
		
		foreach (Image image in childImages)
		{
			image.enabled = shouldDraw;
		}
	}
}

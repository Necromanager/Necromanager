using UnityEngine;
using System.Collections;

public class SplashScreen : NewMenu {

	[SerializeField] float maxTime;
	float currentTime;

	public override void Update ()
	{
		base.Update ();
		if(isOpen)
		{
			currentTime -= Time.deltaTime;

			if(Input.anyKeyDown)
			{
				currentTime = 0;
			}

			if(currentTime <= 0)
			{
				currentTime = 0;
				isOpen = false;
			}
		}

	}

	public override void onShow()
	{
		base.onShow ();
		currentTime = maxTime;
	}
	public override void onHide()
	{
		base.onHide ();
	}


}

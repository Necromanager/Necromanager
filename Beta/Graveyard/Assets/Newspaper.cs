using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Newspaper : MonoBehaviour 
{
	[SerializeField] public Text headline;
	[SerializeField] public Text story1;
	[SerializeField] public Text story2;
	[SerializeField] public Text storyPay;
	[SerializeField] public Image picture;

	Animator anim;

	void Start()
	{
		anim = GetComponent<Animator> ();
	}

	public void endNight(bool goodNight)
	{
		if(goodNight)
		{
			anim.SetTrigger("GoodNight");
		}
		else
		{
			anim.SetTrigger("BadNight");
		}
	}

	public void toStore()
	{
		anim.SetTrigger("ToStore");
	}


	public void playThud()
	{
		GlobalFunctions.PlaySoundEffect ("Sounds/Effects/Thud");
	}

	public void playWhoosh()
	{
		GlobalFunctions.PlaySoundEffect ("Sounds/Effects/Miss");
	}
}

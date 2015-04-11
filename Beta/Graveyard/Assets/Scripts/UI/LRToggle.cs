using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LRToggle : MonoBehaviour {

	protected Selectable me;
	protected EventSystem es;
	protected bool moved = false;

	protected virtual void Start ()
	{
		me = GetComponent<Selectable> ();
		es = EventSystem.current;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(es.currentSelectedGameObject == gameObject)
		{
			if(InputMethod.getAxisRaw("Horizontal") < 0 && !moved)
			{
				handleLeft ();
				moved = true;
				GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.moveCurser);
			}
			else if(InputMethod.getAxisRaw("Horizontal") > 0 && !moved)
			{
				handleRight ();
				moved = true;
				GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.moveCurser);
			}

			if(InputMethod.getAxisRaw("Horizontal") == 0)
			{
				moved = false;
			}
		}
	}

	protected virtual void handleLeft(){}
	protected virtual void handleRight(){}
}

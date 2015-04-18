using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class DingDong : MonoBehaviour {

	public Transform target;
	Vector3 screenSpace;
	public WorldDingDong wdd;
	GameObject parentBody;

	RectTransform rec;

	CanvasGroup cg;
	public bool offScreen = false;

	public float maxAge = 3f;
	public float currentAge;
	public bool kill = false;

	void Start()
	{
		currentAge = maxAge;
		cg = GetComponent<CanvasGroup> ();
		rec = GetComponent<RectTransform>();
	}

	public void init(Transform t, WorldDingDong w, GameObject p)
	{
		target = t;
		GameObject myParent = GameObject.FindGameObjectWithTag ("SoundUI");
		transform.SetParent (myParent.transform);
		wdd = w;
		parentBody = p;
	}

	void Update()
	{
		if (kill)
			killChildren ();

		else if (currentAge < maxAge) // bell is ringing
		{
			currentAge += Time.deltaTime;
			if(Camera.main != null)
			{
				screenSpace = Camera.main.WorldToScreenPoint (target.position);
				if(checkOffScreen ())
				{
					cg.alpha = 1f - (currentAge/maxAge);
					wdd.setAlpha(0);
					Vector3 pos = new Vector3(Mathf.Clamp(screenSpace.x, 0, Screen.width),
					                      Mathf.Clamp(screenSpace.y, 0, Screen.height),
					                      0);
					rec.position = pos;
				}
				else
				{
					cg.alpha = 0;
					wdd.setAlpha(1f - (currentAge/maxAge));
				}
			}
		}
		else
		{
			currentAge = maxAge;
			cg.alpha = 0;
			wdd.setAlpha(0);
		}
	}

	bool checkOffScreen()
	{
		offScreen = false;

		if(screenSpace.x < 0.0f)
		{
			//Debug.Log ("Offscreen to left!");
			offScreen = true;
		}
		else if(screenSpace.x > Screen.width)
		{
			//Debug.Log ("Offscreen to right!");
			offScreen = true;
		}

		if(screenSpace.y < 0)
		{
			//Debug.Log ("Offscreen to bottom!");
			offScreen = true;
		}
		/*else if(screenSpace.y > Screen.height)
		{
			//Debug.Log ("Offscreen to top!");
			offScreen = true;
		}*/

		return offScreen;
	}

	public void ring()
	{
		currentAge = 0;
	}

	public void killChildren()
	{
		Debug.Log ("Killing dd");
		if (wdd != null) {GameObject.Destroy (wdd.gameObject);}
		if(parentBody != null){GameObject.Destroy(parentBody);}
		GameObject.Destroy (gameObject);
	}
}

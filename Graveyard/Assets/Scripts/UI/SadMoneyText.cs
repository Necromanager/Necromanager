using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SadMoneyText : MonoBehaviour {

	RectTransform rt;
	Text t;
	
//	float money = 0;
	float alpha = 2f;

	Color positive = Color.green;
	Color negative = Color.red;

	void Awake ()
	{
		rt = GetComponent<RectTransform> ();
		t = GetComponent<Text> ();
	}
	
	public void init(float m)
	{
	//	money = m;
		
		if (m > 0)
			t.color = positive;
		else
			t.color = negative;
		
	}
	
	void FixedUpdate ()
	{
		rt.position = new Vector3 (rt.position.x, rt.position.y - 30 * Time.deltaTime, 0);
		alpha -= 0.5f * Time.deltaTime;
		t.color = new Color (t.color.r, t.color.g, t.color.b, alpha);
		if(alpha < 1)
			rt.localScale = new Vector3(alpha, alpha, alpha);
		else
			rt.localScale = new Vector3(1, 1, 1);

		if (alpha <= 0f)
		{
			Destroy(gameObject);
		}
	}
}



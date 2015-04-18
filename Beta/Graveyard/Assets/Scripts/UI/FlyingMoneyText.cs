using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FlyingMoneyText : MonoBehaviour {

	Vector3 target;
	RectTransform rt;
	Text t;

	//float money = 0;

	Color positive = Color.green;
	Color negative = Color.red;

	float minScale = 0.1f;
	float maxDist = -1;

	void Awake ()
	{
		rt = GetComponent<RectTransform> ();
		t = GetComponent<Text> ();
		rt.localScale = new Vector3 (minScale, minScale, minScale);
	}

	public void init(Vector3 targ, float m)
	{
		target = targ;
		//money = m;

		if (m > 0)
			t.color = positive;
		else
			t.color = negative;

	}

	void Update ()
	{
		if(maxDist == -1)
			maxDist = Vector3.Distance (rt.position, target);

		float dist = Vector3.Distance (rt.position, target);
		float scaler = dist/maxDist;

		scaler = 1f - scaler;

		scaler = Mathf.Clamp (scaler, minScale, 1);

		rt.localScale = new Vector3 (scaler, scaler, scaler);

		float step = Time.deltaTime * 1000f;
		rt.position = Vector3.MoveTowards (rt.position,
		                                   target, step);

		if (dist < 1f)
		{
			GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.purchaseItem);
			Destroy(gameObject);
		}
	}
}

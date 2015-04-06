using UnityEngine;
using System.Collections;

public class Grow : MonoBehaviour {

	float maxScale = 1.2f;
	float currentScale;
	RectTransform rec;
	float growthRate = 1;

	// Use this for initialization
	void Start () {
		rec = GetComponent<RectTransform>();
		currentScale = rec.localScale.x;
	}
	
	// Update is called once per frame
	void Update ()
	{
		currentScale += Time.deltaTime * growthRate;

		if (currentScale > maxScale)
			currentScale = 0;

		rec.localScale = new Vector3(currentScale, currentScale, currentScale);
	}
}

using UnityEngine;
using System.Collections;

public class WorldDingDong : MonoBehaviour {

	[SerializeField] DingDong screenWaves;
	CanvasGroup cg;
	Transform tf;

	// Use this for initialization
	void Start () {
		cg = GetComponent<CanvasGroup>();
		tf = screenWaves.target;

	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = tf.position;
		/*if (screenWaves.offScreen || screenWaves.currentAge >= screenWaves.maxAge)
			cg.alpha = 0;
		else
			cg.alpha = 1;*/
	}

	public void setAlpha(float a)
	{
		cg.alpha = a;
	}
}

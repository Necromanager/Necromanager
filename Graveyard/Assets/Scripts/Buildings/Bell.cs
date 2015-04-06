using UnityEngine;
using System.Collections;

public class Bell : MonoBehaviour 
{


	DingDong dd;

	void Start ()
	{
		GameObject gdd = GameObject.Instantiate (Resources.Load ("Prefabs/UI/DingDong")) as GameObject;
		if (gdd == null)
			Debug.Log ("gdd is NULL!!!");

		dd = gdd.GetComponentInChildren<DingDong>();
		if (dd == null)
			Debug.Log ("dd is NULL!!!");

		WorldDingDong wdd = gdd.GetComponentInChildren<WorldDingDong>();
		if(wdd == null)
			Debug.Log ("wdd is NULL!!!");

		dd.init (transform, wdd, gdd);
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void RingBell()
	{
		GlobalFunctions.PlaySoundEffect (SoundEffectLibrary.bellRing);
		dd.ring ();
	}

	void OnDestroy()
	{
		dd.kill = true;
	}
}

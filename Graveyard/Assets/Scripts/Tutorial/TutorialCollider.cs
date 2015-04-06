using UnityEngine;
using System.Collections;

public class TutorialCollider : MonoBehaviour {

	public bool triggered = false;

	void OnTriggerEnter(Collider other)
	{
		triggered = true;
	}
}

using UnityEngine;
using System.Collections;

public class PlayerGraveHitbox : MonoBehaviour 
{
	[SerializeField] PlayerScript player;

	void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Grave")
		{
			player.SetOnGrave(true, col.gameObject.GetComponent<Grave>());
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Grave")
		{
			player.SetOnGrave(false, col.gameObject.GetComponent<Grave>());
		}
	}
}

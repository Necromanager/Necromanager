using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectableButton : MonoBehaviour, ISelectHandler {

	Selector selector;

	[SerializeField]
	Building myBuilding;

	protected void Awake()
	{
		selector = GameObject.FindGameObjectWithTag("Selector").GetComponent<Selector>();
	}

	public void OnSelect(BaseEventData eventData)
	{
		Debug.Log ("Button Selected!");
		selector.ChangeBuilding (myBuilding);
	}
}

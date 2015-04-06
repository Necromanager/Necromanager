using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ConfirmScreen : MonoBehaviour {


	StoreItem item;

	[SerializeField]
	Text itemName;
	[SerializeField]
	Text itemPrice;
	[SerializeField]
	Text itemDescription;
	[SerializeField]
	Image itemPic;
	NewMenuManager manager;
	[SerializeField]
	Button button;

	// Use this for initialization
	void Start () {
		manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<NewMenuManager>();
		//hide ();
	}
	
	public void setItem(StoreItem newItem)
	{
		item = newItem;
		itemName.text = item.getName ();
		itemPrice.text = "$" + item.getPrice().ToString();
		itemDescription.text = item.getDescription ();
		itemPic.sprite = item.getImage ();
		//show ();
	}

	public void buyItem()
	{
		if(!item.canBuy())
		{
			item.playSoldOut();
		}
		else
		{
			item.buy ();
			hide ();
			manager.setSelected(button);
		}
	}

	public void hide()
	{
		CanvasGroup group = GetComponent<CanvasGroup> ();
		group.alpha = 0;
		group.blocksRaycasts = false;
		group.interactable = false;
	}

	public void show()
	{
		CanvasGroup group = GetComponent<CanvasGroup> ();
		group.alpha = 1;
		group.blocksRaycasts = true;
		group.interactable = true;
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StoreItem : MonoBehaviour {

	[SerializeField]
	protected string itemName= "Placeholder";
	protected int cost = 400000;
	protected string description = "What is this thing anyways?";

	[SerializeField]
	protected Text titleText;
	[SerializeField]
	protected Text priceText;
	protected Sprite soldOutSprite;
	protected Sprite itemSprite;

	protected Image pic;
	protected ConfirmScreen cnfrm;
	//Button myButton;

	protected bool soldOut = false;

	protected Item item;
	
	//protected AudioClip soldOutClip;
	//protected AudioClip buyClip;
	protected NewMenuManager manager;
	protected EventSystem es;

	bool shouldUpdateConfirm = true;

	public virtual void Start()
	{
		es = GameObject.FindGameObjectWithTag ("EventSystem").GetComponent<EventSystem>();
		init();
	}

	protected void init()
	{
		pic = GetComponent<Image> ();
		//myButton = GetComponent<Button> ();

		cnfrm = GameObject.FindGameObjectWithTag ("StoreDescription").GetComponent<ConfirmScreen>();
		manager = GameObject.FindGameObjectWithTag("UIManager").GetComponent<NewMenuManager>();

		LoadData ();

		titleText.text = itemName;
		priceText.text = "$" + cost.ToString ();

	}

	protected void LoadData()
	{
		GameObject main = GameObject.FindGameObjectWithTag ("Main");
		ItemValues itemVals = main.GetComponent<ItemValues> ();
		ItemData itemData = itemVals.GetData (itemName);

		description = itemData.description;
		cost = itemData.cost;
		itemSprite = itemData.storeSprite;
		soldOutSprite = Resources.Load<Sprite>("Sprites/Store/StoreSoldOut");
		pic.sprite = itemSprite;
		item = getItemFromName (itemName);
	}

	public virtual void Update()
	{

		if(es.currentSelectedGameObject == gameObject)
		{
			if(InputMethod.getInputCode() != InputModeCode.CONTROLLER)
			{
				es.SetSelectedGameObject(null);
			}
			else if(shouldUpdateConfirm)
			{
				cnfrm.setItem(this);
				shouldUpdateConfirm = false;
			}
		}
		else
		{
			shouldUpdateConfirm = true;
		}
	}

	public void buy()
	{
		if (soldOut)
		{
			GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.soldOut);
			return;
		}

		if (GlobalValues.CanSpendMoney(cost))
		{
			Debug.Log("Bought " + itemName);
			GlobalValues.SpendMoney(cost);
			Results.moneySpent += cost;
			if(item != null)
			{
				PlayerScript player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerScript>();
				player.GetItem(item);
			}
			soldOut = true;
			pic.sprite = soldOutSprite;
			//GameObject.FindGameObjectWithTag("Main").GetComponent<SoundManager>().PlaySound(BUY_SOUND);
			GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.purchaseItem);
		}
		else
		{
			GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.soldOut);
		}
	}

	public string getName(){return itemName;}
	public int getPrice(){return cost;}
	public string getDescription(){return description;}
	public Sprite getImage(){return pic.sprite;}
	/*
	public void onItemClick()
	{
		if(!soldOut)
		{
			cnfrm.setItem (this);
			//manager.setSelected(button);
		}
		else
		{
			playSoldOut();
		}
	}
	*/
	public bool isSoldOut(){return soldOut;}
	public void playSoldOut(){GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.soldOut);}
	public bool canBuy(){return cost <= GlobalValues.money;}

	public void playSelected()
	{
		GlobalFunctions.PlaySoundEffect (SoundEffectLibrary.moveCurser);
		Debug.Log ("Played select noise");
	}
	public void playInteract()
	{
		GlobalFunctions.PlaySoundEffect (SoundEffectLibrary.menuSelect);
		Debug.Log ("Played confirm noise");
	}

	public Item getItemFromName(string name)
	{
		Item newItem = null;

		switch(name)
		{
			case "Coffee":
				newItem = new Coffee();
			break;
			case "Pick Axe":
				newItem = new PickAxe();
			break;
			case "Chewing Gum":
				newItem = new ChewingGum();
			break;
			case "Hair Gel":
				newItem = new HairGel();
			break;
			case "Scream Box":
				newItem = new ScreamBox();
			break;
		}

		return newItem;
	}
}

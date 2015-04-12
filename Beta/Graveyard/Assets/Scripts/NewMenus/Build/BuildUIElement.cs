using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BuildUIElement : MonoBehaviour {
	

	[SerializeField]
	BuildingID ID;

	Building building;
	Selector selector;

	Text myText;
	[SerializeField]
	Image myPic;
	[SerializeField]
	Image myBG;

	void Awake()
	{
		myText = GetComponentInChildren<Text> ();
		selector = GameObject.FindGameObjectWithTag ("Selector").GetComponent<Selector>();

		switch (ID)
		{

		case BuildingID.FLOWERS:
			building = new Barricade();
			break;
		case BuildingID.BRAIN:
			building = new BrainStick();
			break;
		case BuildingID.TUNNEL:
			building = new Tunnel();
			break;
		case BuildingID.BELL:
			building = new GraveBell();
			break;
		case BuildingID.SPOTLIGHT:
			building = new SpotlightBuilding();
			break;
		default:
			building = new Barricade();
			break;
		}

		myPic.sprite = building.GetTexture ();
		myText.text = building.GetName() + " - " + building.GetCost();

	}

	public void setColor(Color c)
	{
		myBG.color = c;
		myPic.color = c;
	}

	public void updateSelector()
	{
		selector.ChangeBuilding (building);
	}

	public string updateDescription()
	{
		return building.GetDesc ();
	}
	
}

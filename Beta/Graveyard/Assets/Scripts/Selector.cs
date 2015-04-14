using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Selector : MonoBehaviour 
{
	private const float MAX_CAMERA_SIZE = 20;
	private const float MIN_CAMERA_SIZE = 5;
	private const float CAMERA_ZOOM_SPEED = 0.4f;
	private const float BUTTON_SPACING = 20;

	[SerializeField] private float movementSpeed;
	[SerializeField] private float transparency;
	[SerializeField] private Color openColor = Color.blue;
	[SerializeField] private Color closedColor = Color.red;
	[SerializeField] private Color removeColor = Color.yellow;
	
	private List<Building> buildings;
	
	private Building currentBuilding;
	private bool spaceOpen;
	private bool onMenu;
	[SerializeField]
	private Tile currentTile;
	private SelectorCamera playerCamera;

	[SerializeField]
	private float ControllerDelayTime = 0.1f;
	private float currentDelay = 0;

	void Start () 
	{
		currentBuilding = new Barricade();
		currentTile = null;
		AddBuildings();
		spaceOpen = false;
		//canMove = true;
		playerCamera = Camera.main.GetComponent<SelectorCamera>();
		SetColor(true);
		CheckOnMenu();
		CheckSpaceOpen();
	}
	/*void FixedUpdate()
	{
		if (!onMenu)
		{


		}
	}*/

	void Update () 
	{
		CheckOnMenu();
		if(!onMenu)
		{

			switch(InputMethod.getInputCode())
			{
			case InputModeCode.KEYBOARD_AND_MOUSE:
				UpdateMovementKBM();
				break;
			case InputModeCode.CONTROLLER:
				UpdateMovementController();
				break;
			default:
				UpdateMovementKBM();
				break;
			}
			

			if(InputMethod.getButtonDown("Build"))
			{
				PlaceBuilding();
			}
			else if (InputMethod.getButtonDown("Sell"))
			{
				RemoveBuilding();
			}
		}
	}
	
	void OnGUI()
	{
		/*
		DrawBorder();
		float drawX = 0;
		float drawY = Screen.height-currentBuilding.GetTextureSize().y*1.1f;
		
		GUI.color = Color.white;
		foreach (Building ob in buildings)
		{
			if (ob.DrawAndCheckButton(new Vector2(drawX,drawY)))
			{
				ChangeBuilding(ob);
			}
			
			drawX += BUTTON_SPACING+currentBuilding.GetTextureSize().x*1.1f;
		}
		*/
	}
	
/*	private void DrawBorder()
	{
		float borderHeight = currentBuilding.GetTextureSize().y*1.5f;
		float drawY = Screen.height-borderHeight;
		Rect drawRect = new Rect(0,drawY,Screen.width,borderHeight);
		Texture borderTex = Resources.Load("Textures/Blank") as Texture;
		
		GUI.color = new Color(0,0,0,0.4f);
		GUI.DrawTexture(drawRect,borderTex);
	}*/
	
	private void AddBuildings()
	{
		buildings = new List<Building>();
		buildings.Add(new Barricade());
		buildings.Add(new BrainStick());
	}
	
	public void RemoveCamera()
	{
		DestroyImmediate(playerCamera.gameObject);
	}
	
	private void UpdateMovementKBM()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		
		GameObject ob;
		Vector3 obPos;

		/*
		vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
			This part needs to be redone.
			It causes immense lag.
		vvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvvv
		*/


		RaycastHit[] hits = Physics.RaycastAll(ray,1000f);
		foreach(RaycastHit hit in hits)
		{
			//Debug.Log("You hit: "+hit.collider.gameObject);
			ob = hit.collider.gameObject;
			if (ob.tag == "Floor")
			{
				obPos = ob.transform.position;
				transform.position = new Vector3(obPos.x,transform.position.y,obPos.z);
				CheckSpaceOpen();
				SetColor();
				//canMove = true;
				break;
			}
		}

		/*
		^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
			This part needs to be redone.
			It causes immense lag.
		^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
		*/
	}

	private void UpdateMovementController()
	{

		if(currentDelay > 0)
		{
			currentDelay -= Time.deltaTime;
		}
		else
		{
			currentDelay = 0;

			if(InputMethod.isAxisMoving("Horizontal") || InputMethod.isAxisMoving("Vertical"))
			{
				currentDelay = ControllerDelayTime;
				//move the derp
				Vector3 nextPos = new Vector3(InputMethod.getAxisRaw("Horizontal"),
											  0,
				                              InputMethod.getAxisRaw("Vertical"));

				Vector3 pos = transform.position;
				if (((nextPos.x + pos.x) > GlobalValues.upperBounds.x) || 
				    ((nextPos.x + pos.x) < GlobalValues.lowerBounds.x))
				{
					nextPos = new Vector3(0, 0, nextPos.z);
				}
				if (((nextPos.z + pos.z) > GlobalValues.upperBounds.y) || 
				    ((nextPos.z + pos.z) < GlobalValues.lowerBounds.y))
				{
					nextPos = new Vector3(nextPos.x, 0, 0);
				}
				
				transform.position += nextPos;

				//check out the new space
				CheckSpaceOpen();
				SetColor();
			}
		}
	}

	public void ChangeBuilding(Building newBuilding)
	{
		currentBuilding = newBuilding;
	}
	
	private void PlaceBuilding()
	{
		bool enoughMoney = GlobalValues.CanSpendMoney(currentBuilding.GetCost());
		CheckSpaceOpen();
		if (spaceOpen && enoughMoney)
		{
			//GameObject newBuilding = GameObject.Instantiate(Resources.Load("Prefabs/Barricade")) as GameObject;
			GameObject newBuilding = GameObject.Instantiate(currentBuilding.GetCreateObject()) as GameObject;
			newBuilding.transform.position = transform.position;
			currentTile.SetOpen(!currentBuilding.ShouldCloseSpace());
			currentTile.AddBuilding(newBuilding,currentBuilding.GetCost());
			
			/*foreach (Tile tile in currentTile.GetAdjacent())
			{
				tile.FillNeighborList();
			}*/
		
			GlobalValues.SpendMoney(currentBuilding.GetCost());
			Results.moneySpent += currentBuilding.GetCost();
			GlobalFunctions.PlaySoundEffect(currentBuilding.GetBuildSound());
			CheckSpaceOpen();
			SetColor();
		}
		else
		{
			Debug.Log("Build Failed | Space:" + spaceOpen + " Money:" + enoughMoney);
		}
	}
	
	private void RemoveBuilding()
	{
		if (CheckCanRemove())
		{
			GlobalValues.AddMoney(currentTile.GetBuildingCost());
			Results.moneySpent -= currentBuilding.GetCost();
			currentTile.RemoveBuilding();
			currentTile.SetOpen(true);
			
			foreach (Tile tile in currentTile.GetNeighbors())
			{
				tile.FillNeighborList();
			}
			
			CheckSpaceOpen();
			SetColor();
			GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.removeBuilding);
		}
	}
	
	private void SetColor(bool open)
	{
		if (open)
		{
			GetComponent<Renderer>().material.color = new Color(openColor.r,openColor.g,openColor.b,transparency);
		}
		else
		{
			GetComponent<Renderer>().material.color = new Color(closedColor.r,closedColor.g,closedColor.b,transparency);
		}
	}
	
	private void SetColor()
	{
		if (CheckCanRemove())
		{
			gameObject.GetComponent<Renderer>().material.color = new Color(removeColor.r,removeColor.g,removeColor.b,transparency);
		}
		else if (spaceOpen)
		{
			gameObject.GetComponent<Renderer>().material.color = new Color(openColor.r,openColor.g,openColor.b,transparency);
		}
		else
		{
			gameObject.GetComponent<Renderer>().material.color = new Color(closedColor.r,closedColor.g,closedColor.b,transparency);
		}
	}
	
	private void GetCurrentTile()
	{
		Vector3 spherePos = new Vector3(transform.position.x,transform.position.y-1,transform.position.z);
		Collider[] below = Physics.OverlapSphere(spherePos,0.4f);
		
		spaceOpen = true;
		
		foreach (Collider ob in below)
		{
			if (ob.tag == "Floor")
			{
				currentTile = ob.gameObject.GetComponent<Tile>();
				return;
			}
		}
		
		currentTile = null;
	}
	
	private bool CheckCanRemove()
	{
		if (currentTile == null)
		{
			return false;
		}
	
		bool hasBuilding = currentTile.HasBuilding();
		
		if (hasBuilding)
		{
			GameObject building = currentTile.GetBuilding();
			Spotlight spotLight = building.GetComponent<Spotlight>();
			if (spotLight != null)
			{
				return false;
			}
		}
	
		return hasBuilding;
	}
	
	private void CheckOnMenu()
	{
		//float borderHeight = currentBuilding.GetTextureSize().y*1.5f;
		//float maxY = borderHeight;
		
		onMenu = false;//(Input.mousePosition.y <= 256);
		spaceOpen = false;
	}
	
	private void CheckSpaceOpen()
	{
		GetCurrentTile();
		if (currentTile == null)
		{
			return;
		}
		
		if (currentTile.IsGateSpot())
		{
			spaceOpen = false;
			//Debug.Log("There is a gate here...");
			return;
		}
		if (currentTile.HasBuilding())
		{
			spaceOpen = false;
			//Debug.Log("You built something here already...");
			return;
		}

		if (currentBuilding.GetName() == "Grave Bell")
		{
			spaceOpen = currentTile.CheckGraveOpen();
		}
		else
		{
			spaceOpen = currentTile.IsOpen();
		}
		
		if (spaceOpen)
		{
			spaceOpen = currentBuilding.IsSpaceUsable(currentTile);
			//Debug.Log(spaceOpen + ": You can Build Here!");
		}
		else{//Debug.Log("The tile isn't open...");
		}
	}
}

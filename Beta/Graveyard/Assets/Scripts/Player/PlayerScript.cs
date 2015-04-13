using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour
{
	[SerializeField] private float walkSpeed;
	[SerializeField] private float carrySpeed;
	//[SerializeField] private bool onGrave;
	[SerializeField] private ZombieScript grabbedZombie;
    [SerializeField] private GameObject zombieAttachPoint;

	private float maxVelocityChange;
	private float originalSpeed;
	//private float money;
	private Vector3 targetVelocity;

	private float swingEffectDelay;
	private float timeToSwingEffect;

	
	//private Grave grave;
	private Shovel shovel;
	private Inventory inventory;
	private bool holdingZombie;
	//private bool isMoving;
	private Direction direction;
	
	private Animator animator;

    private Animator animator3D;
    private GameObject characterModel;
	
	private List<Grave> targetedGraves;
	
	private Vector3 startPos;
	//private Quaternion startRot;


	void Awake () 
	{
		targetedGraves = new List<Grave> ();

		maxVelocityChange = 15.0f;	
		originalSpeed = walkSpeed;
		targetVelocity = Vector3.zero;
		grabbedZombie = null;
		//grave = null;
		inventory = new Inventory();
		//GetItem(new PickAxe());

		swingEffectDelay = 0.38f;
		timeToSwingEffect = -1.0f;

		shovel = new Shovel();
		shovel.Init();
		holdingZombie = false;
		//onGrave = false;
		//isMoving = false;
		direction = Direction.RIGHT;
		
		//animator = GetComponentInChildren<Animator>();
		//animator.SetBool("CurrentlyMoving",isMoving);
		//animator.transform.eulerAngles = new Vector3(30,0,0);
		//Vector3 animPos = animator.transform.position;
		//Vector3 cameraPos = Camera.main.transform.position;
		//animator.transform.rotation = Quaternion.LookRotation(animPos - cameraPos);

        animator3D = GetComponentsInChildren<Animator>()[0];
        characterModel = animator3D.gameObject;
        
        startPos = transform.position;
        //startRot = transform.rotation;

		//animator.transform.localScale = new Vector3(-1,.0f,0);
	}
	
	void Update()
	{	
		shovel.UpdateItem(true);
		UpdateInputs();
		UpdateAnimation();
		UpdateHeldZombie();
		inventory.UpdateItems();
	}
	
	public void Reset()
	{
		holdingZombie = false;
		transform.position = startPos;
		//transform.rotation = startRot.eulerAngles;
		inventory.Reset();

	}
	
	void OnGUI()
	{
		//Texture clockTex = Resources.Load("Textures/GUI/ClockFace_resized") as Texture;
	
		inventory.Draw();
	}
	
	void FixedUpdate () 
	{
		UpdateMovement();
		//UpdateAnimation();
	}

	private void UpdateInputs()
	{
		bool shouldSwing = false;
		//if (InputMethod.getAxis("SwingShovel") > 0)
		//if (InputMethod.getInputCode() == InputModeCode.CONTROLLER)
		switch(InputMethod.getInputCode())
		{
		case InputModeCode.CONTROLLER:
			shouldSwing = InputMethod.getAxis("SwingShovel") > 0;
			break;
		case InputModeCode.KEYBOARD_AND_MOUSE:
			shouldSwing = InputMethod.getButtonDown("SwingShovel");
			break;
		}

		if (timeToSwingEffect > 0.0f)
		{
			timeToSwingEffect -= Time.deltaTime;

			if (timeToSwingEffect <= 0.0f)//If we just reached the time to trigger the swing
			{
				shovel.Activate(this);
			}
		}
		
		if (shouldSwing && !GlobalValues.paused)
		{
			if (shovel.CanUse() && !holdingZombie)
			{
				timeToSwingEffect = swingEffectDelay;
				//shovel.Activate(this);
				//shovel.SetCooldown(0);
				shovel.SetCooldown(0);
				animator3D.SetTrigger("Swing");
			}
		}
		else if (InputMethod.getButtonDown("Interact"))
		{
			Interact();
		}
		else if (InputMethod.getButtonDown("Item"))
		{
			UseItem();
		}
		
		if (Time.timeScale != 0)
		{
			if (InputMethod.getButton("Fast Forward"))
			{
				Time.timeScale = 7;
			}
			else
			{
				Time.timeScale = 1;
			}
		}
		
		if (InputMethod.getButtonDown("Next Item"))
		{
			CycleInventory(true);
		}
		else if (InputMethod.getButtonDown("Prev Item"))
		{
			CycleInventory(false);
		}
	}
	
	private void UpdateHeldZombie()
	{
        if (holdingZombie)
        {
            //Vector3 pos = transform.position;
            //grabbedZombie.SetPosition(new Vector3(pos.x, pos.y + 1.5f, pos.z));
			//grabbedZombie.transform.position = new Vector3(pos.x, pos.y + 1.5f, pos.z);
			if (grabbedZombie != null)
			{
				grabbedZombie.transform.localPosition = Vector3.zero;
			}
        }
	}
	
	private void UpdateMovement()
	{	

		if ((animator3D.GetCurrentAnimatorStateInfo(0).IsName("UseItem")) ||
			(!shovel.CanUse()))
		{
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			//animator3D.SetTrigger("Stop");
			//isMoving = false;
			return;
		}

		//Set direction
		if (InputMethod.getAxis("Horizontal") < 0)
		{
			direction = Direction.LEFT;
		}
		else if (InputMethod.getAxis("Horizontal") > 0)
		{
			direction = Direction.RIGHT;
		}
	
		//Get Movement Speed
		targetVelocity = new Vector3(InputMethod.getAxis("Horizontal"),
		                             0,
		                             InputMethod.getAxis("Vertical"));

		targetVelocity = transform.TransformDirection(targetVelocity);
		if (holdingZombie)
		{
			targetVelocity *= carrySpeed;
		}
		else
		{
			targetVelocity *= walkSpeed;
		}
		
		Vector3 velocity = GetComponent<Rigidbody>().velocity;
		Vector3 velocityChange = (targetVelocity - velocity);
		velocityChange.x = Mathf.Clamp(velocityChange.x,-maxVelocityChange,maxVelocityChange);
		velocityChange.z = Mathf.Clamp(velocityChange.z,-maxVelocityChange,maxVelocityChange);
		velocityChange.y = 0;
		GetComponent<Rigidbody>().AddForce(velocityChange,ForceMode.VelocityChange);
		
		//Movement script base:
		//http://answers.unity3d.com/questions/625205/rigid-body-movement-c.html
	}
	
	private void UpdateAnimation()
	{
		//bool hMovement = InputMethod.getAxis("Horizontal") != 0;
		//bool vMovement = InputMethod.getAxis("Vertical") != 0;
		
		/*if (InputMethod.getAxis("Horizontal") < 0)
		{
			//animator.transform.localScale = new Vector3(-1,.0f,0);
		}
		else if (InputMethod.getAxis("Horizontal") > 0)
		{
			//animator.transform.localScale = new Vector3(1,0.0f,0);
		}*/
		
		//isMoving = hMovement || vMovement;
		
//		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Idle") && isMoving)
//		{
//			//Debug.Log("Moving");
//			//animator.SetTrigger("Move");
//		}
//		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Walk") && !isMoving)
//		{
//			//Debug.Log("Stopping");
//			//animator.SetTrigger("Stop");
//		}
		/*if ((hMovement || vMovement) != isMoving)
		{
			isMoving = hMovement || vMovement;
			
			if (isMoving)
			{
				animator.SetTrigger("Move");
			}
			else
			{
				animator.SetTrigger("Stop");
			}
		}*/
		
		//animator.SetBool("CurrentlyMoving",isMoving);
        //animator3D.SetBool("CurrentlyMoving", isMoving);

        if (targetVelocity.sqrMagnitude > 0.1f)
        {
            Vector3 targetVelNorm = targetVelocity.normalized;

            characterModel.transform.localRotation = Quaternion.AngleAxis(Mathf.Atan2(targetVelNorm.z, -targetVelNorm.x) * Mathf.Rad2Deg - 90.0f, Vector3.up);

            animator3D.SetFloat("Speed", 0.0f);
        }
        else
            animator3D.SetFloat("Speed", 1.0f);



		

		//animator3D.SetLayerWeight(1, holdingZombie ? 1 : 0);
		animator3D.speed = holdingZombie ? (carrySpeed / walkSpeed) * 0.9f : 0.9f;
	}
	
	private void Interact()
	{
		if (holdingZombie)
		{
			DropZombie();
		}
		else
		{
			GrabZombie();
		}
	}
	
	public void CycleInventory(bool cycleForward)
	{
		inventory.CycleItem(cycleForward);
	}
	
	public void SetVisible(bool visible)
	{
		//animator.GetComponent<Renderer>().enabled = visible;
		inventory.SetVisible(visible);
	}
	
	public Direction GetDirection()
	{
		return direction;
	}
	
	public float GetSpeed()
	{
		return walkSpeed;
	}
	
	public float GetOriginalSpeed()
	{
		return originalSpeed;
	}
	
	public void SetSpeed(float newSpeed)
	{
		walkSpeed = newSpeed;
	}
	
	public void GetItem(Item newItem)
	{
		newItem.Init();
		inventory.AddItem(newItem);
	}
	
	public Inventory GetInventory()
	{
		return inventory;
	}
	
	public void LoseItem(Item lostItem)
	{
		inventory.LoseItem(lostItem);
	}
	
	private void UseItem()
	{
		Item curItem = inventory.GetCurItem();
		
		if (curItem.CanUse() && !holdingZombie && GlobalValues.inGame)
		{
			if (curItem.IsUsable(this))
			{
				curItem.Activate(this);
				curItem.SetCooldown(0);
				//animator.SetTrigger("ActivateItem");
                animator3D.SetTrigger("Swing");
			}
		}
	}
	
	public Grave GetGrave()
	{
		return targetedGraves[0];
	}

	public void SetOnGrave(bool isOn, Grave curGrave)
	{
		if(isOn)
		{
			targetedGraves.Add(curGrave);
		}
		else
		{
			targetedGraves.Remove(curGrave);
		}
	}
	
	public bool IsOnGrave()
	{
		return (targetedGraves.Count > 0);
	}
	
	private void GrabZombie()
	{
		if (animator3D.GetCurrentAnimatorStateInfo(0).IsName("Swing Weapon"))
		{
			return;
		}
	
		ZombieScript tempZombie;
		Vector3 spherePos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
		Collider[] around = Physics.OverlapSphere(spherePos,1f);
		
		foreach (Collider ob in around)
		{
			if (ob.tag == "Zombie")
			{
				tempZombie = ob.GetComponent<ZombieScript>();
				if (tempZombie.Grab())
				{
					grabbedZombie = tempZombie;
					grabbedZombie.AlterRotation(Quaternion.Euler(0,0,90));
					holdingZombie = true;
                    tempZombie.transform.parent = zombieAttachPoint.transform;
					tempZombie.transform.localRotation =  Quaternion.AngleAxis(90.0f, Vector3.right);
					tempZombie.transform.localPosition = Vector3.zero;
					zombieAttachPoint.GetComponent<MeshRenderer>().enabled = false;
					animator3D.SetTrigger ("Carry");
					return;
				}
			}
		}
	}
	
	private void DropZombie()
	{
		if (IsOnGrave())
		{
			//bool droppedZed = false;

			foreach(Grave g in targetedGraves)
			{
				if(g.HasRoom())
				{
					grabbedZombie.Bury();
					g.ReturnZombie();
					ObjectiveEvents.pickupZombieObjective();
					//droppedZed = true;
					holdingZombie = false;
					grabbedZombie = null;
					zombieAttachPoint.GetComponent<MeshRenderer>().enabled = true;
					return;
				}
				else
				{
					Debug.Log("Grave is full!");
				}
			}
		}
		
		zombieAttachPoint.GetComponent<MeshRenderer>().enabled = true;

		//grabbedZombie.AlterRotation(Quaternion.Euler(0,0,-90));
		grabbedZombie.SetPosition(transform.position);
		grabbedZombie.SetStatus(ZombieStatus.NORMAL);
    	grabbedZombie.transform.parent = null;
		grabbedZombie.transform.rotation = Quaternion.identity;
		grabbedZombie.ResetMovement();

		zombieAttachPoint.GetComponent<MeshRenderer>().enabled = true;
		
		holdingZombie = false;
		grabbedZombie = null;
	}
	
	/*void OnTriggerEnter(Collider col)
	{
		if (col.gameObject.tag == "Grave")
		{
			onGrave = true;
			grave = col.gameObject.GetComponent<Grave>();
		}
	}
	
	void OnTriggerExit(Collider col)
	{
		if (col.gameObject.tag == "Grave")
		{
			onGrave = false;
			grave = null;
		}
	}*/
}

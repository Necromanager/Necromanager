using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
//using UnityEngine.EventSystems.EventSystem;

public enum InputModeCode
{
	ERROR = -1,
	CONTROLLER,
	KEYBOARD_AND_MOUSE
}

public class InputMethod : MonoBehaviour
{
	private static InputModeCode modeCode = InputModeCode.ERROR;
	private static InputMode mode;

	EventSystem eventSystem;
	StandaloneInputModule inputModule;

	private static InputMethod activeInstance;
	public static InputMethod Instance
	{
		get
		{
			if(activeInstance == null)
			{
				activeInstance = GameObject.FindObjectOfType<InputMethod>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(activeInstance.gameObject);
			}
			
			return activeInstance;
		}
	}
	
	// Use this for initialization
	void Start ()
	{
	}
	
	void Awake()
	{
		if(activeInstance == null)
		{
			//Debug.Log ("If I am the first instance, make me the Singleton");
			activeInstance = this;
			DontDestroyOnLoad(this);
			setInitialInput();
		}
		else
		{
			//If a Singleton already exists and you find
			//Debug.Log("another reference in scene, destroy it!");
			if(this != activeInstance)
				Destroy(this.gameObject);
		}


	}

	void OnLevelWasLoaded()
	{
	}

	public void Update()
	{
		if(Input.GetButtonDown("Toggle Mode"))
		{
			Debug.Log("Toggle Hit");
			switch(modeCode)
			{
			case InputModeCode.CONTROLLER:
				mode = new InputModeMAK();
				break;
			
			case InputModeCode.KEYBOARD_AND_MOUSE:
				mode = new InputModeController();
				break;
			}
		}
	}

	public static bool controllerConnected()
	{
		return (Input.GetJoystickNames().Length != 0);
	}

	public static void setInitialInput()
	{
		//Debug.Log ("Initializing Input");
		if(controllerConnected())
		{
			//Debug.Log ("Controller Mode Loaded");
			changeInput(InputModeCode.CONTROLLER);
		}
		else
		{
			//Debug.Log ("KB+M Mode Loaded");
			changeInput(InputModeCode.KEYBOARD_AND_MOUSE);
		}
	}

	public static void changeInput(InputModeCode m)
	{
		switch(m)
		{
		case InputModeCode.CONTROLLER:
			mode = new InputModeController();
			//InputMethod.setInputCode(InputModeCode.CONTROLLER);
			break;

		case InputModeCode.KEYBOARD_AND_MOUSE:
			mode = new InputModeMAK();
			//InputMethod.setInputCode(InputModeCode.KEYBOARD_AND_MOUSE);
			break;
		}
	}

	public static void setInputCode(InputModeCode m){modeCode = m;}
	public static InputModeCode getInputCode(){return modeCode;}

	static private string prefix(string str)
	{
		string newStr = str;
		if(modeCode == InputModeCode.CONTROLLER)
		{
			newStr = "J_" + str;
		}

		return newStr;
	}

	public static bool getButtonDown(string button)
	{
		return Input.GetButtonDown(prefix(button));
	}

	public static bool getButton(string button)
	{
		return Input.GetButton(prefix(button));
	}

	public static float getAxis(string axis)
	{
		return Input.GetAxis(prefix(axis));
	}

	public static float getAxisRaw(string axis)
	{
		return GlobalFunctions.NormalizeFloat( Input.GetAxisRaw(prefix(axis)) );
	}

	public static bool isAxisMoving(string axis)
	{
		return ( Input.GetAxis(prefix(axis)) != 0);
	}
}

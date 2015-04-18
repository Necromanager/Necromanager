using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ButtonCodes
{
	TOGGLE_LEFT,
	TOGGLE_RIGHT,
	USE_ITEM,
	ENTER
}

public class InputImage : MonoBehaviour {

	[SerializeField] ButtonCodes button;
	Image pic;

	// Use this for initialization
	void Start ()
	{
		pic = GetComponent<Image> ();
		setImage (button);
	}
	
	void setImage(ButtonCodes b)
	{
		string imagePath = "Sprites/UI/Buttons/Placeholder/";
		if(InputMethod.getInputCode() == InputModeCode.CONTROLLER)
		{
			imagePath += "XBOX_";
		}

		switch(b)
		{
		case ButtonCodes.USE_ITEM:
			imagePath += "UseItem";
			break;
		case ButtonCodes.TOGGLE_LEFT:
			imagePath += "ToggleLeft";
			break;
		case ButtonCodes.TOGGLE_RIGHT:
			imagePath += "ToggleRight";
			break;
		case ButtonCodes.ENTER:
			imagePath += "Enter";
			break;
		}
		Debug.Log (imagePath);
		pic.sprite = Resources.Load<Sprite>(imagePath);
	}
}

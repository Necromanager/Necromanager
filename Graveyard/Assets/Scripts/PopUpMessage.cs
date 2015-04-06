using UnityEngine;
using System.Collections;

public class PopUpMessage : MonoBehaviour 
{
	/*[SerializeField] private bool useText;
	[SerializeField] private string textMessage;
	[SerializeField] private Vector2 textSize;
	[SerializeField] private Color textColor;
	[SerializeField] private Texture imgMessage;
	[SerializeField] private Vector2 imgSize;*/
	private const float FADE_SPEED = .5f;
	
	private bool useText;
	private bool activated;
	private bool fading;
	
	private float curDuration;
	private float curAlpha;
	
	private string textMessage;
	private int textFontSize;
	private Color textColor;
	
	private Texture imgMessage;
	private Vector2 imgSize;

	private AudioClip messageSound;
	private GUIStyle myStyle;

	void Start ()
	{
		/*useText = true;
		activated = false;
		fading = false;
		curDuration = 0;
		curAlpha = 1;
		textMessage = "";
		textFontSize = 0;
		textColor = Color.white;
		imgMessage = null;
		imgSize = new Vector2(0,0);
		messageSound = null;
		myStyle = new GUIStyle();*/
	}
	
	void Update () 
	{
		if (activated && fading)
		{
			curAlpha -= FADE_SPEED*Time.deltaTime;
			
			if (curAlpha <= 0)
			{
				DestroyImmediate(gameObject);
			}
		}
	}
	
	void OnGUI()
	{
		if (activated)
		{
			
			if (useText)
			{
				DrawText();
			}
			else
			{
				DrawImage();
			}
			
			if (curDuration > 0)
			{
				curDuration -= Time.deltaTime;
				if (curDuration <= 0)
				{
					fading = true;
				}
			}
		}
	}
	
	private void DrawText()
	{
		SetupFont(textFontSize,textColor);
		
		Vector2 textSize = myStyle.CalcSize(new GUIContent(textMessage));
		Texture backTex = Resources.Load("Textures/Blank") as Texture;
		Rect textRect = new Rect(Screen.width/2-textSize.x/2,textSize.y*1.2f,textSize.x,textSize.y*1.2f);
		
		GUI.color = new Color(0,0,0,0.75f*curAlpha);
		GUI.DrawTexture(textRect,backTex);
		
		GUI.color = GetColor();
		GUI.Label(textRect,textMessage);
	}
	
	private void DrawImage()
	{
		GUI.color = GetColor();
	
		Rect picRect = new Rect(Screen.width/2-imgSize.x/2,imgSize.y,imgSize.x,imgSize.y);
		
		GUI.DrawTexture(picRect,imgMessage);
	}
	
	private void Init()
	{
		activated = true;
		fading = false;
		curAlpha = 1;
		
		PlaySound();
	}
	
	public void Init(string message, int size, Color color, float duration, AudioClip sound)
	{
		useText = true;
		textMessage = message;
		textFontSize = size;
		textColor = color;
		messageSound = sound;
		curDuration = duration;
		
		Init();
	}
	
	public void Init(Texture image, Vector2 imageSize, float duration, AudioClip sound)
	{
		useText = false;
		imgMessage = image;
		imgSize = imageSize;
		messageSound = sound;
		curDuration = duration;
		
		Init();
	}
	
	private void PlaySound()
	{
		GameObject.FindGameObjectWithTag("Main").GetComponent<SoundManager>().PlayMessageSound(messageSound);
	}
	
	private void SetupFont(int fontSize, Color fontColor)
	{
		myStyle = new GUIStyle();
		myStyle.fontSize = fontSize;
		GUI.skin.label.fontSize = fontSize;
		GUI.color = GetColor();
	}
	
	private Color GetColor()
	{
		if (useText)
		{
			return new Color(textColor.r,textColor.g,textColor.b,curAlpha);
		}
		else
		{
			return new Color(1,1,1,curAlpha);
		}
	}
}

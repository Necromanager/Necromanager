using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ClockUI : UIElement 
{
	//private const float MINUTE_TIME = 0.25f;
	private Color START_COLOR;
	private Color END_COlOR;

	[SerializeField] Image bigHand;
	[SerializeField] Image littleHand;
	[SerializeField] List<bool> playChimeOnHour;
	List<bool> chimes;
	
	private Image clockFace;
	
	private bool timeMoving;
	//private bool drawClock;
	private float curMinuteTime;
	private Color startColor;
	private Color endColor;

	void Start () 
	{
		curMinuteTime = 0;
		clockFace = GetComponent<Image>();
		startColor = GameObject.FindGameObjectWithTag("Main").GetComponent<Game>().nightColor;
		endColor = GameObject.FindGameObjectWithTag("Main").GetComponent<Game>().dayColor;
		timeMoving = false;
		chimes = new List<bool>( playChimeOnHour );
		//drawClock = true;
	}
	
	void Update () 
	{
		if (timeMoving)
		{
			UpdateTime();
		}
	}
	
	public override void LoadElements()
	{
		if (clockFace == null)
		{
			clockFace = GetComponent<Image>();
		}
	}
	
	public override void SetDraw(bool shouldDraw)
	{
		clockFace.enabled = shouldDraw;
		bigHand.GetComponent<Image>().enabled = shouldDraw;
		littleHand.GetComponent<Image>().enabled = shouldDraw;
	}
	
	public void StartTime()
	{
		curMinuteTime = 0;
		GlobalValues.ResetTime();
		timeMoving = true;
		chimes = new List<bool>( playChimeOnHour );
	}
	
	public void StopTime()
	{
		timeMoving = false;
	}
	
	private void UpdateTime()
	{
		curMinuteTime += Time.deltaTime;
		
		if (curMinuteTime >= GlobalValues.MINUTE_TIME)
		{
			curMinuteTime = 0;
			GlobalValues.PassTime(0,1);
		}

		Debug.Log("Hours:" + GlobalValues.hour + " StartTime:" + GlobalValues.getStartHour());

		if(chimes[GlobalValues.hour])
		{
			chimes[GlobalValues.hour] = false;
			GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.clockChime);
		}

		//Big hand
		float rotateAngle;
		rotateAngle = (GlobalValues.minute/60f)*360f;
		bigHand.rectTransform.rotation = Quaternion.Euler(0,0,-rotateAngle);
		
		//Small hand
		rotateAngle = (GlobalValues.hour/12f)*360f;
		littleHand.rectTransform.rotation = Quaternion.Euler(0,0,-rotateAngle);
		
		RenderSettings.ambientLight = Color.Lerp(startColor,endColor,GlobalValues.GetTimePercentage());
	}
}

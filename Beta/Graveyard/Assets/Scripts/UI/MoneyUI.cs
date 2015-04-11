using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoneyUI : UIElement
{
	Image moneyBackground;
	Text moneyText;
	public FlyingMoneyText fmt;
	public SadMoneyText smt;

	protected static MoneyUI instance;
	protected Vector3 centerPoint;

	void Start () 
	{
		instance = this;
		moneyBackground = GetComponent<Image>();
		moneyText = GetComponentInChildren<Text>();
	}
	
	void FixedUpdate () 
	{
		RectTransform rt = GetComponent<RectTransform> ();
		Vector3 cp = new Vector3 (Mathf.Abs(rt.rect.center.x),
		                          Mathf.Abs(rt.rect.center.y),
		                          0);
		centerPoint = instance.GetComponent<RectTransform> ().position - cp;

		moneyText.text = GlobalValues.GetMoneyString();
	}
	
	public override void LoadElements()
	{
		if (moneyText == null)
		{
			moneyBackground = GetComponent<Image>();
			moneyText = GetComponentInChildren<Text>();
		}
	}

	public override void SetDraw(bool shouldDraw)
	{
		moneyText.enabled = shouldDraw;
		moneyBackground.enabled = shouldDraw;
	}

	public static void spawnMoneyText(Vector3 pos, float m)
	{
		Debug.Log ("Spawing fm");
		GameObject mon = Instantiate(instance.fmt.gameObject);
		mon.transform.SetParent(instance.transform.parent);

		Vector3 screenPos = Camera.main.WorldToScreenPoint(pos);
		mon.GetComponent<RectTransform> ().position = screenPos;
		mon.GetComponent<FlyingMoneyText> ().init (instance.centerPoint, m);

	}

	public static void spawnSadMoneyText(float m)
	{
		Debug.Log ("Spawing sm");
		GameObject mon = Instantiate(instance.smt.gameObject);
		mon.transform.SetParent(instance.transform.parent);

		mon.GetComponent<RectTransform> ().position = instance.centerPoint;
		mon.GetComponent<SadMoneyText> ().init (m);
		
	}
}

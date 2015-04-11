using UnityEngine;
using System.Collections;

public abstract class MenuManager : MonoBehaviour 
{

	[SerializeField] protected int fontSize;
	[SerializeField] protected Color fontColor;
	[SerializeField] protected float bufferSpace;

	protected Menu currentMenu;
	protected bool canDraw;

	void Start () 
	{
		canDraw = true;
		Init ();
		currentMenu = GetInitialMenu();
	}
	
	void Update () 
	{
		
	}
	
	void OnGUI()
	{
		UpdateManager();
		if (canDraw)
		{
			currentMenu.SetupFont(fontSize,fontColor);
			currentMenu.Draw(bufferSpace);
		}
	}
	
	public void SwapMenu(Menu newMenu)
	{
		currentMenu = newMenu;
	}
	
	protected abstract void Init();
	protected abstract Menu GetInitialMenu();
	protected abstract void UpdateManager();
}

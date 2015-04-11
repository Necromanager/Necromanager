using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public abstract class UIElement : MonoBehaviour 
{
	public abstract void SetDraw(bool shouldDraw);
	public abstract void LoadElements();
}

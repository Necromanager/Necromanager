using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NewMenu : MonoBehaviour {

	private Animator anim;
	private CanvasGroup group;
	[SerializeField]
	private Selectable target;

	[SerializeField] bool moveAtStart = true;

	[SerializeField] protected NewMenu next;

	public bool isOpen
	{
		get { return anim.GetBool ("isOpen");}
		set { anim.SetBool ("isOpen", value);}
	}

	public virtual void Awake()
	{
		anim = GetComponent<Animator> ();
		group = GetComponent<CanvasGroup> ();

		if(moveAtStart)
		{
			var rect = GetComponent<RectTransform> ();
			rect.offsetMax = rect.offsetMin = Vector2.zero;
		}
	}

	public virtual void Update()
	{
		if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
		{
			group.blocksRaycasts = group.interactable = false;
		}
		else
		{
			group.blocksRaycasts = group.interactable = true;
		}
	}

	public void setSelected(EventSystem es)
	{
		if(target == null)
		{
			es.SetSelectedGameObject (null);
		}
		else
		{
			es.SetSelectedGameObject (target.gameObject);
		}
	}

	public virtual void onShow()
	{

	}
	 public virtual void onHide()
	{

	}

	public void gotoNext()
	{
		MenuEvents.nextMenu(next);
	}

	public void setNext(NewMenu menu)
	{
		next = menu;
		isOpen = false;
	}

	public void showBG()
	{
		MenuEvents.showMainBG ();
	}
}

using UnityEngine;
using System.Collections;

public abstract class Item
{
	protected const float SELECTED_ALPHA = 1f;
	protected const float UNSELECTED_ALPHA = 0.25f;
	protected const string SOLD_OUT_TEXTURE = "Textures/Items/StoreSoldOut";
	protected Color ACTIVE_COLOR = Color.blue;
	protected Color INACTIVE_COLOR = Color.red;
	protected const float USE_AREA_ALPHA = 0.5f;
	
	//[SerializeField] protected int price;
	//[SerializeField] protected float cooldownTime;
	
	protected Sprite itemPic;
	protected Texture storePic;
	
	protected string itemName;
	protected string itemDesc;
	protected string soundPath;
	
	protected int price;
	
	protected float cooldownTime;
	//protected float maxCooldown;
	protected float curCooldown;
	
	protected bool soldOut = false;
	protected bool selected = false;
	
	void Start () 
	{
	}
	
	void Update () 
	{
	}
	
	public void Reset()
	{
		curCooldown = cooldownTime;
		SpecialReset();
	}
	
	public void UpdateItem(bool isSelected)
	{
		selected = isSelected;
		UpdateCooldown();
		UpdateSpecial();
	}
	
	public void UpdateCooldown()
	{
		if (curCooldown < cooldownTime)
		{
			curCooldown += Time.deltaTime;
			if (curCooldown > cooldownTime)
			{
				curCooldown = cooldownTime;
			}
		}
	}
	
	public Color GetHudColor(bool selected)
	{
		float alpha = UNSELECTED_ALPHA;
		if (selected)
		{
			alpha = SELECTED_ALPHA;
		}
		
		Color coolColor = Color.white;
		
		if (!CanUse())
		{
			coolColor = Color.Lerp(Color.red,Color.gray,GetCooldownPercent());
		}
		
		return (new Color(coolColor.r,coolColor.g,coolColor.b,alpha));
	}
	
	protected virtual void PlaySoundEffect()
	{
		GlobalFunctions.PlaySoundEffect(SoundEffectLibrary.usePlaceholder);
	}
	
	/*protected void PlaySoundEffect(AudioClip sound)
	{
		GlobalFunctions.PlaySoundEffect(sound);
	}*/
	
	public string GetName()
	{
		return itemName;
	}
	
	public int GetCost()
	{
		return price;
	}
	
	public string GetDesc()
	{
		return itemDesc;
	}
	
	public string GetDescText()
	{
		return (itemName+"\n"+itemDesc);
	}
	
	public string GetStoreText()
	{
		return (itemName+" - $"+price);
	}
	
	public float GetCooldownPercent()
	{
		return (curCooldown/cooldownTime);
	}
	
	public string GetCooldownString()
	{
		float percent = GetCooldownPercent()*100;
		return percent.ToString("F0")+"%";
	}
	
	public Sprite GetSprite()
	{
		return itemPic;
	}
	
	public Texture GetStoreTexture()
	{
		if (soldOut)
		{
			return GetSoldOutTexture();
		}
		
		return storePic;
	}
	
	public Texture GetSoldOutTexture()
	{
		return Resources.Load(SOLD_OUT_TEXTURE) as Texture;
	}
	
	public void SetCooldown(float percent)
	{
		curCooldown = cooldownTime*percent;
		Debug.Log ("Cooldown set to: " + curCooldown);
	}
	
	public bool CanUse()
	{
		return (curCooldown >= cooldownTime);
	}
	
	public bool IsSoldOut()
	{
		return soldOut;
	}

	protected void LoadData()
	{
		GameObject main = GameObject.FindGameObjectWithTag ("Main");
		ItemValues itemVals = main.GetComponent<ItemValues> ();
		ItemData itemData = itemVals.GetData (itemName);

		price = itemData.cost;
		cooldownTime = itemData.cooldown;
	}
	
	public abstract void Init();
	public abstract bool IsUsable(PlayerScript player);
	public abstract void Activate(PlayerScript player);
	public abstract void Buy(); 
	public abstract void SpecialReset();
	protected abstract void UpdateSpecial();
}

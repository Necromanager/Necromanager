using UnityEngine;
using System.Collections;

public class StoreMenu : NewMenu {

	[SerializeField]
	ConfirmScreen confirmScreen;

	public override void onShow()
	{
		base.onShow ();
		//GlobalFunctions.PlaySoundEffect (SoundEffectLibrary.enterStore);
	}

	public override void onHide()
	{
		base.onHide ();
		//GlobalFunctions.PlaySoundEffect (SoundEffectLibrary.leaveStore);
	}
}

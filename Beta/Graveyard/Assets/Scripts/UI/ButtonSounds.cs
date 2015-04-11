using UnityEngine;
using System.Collections;

public class ButtonSounds : MonoBehaviour {

	public void playSelect()
	{
		GlobalFunctions.PlaySoundEffect (SoundEffectLibrary.moveCurser);
	}

	public void playConfirm()
	{
		GlobalFunctions.PlaySoundEffect (SoundEffectLibrary.menuSelect);
	}
}

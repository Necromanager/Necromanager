using UnityEngine;
using System.Collections;

public class OptionsMenu : Menu
{
	public OptionsMenu()
	{
		Init ();
	}

	protected override void DrawExtras()
	{
	}
	
	protected override void AddOptions()
	{
		options.Add(new MusicOption());
		options.Add(new SoundEffectsOption());
		options.Add(new BackOption());
	}
}

using UnityEngine;
using System.Collections;

public class SizeLRToggle : LRToggle {

	protected override void handleLeft()
	{
		GlobalValues.IncrementSize(-1);
	}
	
	protected override void handleRight()
	{
		GlobalValues.IncrementSize(1);
	}
}

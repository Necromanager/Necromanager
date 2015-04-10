using UnityEngine;
using System.Collections;

public static class Results
{
	//public static float totalNights = 0;

	public static float moneyEarned = 0;//
	public static float moneySpent = 0;
	public static float moneyLost = 0;//

	public static float zombiesSpawned = 0;//
	public static float zombiesReturned = 0;//
	public static float zombiesEscaped = 0;//

	public static void reset()
	{
		//totalNights = 0;
		
		moneyEarned = 0;
		moneySpent = 0;
		moneyLost = 0;
		
		zombiesSpawned = 0;
		zombiesReturned = 0;
		zombiesEscaped = 0;
	}
}

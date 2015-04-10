using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultsScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		GetComponent<Text> ().text =
			"Total nights: " + GlobalValues.day +
				"\nMoney earned: $" + Results.moneyEarned +
				"\nMoney spent: $" + Results.moneySpent +
				"\nFines: $" + Results.moneyLost +
				"\nZombies returned: " + Results.zombiesReturned +
				"\nZombies escaped: " + Results.zombiesEscaped;
		
		;
	}

	public void updateDisplay()
	{

	}
}

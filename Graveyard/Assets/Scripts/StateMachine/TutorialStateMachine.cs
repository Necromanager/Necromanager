using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TutorialStateMachine : GameStateMachine {

	public TutorialStateMachine(Game parentGame) : base(parentGame)
	{
		game = parentGame;
		SetupStates();
		currentState = states[0];
	}

	protected override void SetupStates()
	{
		states = new List<GameState>();
		
		MainGameState gameState = new MainGameState();

		
		gameState.SetLinkedStates(1,2);
		
		states.Add(gameState);

	}

}

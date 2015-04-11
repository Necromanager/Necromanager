using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameStateMachine
{
	protected List<GameState> states;
	protected GameState currentState;
	protected Game game;

	public GameStateMachine(Game parentGame)
	{
		game = parentGame;
		SetupStates();
		currentState = states[0];
	}
	
	protected virtual void SetupStates()
	{
		states = new List<GameState>();
		
		MainGameState gameState = new MainGameState();
		StoreState storeState = new StoreState();
		BuildState buildState = new BuildState();
		
		gameState.SetLinkedStates(1,2);
		storeState.SetLinkedStates(2,0);
		buildState.SetLinkedStates(0,1);
		
		states.Add(gameState);
		states.Add(storeState);
		states.Add(buildState);
	}
	
	public void LoadState(GameMode mode)
	{
		currentState = states[0];
		foreach (GameState state in states)
		{
			if (state.GetGameMode() == mode)
			{
				currentState = state;
				break;
			}
		}
		
		currentState.Init();
		game.SwitchGameMode(currentState.GetGameMode());
	}
	
	public void UpdateState()
	{
		currentState.UpdateState();
		
		if (currentState.ShouldSwitchState())
		{
			LoadNextState();
			currentState.Init();
			game.SwitchGameMode(currentState.GetGameMode());
		}
	}
	
	public GameMode GetGameMode()
	{
		return currentState.GetGameMode();
	}
	
	public void Draw()
	{
		currentState.Draw();
	}
	
	public string GetControls()
	{
		return currentState.GetControls();
	}
	
	private void LoadNextState()
	{
		currentState = states[currentState.GetNextStateIndex()];
	}
	
	private void LoadPrevState()
	{
		currentState = states[currentState.GetPrevStateIndex()];
	}
}

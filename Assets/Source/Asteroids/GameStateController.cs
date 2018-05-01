using System;

public class GameStateController
{
    public GameStateModel GameState;

    public GameStateController()
    {
        GameState = new GameStateModel();
    }

    public void Initialize(StageModel stageModel)
    {
        GameState.CurrentLives.Value = stageModel.StartingLivesAmount;
        GameState.CurrentScore.Value = 0;
    }

    public void UpdateLives(int lives)
    {
        GameState.CurrentLives.Value = lives;
    }
}
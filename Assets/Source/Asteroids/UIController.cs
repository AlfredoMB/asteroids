using UnityEngine;

public class UIController : MonoBehaviour
{
    public TemplateListView LivesView;
    public TextView ScoreView;

    public void Initialize(GameStateController gameState)
    {
        gameState.GameState.CurrentLives.OnUpdated += OnLivesUpdated;
        gameState.GameState.CurrentScore.OnUpdated += OnScoreUpdated;
    }

    private void OnLivesUpdated(int obj)
    {
        LivesView.UpdateValue(obj);
    }

    private void OnScoreUpdated(int obj)
    {
        ScoreView.UpdateValue(obj);
    }
}
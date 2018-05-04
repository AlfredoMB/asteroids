using UnityEngine;

public class StageUI : MonoBehaviour
{
    public StageStateModel StageStateModel;

    public TemplateListView LivesView;
    public TextView ScoreView;

    public void OnEnable()
    {
        OnLivesUpdated(StageStateModel.Lives.Value);
        OnScoreUpdated(StageStateModel.Score.Value);

        StageStateModel.Lives.OnUpdated += OnLivesUpdated;
        StageStateModel.Score.OnUpdated += OnScoreUpdated;
    }

    private void OnDisable()
    {
        StageStateModel.Lives.OnUpdated -= OnLivesUpdated;
        StageStateModel.Score.OnUpdated -= OnScoreUpdated;
    }

    private void OnLivesUpdated(int value)
    {
        if (LivesView == null)
        {
            return;
        }

        LivesView.UpdateValue(value);
    }

    private void OnScoreUpdated(int value)
    {
        if (ScoreView == null)
        {
            return;
        }

        ScoreView.UpdateValue(value);
    }
}
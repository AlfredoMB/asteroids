using UnityEngine;

public class StageUI : MonoBehaviour
{
    public StageStateModel StageStateModel;

    public TemplateListView LivesView;
    public TextView ScoreView;

    public void Start()
    {
        OnLivesUpdated(StageStateModel.Lives.Value);
        OnScoreUpdated(StageStateModel.Score.Value);

        StageStateModel.Lives.OnUpdated += OnLivesUpdated;
        StageStateModel.Score.OnUpdated += OnScoreUpdated;
    }

    private void OnLivesUpdated(int value)
    {
        LivesView.UpdateValue(value);
    }

    private void OnScoreUpdated(int value)
    {
        ScoreView.UpdateValue(value);
    }
}
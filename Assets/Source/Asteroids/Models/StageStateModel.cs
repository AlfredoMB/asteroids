public class StageStateModel
{
    public Observable<int> Lives = new Observable<int>();
    public Observable<int> Score = new Observable<int>();
    public Observable<int> Level = new Observable<int>();

    public void Initialize(StageModel stageModel)
    {
        Lives.Value = stageModel.StartingLivesAmount;
        Score.Value = 0;
        Level.Value = 1;
    }
}

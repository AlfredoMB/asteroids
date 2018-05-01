using UnityEngine;

public class GameStateModel
{
    public Observable<int> CurrentLives = new Observable<int>();
    public Observable<int> CurrentScore = new Observable<int>();
    public Observable<int> Level = new Observable<int>();
}

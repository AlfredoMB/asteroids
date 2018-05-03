using UnityEngine;

public class TitleScreenState : MonoBehaviour
{
    public BaseTitleScreenInput Input;
    public GameObject StartStageState;
    public BaseFSM FSM;
    public BaseAsteroidsGameController AsteroidsGameController;

    public int StartingAsteroidsAmount = 5;
    public float AsteroidStartingForceIntensity = 100f;

    private void OnEnable()
    {
        AsteroidsGameController.CreateAsteroidsAroundTheScreen(StartingAsteroidsAmount, AsteroidStartingForceIntensity);
        Input.OnStartStage += OnStartStage;
    }

    private void OnDisable()
    {
        Input.OnStartStage -= OnStartStage;
    }

    private void OnStartStage()
    {
        FSM.ChangeState(StartStageState);
    }
}
using UnityEngine;

public class TitleScreenState : MonoBehaviour
{
    public BaseTitleScreenInput Input;
    public BaseAsteroidsGameController AsteroidsGameController;

    public GameObject StartStageState;
    public BaseFSM FSM;
    
    public int StartingAsteroidsAmount = 5;
    public float AsteroidStartingForceIntensity = 100f;

    private void OnEnable()
    {
        AsteroidsGameController.Reset();
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
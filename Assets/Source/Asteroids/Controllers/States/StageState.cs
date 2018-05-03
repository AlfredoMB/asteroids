using System.Collections;
using UnityEngine;

public class StageState : MonoBehaviour
{
    public float RespawnDelay = 1f;

    public BaseShipInput ShipInput;
    public BaseAsteroidsGameController AsteroidsGameController;

    public GameObject GameOverState;
    public BaseFSM FSM;

    public void OnEnable()
    {
        AsteroidsGameController.Initialize();
        AsteroidsGameController.StartLevel();

        AsteroidsGameController.PlayerShip.SetInput(ShipInput);

        AsteroidsGameController.OnGameOver += OnGameOver;
        AsteroidsGameController.OnLevelFinished += OnLevelFinished;
        AsteroidsGameController.OnShipDestroyed += OnShipDestroyed;     
    }

    public void OnDisable()
    {
        AsteroidsGameController.OnGameOver -= OnGameOver;
        AsteroidsGameController.OnLevelFinished -= OnLevelFinished;
        AsteroidsGameController.OnShipDestroyed -= OnShipDestroyed;
    }

    private void OnShipDestroyed()
    {
        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(RespawnDelay);
        AsteroidsGameController.RespawnShip();
    }

    private void OnLevelFinished()
    {
        AsteroidsGameController.StartLevel();
    }

    private void OnGameOver()
    {
        FSM.ChangeState(GameOverState);
    }

}
using UnityEngine;

public class StageState : MonoBehaviour
{
    public ShipModel ShipModel;
    public StageModel StageModel;
    public StageStateModel StageStateModel;

    public BaseShipInput ShipInput;
    public BaseAsteroidsGameController AsteroidsGameController;

    public GameObject GameOverState;
    public BaseFSM FSM;

    private ShipController _playerShip;

    public void OnEnable()
    {
        StageStateModel.Initialize(StageModel);

        AsteroidsGameController.Reset();
        AsteroidsGameController.CreateAsteroidsAroundTheScreen(StageModel.StartingAsteroidsAmount, StageModel.AsteroidStartingForceIntensity);

        CreateShip();
        AttachShip();
    }

    private void CreateShip()
    {
        _playerShip = AsteroidsGameController.CreateShip(ShipModel);
    }

    private void AttachShip()
    { 
        _playerShip.OnShipDestruction += OnShipDestruction;

        ShipInput.OnStartMainThrusters += _playerShip.StartMainThrusters;
        ShipInput.OnStopMainThrusters += _playerShip.StopMainThrusters;

        ShipInput.OnStartLeftThrusters += _playerShip.StartLeftThrusters;
        ShipInput.OnStopLeftThrusters += _playerShip.StopLeftThrusters;

        ShipInput.OnStartRightThrusters += _playerShip.StartRightThrusters;
        ShipInput.OnStopRightThrusters += _playerShip.StopRightThrusters;

        ShipInput.OnFire += _playerShip.Fire;
    }

    public void OnDisable()
    {
        DeattachShip();
    }

    private void DeattachShip()
    { 
        _playerShip.OnShipDestruction -= OnShipDestruction;

        ShipInput.OnStartMainThrusters -= _playerShip.StartMainThrusters;
        ShipInput.OnStopMainThrusters -= _playerShip.StopMainThrusters;

        ShipInput.OnStartLeftThrusters -= _playerShip.StartLeftThrusters;
        ShipInput.OnStopLeftThrusters -= _playerShip.StopLeftThrusters;

        ShipInput.OnStartRightThrusters -= _playerShip.StartRightThrusters;
        ShipInput.OnStopRightThrusters -= _playerShip.StopRightThrusters;

        ShipInput.OnFire -= _playerShip.Fire;
    }

    private void OnShipDestruction()
    {
        DeattachShip();

        StageStateModel.Lives.Value -= 1;

        if (StageStateModel.Lives.Value > 0)
        {
            CreateShip();
            AttachShip();
        }
        else
        {
            FSM.ChangeState(GameOverState);
        }
    }

    private void OnAsteroidDestruction()
    {
        StageStateModel.Lives.Value += 100;
    }
}
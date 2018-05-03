using UnityEngine;

public class StageState : MonoBehaviour
{
    public ShipModel ShipModel;
    public StageModel StageModel;
    public StageStateModel StageStateModel = new StageStateModel();

    public BaseShipInput ShipInput;
    public BaseAsteroidsGameController AsteroidsGameController;

    private ShipController _playerShip;

    public void OnEnable()
    {
        StageStateModel.Initialize(StageModel);

        AsteroidsGameController.Reset();
        AsteroidsGameController.CreateAsteroidsAroundTheScreen(StageModel.StartingAsteroidsAmount, StageModel.AsteroidStartingForceIntensity);

        _playerShip = AsteroidsGameController.CreateShip(ShipModel);
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
        StageStateModel.Lives.Value -= 1;
    }

    private void OnAsteroidDestruction()
    {
        StageStateModel.Lives.Value += 100;
    }
}
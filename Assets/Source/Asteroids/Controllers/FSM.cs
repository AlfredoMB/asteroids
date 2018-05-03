using UnityEngine;

public class FSM : BaseFSM
{
    public GameObject CurrentState;

    private void Start()
    {
        CurrentState.SetActive(true);
    }

    public override void ChangeState(GameObject state)
    {
        if (CurrentState != null)
        {
            CurrentState.SetActive(false);
        }

        CurrentState = state;
        CurrentState.SetActive(true);
    }
}
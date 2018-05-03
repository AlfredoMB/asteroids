using UnityEngine;

public abstract class BaseFSM : MonoBehaviour
{
    public abstract void ChangeState(GameObject state);
}
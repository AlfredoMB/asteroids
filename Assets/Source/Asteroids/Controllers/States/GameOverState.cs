using System.Collections;
using UnityEngine;

public class GameOverState : MonoBehaviour
{
    public GameObject TitleScreenState;
    public BaseFSM FSM;

    public int GameOverDuration = 5;

    private void OnEnable()
    {
        StartCoroutine(WaitToRestart());
    }

    private IEnumerator WaitToRestart()
    {
        yield return new WaitForSeconds(GameOverDuration);
        FSM.ChangeState(TitleScreenState);
    }
}

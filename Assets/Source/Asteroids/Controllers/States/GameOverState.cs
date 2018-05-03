using UnityEngine;
using System.Collections;

public class GameOverState : MonoBehaviour
{
    public GameObject TitleScreenState;
    public BaseFSM FSM;

    public int GameOverDuration = 5;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(GameOverDuration);
        FSM.ChangeState(TitleScreenState);
    }
}

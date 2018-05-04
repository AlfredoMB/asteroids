#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;
using NUnit.Framework;

public class GameOverStateTests
{
    private class MockFSM : BaseFSM
    {
        public bool hasChangedState;
        public GameObject CurrentState;

        public override void ChangeState(GameObject state)
        {
            CurrentState = state;
            hasChangedState = true;
        }
    }

    [UnityTest]
    public IEnumerator WaitToRestartTest()
    {
        var titleStateGameObject = new GameObject();

        var gameObject = new GameObject();
        var gameOverState = gameObject.AddComponent<GameOverState>();
        var fsm = gameObject.AddComponent<MockFSM>();
        gameOverState.TitleScreenState = titleStateGameObject;
        gameOverState.FSM = fsm;

        yield return new WaitUntil(() => fsm.hasChangedState);

        Assert.That(fsm.CurrentState, Is.EqualTo(titleStateGameObject));
    }
}
#endif
using UnityEngine;

public class TitleScreenInput : BaseTitleScreenInput
{
    private void Update()   
    {
        if (Input.anyKey && OnStartStage != null)
        {
            OnStartStage();
        }
    }

    private void OnDisable()
    {
        OnStartStage = null;
    }
}

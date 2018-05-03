using UnityEngine;
using UnityEngine.UI;

public class TextView : MonoBehaviour
{
    public Text Text;

    public void UpdateValue(int value)
    {
        Text.text = value.ToString();
    }

    public void UpdateValue(string value)
    {
        Text.text = value;
    }
}
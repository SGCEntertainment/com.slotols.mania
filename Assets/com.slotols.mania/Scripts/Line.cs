using UnityEngine.UI;
using UnityEngine;

public class Line : MonoBehaviour
{
    private void Awake()
    {
        Text textComponent = GetComponent<Text>();
        Manager.OnSetLine += (value) =>
        {
            textComponent.text = $"{value}";
        };

        textComponent.text = $"{1}";
    }
}

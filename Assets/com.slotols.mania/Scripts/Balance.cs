using UnityEngine;
using UnityEngine.UI;

public class Balance : MonoBehaviour
{
    private const string key = "balance";

    private int Count
    {
        get => PlayerPrefs.GetInt(key, 1000);
        set => PlayerPrefs.SetInt(key, value);
    }

    private void Awake()
    {
        Text textComponent = GetComponent<Text>();

        Manager.OnEndRolling += (value) =>
        {
            Count += value;
            textComponent.text = $"{Count:N}";
        };

        textComponent.text = $"{Count:N}";
    }

    private void OnDestroy()
    {
        Manager.OnEndRolling = null;
    }
}

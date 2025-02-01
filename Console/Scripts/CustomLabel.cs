using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomLabel : MonoBehaviour
{
    public TMP_InputField inputField;
    public TMP_Text textToChange;
    public string playerPrefsKey = "KeyLabel";

    private void Start()
    {
        string savedText = PlayerPrefs.GetString(playerPrefsKey, "");
        inputField.text = savedText;
        textToChange.text = savedText;

        inputField.onValueChanged.AddListener(OnInputFieldTextChanged);
    }

    private void OnInputFieldTextChanged(string newText)
    {
        textToChange.text = newText;

        PlayerPrefs.SetString(playerPrefsKey, newText);
    }
}
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitcher : MonoBehaviour
{
    public Toggle toggle;
    public GameObject[] objects;
    public string playerPrefsKey = "KeyState";

    private void Start()
    {
        int savedState = PlayerPrefs.GetInt(playerPrefsKey, 0);
        toggle.isOn = savedState == 1;

        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(toggle.isOn);
            }
        }

        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool newValue)
    {
        PlayerPrefs.SetInt(playerPrefsKey, newValue ? 1 : 0);

        foreach (GameObject obj in objects)
        {
            if (obj != null)
            {
                obj.SetActive(newValue);
            }
        }
    }
}
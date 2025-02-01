using UnityEngine;

public class Switcher : MonoBehaviour
{
    public GameObject window;
    private bool active;

    void Start()
    {
        active = window.activeSelf;
        window.SetActive(active);
    }

    public void Switch()
    {
        active = !active;
        window.SetActive(active);
    }

    public void SetSwitch(bool setActive)
    {
        active = setActive;
        window.SetActive(setActive);
    }
}
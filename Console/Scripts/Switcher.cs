using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Application;

public class Switcher : MonoBehaviour
{
    public GameObject window;

    void Start()
    {
        window.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.backquoteKey.wasPressedThisFrame)
        {
            window.SetActive(!window.activeSelf);
        }
    }

    public void Switch()
    {
        window.SetActive(!window.activeSelf);
    }
}
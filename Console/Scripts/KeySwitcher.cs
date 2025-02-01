using UnityEngine;
using UnityEngine.InputSystem;

public class KeySwitcher : MonoBehaviour
{
    public GameObject window;

    void Update()
    {
        if (Keyboard.current.backquoteKey.wasPressedThisFrame)
        {
            window.SetActive(!window.activeSelf);
        }
    }
}
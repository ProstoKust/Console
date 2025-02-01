using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void OpenLink(string targetURL)
    {
        Application.OpenURL(targetURL);
    }
}
using UnityEngine;
using TMPro;

public class About : MonoBehaviour
{
    public TMP_Text ProductText;
    public TMP_Text UnityText;
    public TMP_Text DeveloperText;

    void Start()
    {
        ProductText.text = $"{Application.productName} v{Application.version}";
        UnityText.text = $"Unity {Application.unityVersion}";
        DeveloperText.text = $"from {Application.companyName}";
    }
}
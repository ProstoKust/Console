using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CustomImage : MonoBehaviour
{
    public Image mainImage;
    public GameObject buttonPrefab;
    public Transform buttonContainer;

    public List<Sprite> imageList;
    public Sprite noneSprite;

    private int currentImageIndex = 0;
    private bool mainImageActive = true;

    private void Start()
    {
        currentImageIndex = PlayerPrefs.GetInt("CurrentImageIndex", 0);

        if (currentImageIndex >= imageList.Count)
        {
            currentImageIndex = 0;
        }

        SetImage(currentImageIndex);

        for (int i = 0; i < imageList.Count; i++)
        {
            int index = i + 1;
            GameObject button = Instantiate(buttonPrefab, buttonContainer);
            Button btnComponent = button.GetComponent<Button>();

            if (btnComponent != null)
            {
                btnComponent.onClick.AddListener(() => { SwitchImage(index); });

                Image buttonImage = button.GetComponent<Image>();
                if (buttonImage != null)
                {
                    buttonImage.sprite = imageList[i];
                }
            }
        }

        GameObject crossButton = Instantiate(buttonPrefab, buttonContainer);
        Button crossBtnComponent = crossButton.GetComponent<Button>();
        if (crossBtnComponent != null)
        {
            crossBtnComponent.onClick.AddListener(() => { SwitchImage(0); });
            Image crossButtonImage = crossButton.GetComponent<Image>();
            if (crossButtonImage != null)
            {
                crossButtonImage.sprite = noneSprite;
            }
        }
    }

    private void SetImage(int index)
    {
        if (index == 0)
        {
            mainImage.gameObject.SetActive(false);
            mainImageActive = false;
        }
        else if (index >= 1 && index <= imageList.Count)
        {
            mainImage.sprite = imageList[index - 1];
            mainImage.gameObject.SetActive(true);
            mainImageActive = true;
            currentImageIndex = index;
        }
    }

    private void SwitchImage(int index)
    {
        SetImage(index);

        PlayerPrefs.SetInt("CurrentImageIndex", currentImageIndex);
        PlayerPrefs.Save();
    }
}
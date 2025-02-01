using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject[] tabs; // Массив вкладок
    private int activeTabIndex = -1; // Индекс активной вкладки

    public void ToggleTab(int tabIndex)
    {
        if (tabIndex == activeTabIndex)
        {
            // Если вкладка уже активна, закроем её
            CloseTab(tabIndex);
        }
        else
        {
            // Закрываем предыдущую вкладку, если она была открыта
            if (activeTabIndex != -1)
            {
                CloseTab(activeTabIndex);
            }
            // Открываем новую вкладку
            OpenTab(tabIndex);
        }
    }

    private void OpenTab(int tabIndex)
    {
        tabs[tabIndex].SetActive(true);
        activeTabIndex = tabIndex;
    }

    private void CloseTab(int tabIndex)
    {
        tabs[tabIndex].SetActive(false);
        activeTabIndex = -1;  // Сбрасываем активный индекс
    }
}
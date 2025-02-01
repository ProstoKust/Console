using UnityEngine;
using UnityEngine.UI;

public class TabManager : MonoBehaviour
{
    public GameObject[] tabs; // ������ �������
    private int activeTabIndex = -1; // ������ �������� �������

    public void ToggleTab(int tabIndex)
    {
        if (tabIndex == activeTabIndex)
        {
            // ���� ������� ��� �������, ������� �
            CloseTab(tabIndex);
        }
        else
        {
            // ��������� ���������� �������, ���� ��� ���� �������
            if (activeTabIndex != -1)
            {
                CloseTab(activeTabIndex);
            }
            // ��������� ����� �������
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
        activeTabIndex = -1;  // ���������� �������� ������
    }
}
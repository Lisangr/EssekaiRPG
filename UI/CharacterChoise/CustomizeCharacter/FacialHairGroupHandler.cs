using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacialHairGroupHandler : MonoBehaviour
{
    public string[] namesForSearch; // ������ ���� ��� ������
    public Button[] arrowLeftButtons; // ������ ������ ������� �����
    public Button[] arrowRightButtons; // ������ ������ ������� ������

    private List<List<GameObject>> facialHairGroups; // ������ ������� ��������
    private int[] currentIndexes; // ������ ������� �������� ��� ������� ������

    void Start()
    {
        facialHairGroups = new List<List<GameObject>>();
        currentIndexes = new int[namesForSearch.Length];

        for (int i = 0; i < namesForSearch.Length; i++)
        {
            // ������������� ��������
            currentIndexes[i] = -1;

            // ����� ������ � ��������� ������
            Transform facialHairGroup = FindChildByName(transform, namesForSearch[i]);

            if (facialHairGroup != null)
            {
                // �������� ��� �������� �������
                List<GameObject> children = GetAllChildren(facialHairGroup);
                facialHairGroups.Add(children);

                // ���������, ��� ���� ������� ��� ������������
                if (children.Count > 0)
                {
                    currentIndexes[i] = 0;
                    ActivateObject(i, currentIndexes[i]);
                }

                // ��������� ����������� ������� ��� ������
                int index = i; // ��������� ����� ��� ������������� � ������-���������
                arrowLeftButtons[i].onClick.AddListener(() => OnArrowLeftClick(index));
                arrowRightButtons[i].onClick.AddListener(() => OnArrowRightClick(index));
            }
            else
            {
                Debug.LogWarning($"Group '{namesForSearch[i]}' not found.");
                facialHairGroups.Add(new List<GameObject>());
            }
        }
    }

    private Transform FindChildByName(Transform parent, string name)
    {
        foreach (Transform child in parent)
        {
            if (child.name == name)
                return child;

            Transform result = FindChildByName(child, name);
            if (result != null)
                return result;
        }
        return null;
    }

    private List<GameObject> GetAllChildren(Transform parent)
    {
        List<GameObject> children = new List<GameObject>();

        foreach (Transform child in parent)
        {
            children.Add(child.gameObject);
        }

        return children;
    }

    private void OnArrowLeftClick(int groupIndex)
    {
        if (currentIndexes[groupIndex] > 0)
        {
            ActivateObject(groupIndex, currentIndexes[groupIndex] - 1);
        }
    }

    private void OnArrowRightClick(int groupIndex)
    {
        if (currentIndexes[groupIndex] < facialHairGroups[groupIndex].Count - 1)
        {
            ActivateObject(groupIndex, currentIndexes[groupIndex] + 1);
        }
    }

    private void ActivateObject(int groupIndex, int newIndex)
    {
        if (currentIndexes[groupIndex] >= 0 && currentIndexes[groupIndex] < facialHairGroups[groupIndex].Count)
        {
            facialHairGroups[groupIndex][currentIndexes[groupIndex]].SetActive(false);
        }

        facialHairGroups[groupIndex][newIndex].SetActive(true);
        currentIndexes[groupIndex] = newIndex;
    }
}

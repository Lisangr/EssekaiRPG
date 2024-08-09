using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FacialHairGroupHandler : MonoBehaviour
{
    public string[] namesForSearch; // Массив имен для поиска
    public Button[] arrowLeftButtons; // Массив кнопок стрелки влево
    public Button[] arrowRightButtons; // Массив кнопок стрелки вправо

    private List<List<GameObject>> facialHairGroups; // Список списков объектов
    private int[] currentIndexes; // Массив текущих индексов для каждого списка

    void Start()
    {
        facialHairGroups = new List<List<GameObject>>();
        currentIndexes = new int[namesForSearch.Length];

        for (int i = 0; i < namesForSearch.Length; i++)
        {
            // Инициализация индексов
            currentIndexes[i] = -1;

            // Найти объект с указанным именем
            Transform facialHairGroup = FindChildByName(transform, namesForSearch[i]);

            if (facialHairGroup != null)
            {
                // Получить все дочерние объекты
                List<GameObject> children = GetAllChildren(facialHairGroup);
                facialHairGroups.Add(children);

                // Убедиться, что есть объекты для переключения
                if (children.Count > 0)
                {
                    currentIndexes[i] = 0;
                    ActivateObject(i, currentIndexes[i]);
                }

                // Назначить обработчики событий для кнопок
                int index = i; // Локальная копия для использования в лямбда-выражении
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
            SaveCurrentActiveElement(groupIndex);
        }
    }

    private void OnArrowRightClick(int groupIndex)
    {
        if (currentIndexes[groupIndex] < facialHairGroups[groupIndex].Count - 1)
        {
            ActivateObject(groupIndex, currentIndexes[groupIndex] + 1);
            SaveCurrentActiveElement(groupIndex);
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

    private void SaveCurrentActiveElement(int groupIndex)
    {
        string activeElementName = facialHairGroups[groupIndex][currentIndexes[groupIndex]].name;
        PlayerPrefs.SetString(namesForSearch[groupIndex], activeElementName);
        PlayerPrefs.Save();
        Debug.Log($"Saved active element '{activeElementName}' for group '{namesForSearch[groupIndex]}'");
    }


    private void DisplaySavedElements()
    {
        Debug.Log("Saved elements:");
        foreach (string key in namesForSearch)
        {
            string value = PlayerPrefs.GetString(key, "not found");
            Debug.Log($"{key}: {value}");
        }
    }
    public void AddSavedElementsToForm(WWWForm form)
    {
        foreach (string key in namesForSearch)
        {
            if (PlayerPrefs.HasKey(key))
            {
                string value = PlayerPrefs.GetString(key);
                form.AddField(key, value);
            }
            else
            {
                Debug.LogWarning($"PlayerPrefs does not contain key: {key}");
            }
        }
    }
}

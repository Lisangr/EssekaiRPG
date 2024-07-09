using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    // Список объектов, который нужно переключать
    public GameObject[] objects;
    private int currentIndex = 0;

    private void Start()
    {
        // Убедимся, что только первый объект активен при запуске
        UpdateObjectStates();
    }

    private void Update()
    {
        // Для примера будем переключать объекты при нажатии клавиши Space
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            SwitchToNextObject();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            SwitchToPreviousObject();
        }
    }

    public void SwitchToNextObject()
    {
        // Отключаем текущий объект
        objects[currentIndex].SetActive(false);

        // Увеличиваем индекс, чтобы перейти к следующему объекту
        currentIndex++;

        // Если индекс выходит за пределы массива, возвращаем его к 0
        if (currentIndex >= objects.Length)
        {
            currentIndex = 0;
        }

        // Активируем следующий объект
        objects[currentIndex].SetActive(true);
    }
    public void SwitchToPreviousObject()
    {
        // Отключаем текущий объект
        objects[currentIndex].SetActive(false);

        // Уменьшаем индекс, чтобы перейти к предыдущему объекту
        currentIndex--;

        // Если индекс выходит за пределы массива, возвращаем его к последнему объекту
        if (currentIndex < 0)
        {
            currentIndex = objects.Length - 1;
        }

        // Активируем предыдущий объект
        objects[currentIndex].SetActive(true);
    }
    private void UpdateObjectStates()
    {
        // Активируем только текущий объект, остальные отключаем
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == currentIndex);
        }
    }
}

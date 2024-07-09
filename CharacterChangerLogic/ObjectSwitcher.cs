using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    // ������ ��������, ������� ����� �����������
    public GameObject[] objects;
    private int currentIndex = 0;

    private void Start()
    {
        // ��������, ��� ������ ������ ������ ������� ��� �������
        UpdateObjectStates();
    }

    private void Update()
    {
        // ��� ������� ����� ����������� ������� ��� ������� ������� Space
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
        // ��������� ������� ������
        objects[currentIndex].SetActive(false);

        // ����������� ������, ����� ������� � ���������� �������
        currentIndex++;

        // ���� ������ ������� �� ������� �������, ���������� ��� � 0
        if (currentIndex >= objects.Length)
        {
            currentIndex = 0;
        }

        // ���������� ��������� ������
        objects[currentIndex].SetActive(true);
    }
    public void SwitchToPreviousObject()
    {
        // ��������� ������� ������
        objects[currentIndex].SetActive(false);

        // ��������� ������, ����� ������� � ����������� �������
        currentIndex--;

        // ���� ������ ������� �� ������� �������, ���������� ��� � ���������� �������
        if (currentIndex < 0)
        {
            currentIndex = objects.Length - 1;
        }

        // ���������� ���������� ������
        objects[currentIndex].SetActive(true);
    }
    private void UpdateObjectStates()
    {
        // ���������� ������ ������� ������, ��������� ���������
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == currentIndex);
        }
    }
}

using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    public GameObject[] objects;
    private int currentIndex = 0; // Сделаем currentIndex публичным

    private void Start()
    {
        UpdateObjectStates();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchToNextObject();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SwitchToPreviousObject();
        }
    }

    public void SwitchToNextObject()
    {
        objects[currentIndex].SetActive(false);
        currentIndex++;
        if (currentIndex >= objects.Length)
        {
            currentIndex = 0;
        }
        objects[currentIndex].SetActive(true);
    }

    public void SwitchToPreviousObject()
    {
        objects[currentIndex].SetActive(false);
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = objects.Length - 1;
        }
        objects[currentIndex].SetActive(true);
    }

    private void UpdateObjectStates()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(i == currentIndex);
        }
    }
}

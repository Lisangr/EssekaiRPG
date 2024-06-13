using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ObjectChecker : MonoBehaviour
{
    public Text pressFText; // UI текст для отображения сообщения
    public string[] interactableObjects; // Список имен объектов для взаимодействия

    public delegate void PlayerAction();
    public static event PlayerAction OnPressButtonF;

    private GameObject currentInteractableObject;

    void Start()
    {
        pressFText.gameObject.SetActive(false); // Скрываем текст по умолчанию
    }

    void Update()
    {
        // Если нажата клавиша F и есть объект для взаимодействия
        if (currentInteractableObject != null && Input.GetKeyDown(KeyCode.F))
        {
            InteractWithObject(currentInteractableObject);
            pressFText.gameObject.SetActive(false);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsInteractable(other.gameObject))
        {
            pressFText.text = "Нажмите F";
            pressFText.gameObject.SetActive(true);
            currentInteractableObject = other.gameObject;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentInteractableObject == other.gameObject)
        {
            pressFText.gameObject.SetActive(false);
            currentInteractableObject = null;            
        }
    }

    bool IsInteractable(GameObject obj)
    {
        foreach (string name in interactableObjects)
        {
            if (obj.name.Contains(name))
            {
                return true;
            }
        }
        return false;
    }

    void InteractWithObject(GameObject obj)
    {
        OnPressButtonF?.Invoke();
    }
}
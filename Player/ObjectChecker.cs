using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ObjectChecker : MonoBehaviour
{
    public Text interactionText; // UI текст для отображения сообщения
    public string[] interactableObjects; // Список имен объектов для взаимодействия

    public delegate void PlayerAction();
    public static event PlayerAction OnPressButtonF;

    private GameObject currentInteractableObject;

    void Start()
    {
        interactionText.gameObject.SetActive(false); // Скрываем текст по умолчанию
    }

    void Update()
    {
        // Если нажата клавиша F и есть объект для взаимодействия
        if (currentInteractableObject != null && Input.GetKeyDown(KeyCode.F))
        {
            InteractWithObject(currentInteractableObject);
            interactionText.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsInteractable(other.gameObject))
        {
            interactionText.text = "Нажмите F";
            interactionText.gameObject.SetActive(true);
            currentInteractableObject = other.gameObject;            
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (currentInteractableObject == other.gameObject)
        {
            interactionText.gameObject.SetActive(false);
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
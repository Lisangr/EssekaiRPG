using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ObjectChecker : MonoBehaviour
{
    public Text pressFText; // UI ����� ��� ����������� ���������
    public string[] interactableObjects; // ������ ���� �������� ��� ��������������

    public delegate void PlayerAction();
    public static event PlayerAction OnPressButtonF;

    private GameObject currentInteractableObject;

    void Start()
    {
        pressFText.gameObject.SetActive(false); // �������� ����� �� ���������
    }

    void Update()
    {
        // ���� ������ ������� F � ���� ������ ��� ��������������
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
            pressFText.text = "������� F";
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
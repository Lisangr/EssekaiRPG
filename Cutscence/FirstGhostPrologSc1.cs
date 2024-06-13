using UnityEngine;

public class FirstGhostPrologSc1 : MonoBehaviour
{
    public GameObject ghostNpc;
    public GameObject dialogPanel;

    private bool isInstantiate = false;
    private bool playerInTrigger = false; // Добавленная переменная для отслеживания игрока в триггере

    private void Start()
    {

        dialogPanel.SetActive(false);
        ghostNpc.gameObject.SetActive(false); // Убедитесь, что ghostNpc неактивен в начале
    }

    private void Update()
    {
        if (playerInTrigger && !isInstantiate && Input.GetKeyDown(KeyCode.F))
        {
            ghostNpc.gameObject.SetActive(true);
            dialogPanel.SetActive(true);
            isInstantiate = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            playerInTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            playerInTrigger = false;
            isInstantiate = false;
            ghostNpc.gameObject.SetActive(false);
            dialogPanel.SetActive(false);
        }
    }
}

using Cinemachine;
using UnityEngine;

public class ForCMCameras : MonoBehaviour
{
    public Camera mainCamera;
    public CinemachineVirtualCamera cinemachineCamera;
    public CinemachineVirtualCamera cinemachineForPlayerCamera;
    public GameObject traderPanel;
    public GameObject playerInventoryPanel;

    private bool isCMActiveted =false;

    public delegate void PlayerAction();
    public static event PlayerAction GoAwayFromNPC;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.F))
        {
            isCMActiveted = !isCMActiveted;

            if (isCMActiveted)
            {
                cinemachineForPlayerCamera.gameObject.SetActive(false);
                cinemachineCamera.gameObject.SetActive(true);
                traderPanel.SetActive(true);
                playerInventoryPanel.SetActive(true);
            }
            else 
            {
                cinemachineForPlayerCamera.gameObject.SetActive(true);
                cinemachineCamera.gameObject.SetActive(false);
                traderPanel.SetActive(false);
                playerInventoryPanel.SetActive(false);
                GoAwayFromNPC?.Invoke();
            }
        }
        
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.W)
            || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            cinemachineForPlayerCamera.gameObject.SetActive(true);
            cinemachineCamera.gameObject.SetActive(false);
            GoAwayFromNPC?.Invoke();
            isCMActiveted = false;
        }
    }    
}
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfoPanelForSkill : MonoBehaviour
{
    public GameObject panel;
    private Coroutine showPanelCoroutine;
    public void OnPointerEnter()
    {
        showPanelCoroutine = StartCoroutine(ShowPanelWithDelay(1f));
    }

    public void OnPointerExit()
    {
        if (showPanelCoroutine != null)
        {
            StopCoroutine(showPanelCoroutine);
            showPanelCoroutine = null;
        }
        panel.SetActive(false);
    }

    private IEnumerator ShowPanelWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        panel.SetActive(true);
    }
}
using UnityEngine;

public class SkillTreeChangeTab : MonoBehaviour
{
    public GameObject waterPanel;
    public GameObject firePanel;
    public GameObject earthPanel;
    public GameObject airPanel;
    public GameObject darkPanel;
    public GameObject godPanel;
    public GameObject healingPanel;
    public GameObject curvePanel;
    public GameObject resistancePanel;
    public GameObject defendPanel;
    public GameObject attackPanel;
    public GameObject assasinPanel;
    public GameObject archerPanel;
    private void OnEnable()
    {
        DeactivateAllPanels();
        waterPanel.SetActive(true);
    }

    private void DeactivateAllPanels()
    {
        waterPanel.SetActive(false);
        firePanel.SetActive(false);
        earthPanel.SetActive(false);
        airPanel.SetActive(false);
        darkPanel.SetActive(false);
        godPanel.SetActive(false);
        healingPanel.SetActive(false);
        curvePanel.SetActive(false);
        resistancePanel.SetActive(false);
        defendPanel.SetActive(false);
        attackPanel.SetActive(false);
        assasinPanel.SetActive(false);
        archerPanel.SetActive(false);
    }

    public void OnClickEnter(GameObject activateGameObject)
    {
        DeactivateAllPanels();
        activateGameObject.SetActive(true);
    }
}

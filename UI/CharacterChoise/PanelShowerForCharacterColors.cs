using UnityEngine;

public class PanelShowerForCharacterColors : MonoBehaviour
{
    public GameObject skinPanel;
    public GameObject eyesPanel;
    public GameObject bodyartPanel;
    public GameObject hairPanel;
    public GameObject currentPanel;
    public GameObject nextPanel;
    public GameObject metalColorPanel;
    public GameObject leatherColorPanel;
    public GameObject metalSecondaryColorPanel;
    public GameObject leatherSecondaryColorPanel;
    public GameObject secondaryColorPanel;
    public GameObject darkMetalColorPanel;
    public GameObject primaryColorPanel;
    public GameObject notForFemalePole;
    public GameObject secondCharacterPanel;

    private GameObject panel;

    void Start()
    {
        SetPanelsActive(false);
    }
    private void Update()
    {
        if (RaceAndGender.isFemale)
        {
            notForFemalePole.SetActive(false);
        }
        else
        {
            notForFemalePole.SetActive(true);
        }
    }
    private void SetPanelsActive(bool state)
    {
        skinPanel.SetActive(state);
        eyesPanel.SetActive(state);
        bodyartPanel.SetActive(state);
        hairPanel.SetActive(state);
        metalColorPanel.SetActive(state);
        leatherColorPanel.SetActive(state);
        metalSecondaryColorPanel.SetActive(state);
        leatherSecondaryColorPanel.SetActive(state);
        secondaryColorPanel.SetActive(state);
        darkMetalColorPanel.SetActive(state);
        primaryColorPanel.SetActive(state);
        secondCharacterPanel.SetActive(state);
    }

    private void SetActivePanel(GameObject panelToActivate)
    {
        panel?.SetActive(false);
        panelToActivate.SetActive(true);
        panel = panelToActivate;
    }

    public void OnSkinPanel() => SetActivePanel(skinPanel);
    public void OnEyesPanel() => SetActivePanel(eyesPanel);
    public void OnBodyartPanel() => SetActivePanel(bodyartPanel);
    public void OnHairPanel() => SetActivePanel(hairPanel);
    public void OnMetalColorPanel() => SetActivePanel(metalColorPanel);
    public void OnLeatherColorPanel() => SetActivePanel(leatherColorPanel);
    public void OnMetalSecondaryColorPanel() => SetActivePanel(metalSecondaryColorPanel);
    public void OnLeatherSecondaryColorPanel() => SetActivePanel(leatherSecondaryColorPanel);
    public void OnSecondaryColorPanel() => SetActivePanel(secondaryColorPanel);
    public void OnDarkMetalColorPanel() => SetActivePanel(darkMetalColorPanel);
    public void OnPrimaryColorPanel() => SetActivePanel(primaryColorPanel);

    public void OnClickOk() => panel?.SetActive(false);
    public void OnContinueClick()
    {
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
    }
    public void OnPreviousClick()
    {
        currentPanel.SetActive(true);
        nextPanel.SetActive(false);
    }
}

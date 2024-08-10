using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssignedSkillAtSkillTreeAndSkillSO : MonoBehaviour
{
    public Image skillImageTarget;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI skillNameText;
    public SkillSO skillSO;

    private void Start()
    {
        // Check if the SkillSO reference is not null
        if (skillSO != null)
        {
            // Check if the target sprite in SkillSO is not null
            if (skillSO.targetImage != null)
            {
                // Directly assign the sprite from SkillSO to the Image component
                skillImageTarget.sprite = skillSO.targetImage;
            }
            else
            {
                Debug.LogWarning("No target image set in SkillSO.");
            }

            // Set the description and skill name text fields
            descriptionText.text = skillSO.description;
            skillNameText.text = skillSO.skillName;
        }
        else
        {
            Debug.LogWarning("SkillSO reference is missing.");
        }
    }
}

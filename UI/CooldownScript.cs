using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CooldownScript : MonoBehaviour
{
    public Transform player;
    public SkillSlot[] skillSlots; // Массив ячеек скиллов
    public static bool canCast;
    private static Image previousTargetImage;
    private Player playerObject;
    public static int currentMP, maxMP;
    public TextMeshProUGUI mpText; // Добавьте ссылку на текстовое поле

    public int manaRegenAmount = 10; // Количество восстанавливаемой маны
    private float manaRegenInterval = 3f; // Интервал восстановления маны в секундах
    private float manaRegenTimer;
    void Start()
    {
        playerObject = FindObjectOfType<Player>().GetComponent<Player>();
        maxMP = playerObject.MaxMP;
        currentMP = maxMP;

        canCast = true;
    }

    void Update()
    {
        if (currentMP < maxMP)
        {
            manaRegenTimer += Time.deltaTime;        
            if (manaRegenTimer >= manaRegenInterval)
            {
                RegenerateMana();
                manaRegenTimer = 0f;
            }
        }
        UpdateMPText(); 

        foreach (var slot in skillSlots)
        {
            if (Input.GetKeyDown(slot.skill.key) && canCast)
            {
                slot.OnClickUseSkill();

                if (previousTargetImage != null && previousTargetImage != slot.targetImage)
                {
                    ChangeAlpha(previousTargetImage, 0f);
                }
                previousTargetImage = slot.targetImage;
            }
        }
    }

    private void ChangeAlpha(Image img, float newAlpha)
    {
        if (img != null)
        {
            Color currentColor = img.color;
            currentColor.a = newAlpha;
            img.color = currentColor;
        }
    }

    public bool CanUseSkill(SkillSO skill)
    {
        return currentMP >= skill.costMP;
    }

    public void DeductMP(SkillSO skill)
    {
        if (currentMP >= skill.costMP)
        {
            currentMP -= skill.costMP;
            Debug.Log("Skill used: " + skill.name + ". Remaining MP: " + currentMP);
        }
        else
        {
            Debug.Log("Not enough MP to use skill: " + skill.name);
        }
    }
    private void UpdateMPText()
    {
        if (mpText != null)
        {
            mpText.text = "MP: " + currentMP + "/" + maxMP;
        }
    }
    private void RegenerateMana()
    {
        currentMP = Mathf.Min(currentMP + manaRegenAmount, maxMP);
        Debug.Log("Mana regenerated. Current MP: " + currentMP);
    }
}
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
    public Image mpImage;

    private EnemyMainInfo enemyMainInfo;
    private float manaRegenInterval = 3f; // Интервал восстановления маны в секундах
    private float manaRegenTimer;

    void Start()
    {
        playerObject = FindObjectOfType<Player>().GetComponent<Player>();
        player = playerObject.transform;
        maxMP = playerObject.MaxMP;
        currentMP = maxMP;
        mpImage.fillAmount = 1;

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

            enemyMainInfo = FindObjectOfType<EnemyMainInfo>();
            enemyMainInfo.TakeMagicDamage(skill.healing);
            enemyMainInfo.TakePhisicalDamage(skill.damage);

            Debug.Log("Skill used: " + skill.skillName + ", MP spent: " + currentMP);
        }
        else
        {
            Debug.Log("Not enough MP to use skill: " + skill.skillName);
        }
    }

    private void UpdateMPText()
    {
        if (mpText != null)
        {
            mpText.text = "MP: " + currentMP + "/" + maxMP;
            mpImage.fillAmount = (float)currentMP / maxMP; // Обновление заполненности изображения
        }
    }

    private void RegenerateMana()
    {
        currentMP = Mathf.Min(currentMP + manaRegenAmount, maxMP);
        Debug.Log("Mana regenerated. Current MP: " + currentMP);
    }
}
    /*
    public Transform player;
    public SkillSlot[] skillSlots; // Массив ячеек скиллов
    public static bool canCast;
    private static Image previousTargetImage;
    private Player playerObject;
    public static int currentMP, maxMP;
    public TextMeshProUGUI mpText; // Добавьте ссылку на текстовое поле
    public int manaRegenAmount = 10; // Количество восстанавливаемой маны
    public Image mpImage;

    private EnemyMainInfo enemyMainInfo;
    private float manaRegenInterval = 3f; // Интервал восстановления маны в секундах
    private float manaRegenTimer;
    void Start()
    {
        playerObject = FindObjectOfType<Player>().GetComponent<Player>();
        player = FindObjectOfType<Player>().GetComponent<Transform>();
        maxMP = playerObject.MaxMP;
        currentMP = maxMP;
        mpImage.fillAmount = 1;

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
            

            enemyMainInfo = FindObjectOfType<EnemyMainInfo>();
            enemyMainInfo.TakeMagicDamage(skill.healing);
            enemyMainInfo.TakePhisicalDamage(skill.damage);

            Debug.Log("Скилл использован" + skill.name + "маны потрачено" + currentMP);
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
            mpImage.fillAmount = (float)currentMP / maxMP; // Обновление заполненности изображения
        }
    }
    private void RegenerateMana()
    {
        currentMP = Mathf.Min(currentMP + manaRegenAmount, maxMP);
        Debug.Log("Mana regenerated. Current MP: " + currentMP);
    }
}*/

using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillSO skill;
    public Image fillImage;
    public Image targetImage;

    private bool isCoolingDown;
    private float startFillAmount = 1f;
    private static Image previousTargetImage;
    private CooldownScript cooldownScript;

    void Start()
    {
        if (skill != null)
        {
            fillImage.fillAmount = 0;
            isCoolingDown = false;
        }

        cooldownScript = FindObjectOfType<CooldownScript>();
    }

    void Update()
    {
        if (isCoolingDown && fillImage.fillAmount > 0)
        {
            fillImage.fillAmount -= startFillAmount / skill.countdown * Time.deltaTime;
        }
        if (fillImage.fillAmount <= 0)
        {
            OnCooldownComplete();
        }
    }

    public void OnClickUseSkill()
    {
        if ((!isCoolingDown) && CooldownScript.canCast && skill != null)
        {
            if (cooldownScript.CanUseSkill(skill))
            {
                isCoolingDown = true;
                fillImage.fillAmount = startFillAmount;

                // Создание снаряда и передача позиции игрока или врага в зависимости от направления
                Transform spawnPosition = skill.isPlayerToEnemy ? cooldownScript.player : ClickHandler.enemyPosition;
                Transform targetPosition = skill.isPlayerToEnemy ? ClickHandler.enemyPosition : cooldownScript.player;

                GameObject projectile = Instantiate(skill.skillPrefab, spawnPosition.position, spawnPosition.rotation);
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                if (projectileScript != null)
                {
                    projectileScript.SetTarget(targetPosition);
                }

                ChangeAlpha(targetImage, 255);

                if (previousTargetImage != null && previousTargetImage != targetImage)
                {
                    ChangeAlpha(previousTargetImage, 0f);
                }
                previousTargetImage = targetImage;

                cooldownScript.DeductMP(skill);
            }
            else
            {
                Debug.Log("Not enough MP to use skill: " + skill.name);
            }
        }
    }

    private void OnCooldownComplete()
    {
        isCoolingDown = false;
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
}

using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public SkillSO skill;
    public KeyCode key;
    public bool useCtrl;
    public bool useShift;
    public Image fillImage;
    public Image targetImage;
    public Text hotkey;

    private bool isCoolingDown;
    private float startFillAmount = 1f;
    private CooldownScript cooldownScript;

    void Start()
    {
        string keyText = key.ToString().Replace("Alpha", "");
        if (useCtrl) keyText = "Ctrl + " + keyText;
        if (useShift) keyText = "Shift + " + keyText;
        hotkey.text = keyText;

        if (skill != null)
        {
            fillImage.fillAmount = 0;
            isCoolingDown = false;
        }

        cooldownScript = FindObjectOfType<CooldownScript>();
    }

    void Update()
    {
        bool isCtrlPressed = !useCtrl || Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl);
        bool isShiftPressed = !useShift || Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        bool isKeyPressed = Input.GetKeyDown(key);

        if (isCtrlPressed && isShiftPressed && isKeyPressed && !isCoolingDown)
        {
            OnClickUseSkill();
        }

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
        if (!isCoolingDown && CooldownScript.canCast && skill != null)
        {
            if (cooldownScript.CanUseSkill(skill))
            {
                isCoolingDown = true;
                fillImage.fillAmount = startFillAmount;

                Transform spawnPosition = skill.isPlayerToEnemy ? cooldownScript.player : ClickHandler.enemyPosition;
                Transform targetPosition = skill.isPlayerToEnemy ? ClickHandler.enemyPosition : cooldownScript.player;

                GameObject projectile = Instantiate(skill.skillPrefab, spawnPosition.position, spawnPosition.rotation);
                Projectile projectileScript = projectile.GetComponent<Projectile>();
                if (projectileScript != null)
                {
                    projectileScript.SetTarget(targetPosition);
                }

                ChangeAlpha(targetImage, 1f);

                cooldownScript.DeductMP(skill);
            }
            else
            {
                Debug.Log("Not enough MP to use skill: " + skill.skillName);
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

    public void SetSkill(SkillSO newSkill)
    {
        skill = newSkill;
        Debug.Log("SkillSO has been updated in SkillSlot.");
    }

    public void MoveChildFromCategorySkill(Transform child)
    {
        if (child != null)
        {
            Image draggedImage = child.GetComponent<Image>();
            if (draggedImage != null && targetImage != null)
            {
                // Directly assign the sprite from the dragged image to the target image.
                targetImage.sprite = draggedImage.sprite;
                targetImage.color = Color.white;

                // Reparent and reposition the child object.
                child.SetParent(this.transform);
                child.localPosition = Vector3.zero;
                child.gameObject.SetActive(true);

                Debug.Log("Skill image has been set in SkillSlot.");
            }
            else
            {
                Debug.LogWarning("Dragged object or targetImage does not have Image component.");
            }
        }
        else
        {
            Debug.LogWarning("Attempted to move a null child object.");
        }
    }
}
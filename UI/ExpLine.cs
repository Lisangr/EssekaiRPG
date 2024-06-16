using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpLine : MonoBehaviour
{
    public Image expImage;
    public TextMeshProUGUI expText;
    private int expForLevelUp = 100;
    private int currentExp;

    void Start()
    {
        currentExp = 0;
    }
    private void OnEnable()
    {
        EnemyMainInfo.OnEnemyDeath += AddExperience;
    }
    private void OnDisable()
    {
        EnemyMainInfo.OnEnemyDeath -= AddExperience;
    }
    private void AddExperience(int exp)
    {
        currentExp += exp;

        if (currentExp >= expForLevelUp)
        {
            currentExp = expForLevelUp;  // ќграничиваем максимальное значение опыта
        }

        UpdateExpUI();
    }

    private void UpdateExpUI()
    {
        expImage.fillAmount = (float)currentExp / expForLevelUp;
        expText.text = "EXP: " + currentExp + "/" + expForLevelUp;
    }
}

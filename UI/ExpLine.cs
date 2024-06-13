using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExpLine : MonoBehaviour
{
    public Image expImage;
    public TextMeshProUGUI expText;
    private int expForLevelUp;
    private int currentExp;

    void Start()
    {
        currentExp = 0;
    }

    void Update()
    {
        int exp = MainInfo.experience;
        currentExp += exp;

        expImage.fillAmount = currentExp / 100;
        expText.text = "EXP: " + currentExp + "/" + expForLevelUp;
    }
}

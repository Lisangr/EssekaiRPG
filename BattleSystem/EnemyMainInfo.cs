using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMainInfo : MonoBehaviour
{
    public int maxHP;
    public Image hpImage;
    public Image healingHPImage;
    public int experience = 10;  
    public AudioClip[] audioClips; // Массив звуковых клипов

    private AudioSource audioSource;
    public int currentHP, currentHealingHP, tempHP, tempHealingHP;
    private float maxHealingHP = 0;

    public delegate void DeathAction(int exp);
    public static event DeathAction OnEnemyDeath;
    void Start()
    {
        currentHP = maxHP;
        currentHealingHP = 0;
        maxHealingHP += 200;
        audioSource = GetComponent<AudioSource>();
        healingHPImage.fillAmount = 0;
        hpImage.fillAmount = 1;
    }

    void Update()
    {
        if (currentHP <= 0 || currentHealingHP >= maxHealingHP)
        {
            int exp = experience;
            OnEnemyDeath?.Invoke(exp); // событие для лута
            Destroy(this.gameObject);
        }      
    }
    public void TakePhisicalDamage(int damage)
    {
        if (currentHealingHP >= 0)
        {
            tempHP = currentHP + currentHealingHP - damage;
            healingHPImage.fillAmount -= currentHealingHP / maxHealingHP;
            
            if (healingHPImage.fillAmount <= 0)
            { 
                tempHealingHP = 0;
                tempHP = currentHP - damage;
                hpImage.fillAmount -= tempHP / maxHP;
            }
        }
        else
        {
            tempHP = currentHP - damage;
            hpImage.fillAmount -= tempHP / maxHP;
        }
    }
    public void TakeMagicDamage(int healing)
    {
        currentHealingHP += healing;
        healingHPImage.fillAmount += currentHealingHP / maxHealingHP;
        Debug.Log("Скилл использован ВЫЛЕЧЕНО   " + healing);
    }

    public void OnAttak() 
    {
        if (audioClips.Length > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Length);
            AudioClip randomClip = audioClips[randomIndex];

            audioSource.clip = randomClip;
            audioSource.Play();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainInfo : MonoBehaviour
{
    public int maxHP;
    public Image hpImage;
    public Image healingHPImage;
    public static int experience;  
    public AudioClip[] audioClips; // Массив звуковых клипов

    private AudioSource audioSource;
    private int currentHP, currentHealingHP, maxHealingHP;

    public delegate void DeathAction();
    public static event DeathAction OnEnemyDeath;
    void Start()
    {
        currentHP = maxHP;
        currentHealingHP = 0;
        maxHealingHP += maxHP;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (currentHP <= 0 || currentHealingHP >= maxHealingHP)
        { 
            OnEnemyDeath?.Invoke(); 
            Destroy(this.gameObject);
        }     
        hpImage.fillAmount = currentHP / 100;
        healingHPImage.fillAmount = currentHealingHP / 100;
    }
    public void TakePhisicalDamage(int damage)
    {
        currentHP -= damage;
    }
    public void TakeMagicDamage(int healing)
    {
        currentHealingHP += healing;
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

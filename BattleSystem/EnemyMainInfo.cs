using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMainInfo : MonoBehaviour
{
    public int maxHP;
    public Image hpImage;
    public Image healingHPImage;
    public int experience = 10;
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private int currentHP, currentHealingHP;
    private float maxHealingHP;

    public delegate void DeathAction(int exp);
    public static event DeathAction OnEnemyDeath;

    public delegate void LootAction();
    public static event LootAction OnEnemyDestroy;
    void Start()
    {
        currentHP = maxHP;
        currentHealingHP = 0;
        maxHealingHP = 2 * maxHP;
        audioSource = GetComponent<AudioSource>();
        healingHPImage.fillAmount = 0;
        hpImage.fillAmount = 1;

        // Установка параметров 3D звука
        audioSource.spatialBlend = 1.0f; // Использование 3D звука
        audioSource.maxDistance = 2.0f; // Установка максимальной дистанции звука на 2 метра

    }

    void Update()
    {
        if (currentHP <= 0 || currentHealingHP >= maxHealingHP)
        {
            int exp = experience;
            OnEnemyDeath?.Invoke(exp);
            OnEnemyDestroy?.Invoke();
            Destroy(this.gameObject);
        }
    }

    public void TakePhisicalDamage(int damage)
    {
        if (currentHealingHP > 0)
        {
            int totalHP = currentHP + currentHealingHP;
            totalHP -= damage;

            if (totalHP < currentHP)
            {
                currentHealingHP = 0;
                currentHP = Mathf.Max(totalHP, 0);
            }
            else
            {
                currentHealingHP = Mathf.Max(totalHP - currentHP, 0);
            }
        }
        else
        {
            currentHP = Mathf.Max(currentHP - damage, 0);
        }

        UpdateHealthBars();
    }

    public void TakeMagicDamage(int healing)
    {
        int missingHP = maxHP - currentHP;

        if (healing > missingHP)
        {
            currentHP = maxHP;
            currentHealingHP = Mathf.Min(currentHealingHP + (healing - missingHP), (int)maxHealingHP);
        }
        else
        {
            currentHP += healing;
        }

        UpdateHealthBars();
        Debug.Log("Скилл использован ВЫЛЕЧЕНО: " + healing);
    }

    private void UpdateHealthBars()
    {
        hpImage.fillAmount = (float)currentHP / maxHP;
        healingHPImage.fillAmount = (float)currentHealingHP / maxHealingHP;
    }

    public void OnAttak()
    {
        if (audioClips.Length > 0 && !audioSource.isPlaying)
        {
            int randomIndex = Random.Range(0, audioClips.Length);
            AudioClip randomClip = audioClips[randomIndex];

            audioSource.clip = randomClip;
            audioSource.Play();
        }
    }
}

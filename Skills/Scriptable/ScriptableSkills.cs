// Scriptable Object для скилла
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/Skill")]
public class SkillSO : ScriptableObject
{   
    public GameObject skillPrefab;
    public Sprite targetImage;

    public int costMP;
    public float countdown;
    public float distance;

    public bool isPlayerToEnemy; // Направление, если false, то от врага к игроку, например вампирик
    public int damage = 0;
    public int healing;

    public string skillName;
    public string description;
}

// Scriptable Object для скилла
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/Skill")]
public class SkillSO : ScriptableObject
{
    public KeyCode key;
    public GameObject skillPrefab;
    public GameObject targetImage;
    public int costMP;
    public float timeFill;
    public bool isPlayerToEnemy; // Направление, если false, то от врага к игроку, например вампирик
    public int damage = 0;
    public int healing;
}

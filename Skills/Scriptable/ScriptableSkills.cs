// Scriptable Object ��� ������
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/Skill")]
public class SkillSO : ScriptableObject
{
    public KeyCode key;
    public GameObject skillPrefab;
    public GameObject targetImage;
    public int costMP;
    public float timeFill;
    public bool isPlayerToEnemy; // �����������, ���� false, �� �� ����� � ������, �������� ��������
    public int damage = 0;
    public int healing;
}

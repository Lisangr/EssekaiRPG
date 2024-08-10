// Scriptable Object ��� ������
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

    public bool isPlayerToEnemy; // �����������, ���� false, �� �� ����� � ������, �������� ��������
    public int damage = 0;
    public int healing;

    public string skillName;
    public string description;
}

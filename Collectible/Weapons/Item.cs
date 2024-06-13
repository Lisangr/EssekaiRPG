// Scriptable Object ��� ������� ��������� � ���������
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public GameObject prefab = null;
    public Sprite icon = null;
    public int quantity = 1;
    public int sellPrice;
    public int buyPrice;
    public int itemWeight;
}

// Scriptable Object для подбора предметов в инвентарь
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item/New Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public GameObject prefab = null;
    public Sprite icon = null;
    public int quantity = 1;
}

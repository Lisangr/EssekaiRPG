using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootInfo : MonoBehaviour
{
    public List<Item> items;

    private void OnEnable()
    {
        EnemyMainInfo.OnEnemyDestroy += SpawnLoot;
    }

    private void OnDisable()
    {
        EnemyMainInfo.OnEnemyDestroy -= SpawnLoot;
    }

    private void SpawnLoot()
    {
        if (items == null || items.Count == 0)
            return;

        Item randomItem = items[Random.Range(0, items.Count)];

        if (randomItem != null && randomItem.prefab != null)
        {
            GameObject spawnedItem = Instantiate(randomItem.prefab, transform.position, Quaternion.identity);
            spawnedItem.name = randomItem.itemName;

            ItemPickup itemPickup = spawnedItem.GetComponent<ItemPickup>();
            if (itemPickup != null)
            {
                itemPickup.item = randomItem;
                itemPickup.InitializeItem();
                Debug.Log($"Spawned loot: {randomItem.itemName}");
            }
            else
            {
                Debug.LogWarning($"ItemPickup component not found on prefab for item '{randomItem.itemName}'");
            }
        }
        else
        {
            Debug.LogWarning($"Prefab for item '{randomItem?.itemName}' not found or is null.");
        }
    }
}

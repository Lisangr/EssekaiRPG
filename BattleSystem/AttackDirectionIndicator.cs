using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDirectionIndicator : MonoBehaviour
{
    public GameObject attackAreaPrefab; // Префаб индикатора атаки
    private GameObject currentIndicator; // Текущий активный индикатор
    public float indicatorScale = 1.0f; // Масштаб индикатора

    void Update()
    {
        UpdateAttackIndicator();
    }

    public void UpdateAttackIndicator()
    {
        // Проецируем луч вниз от позиции врага
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            // Проверяем, что луч попал в поверхность с тегом Ground
            if (hit.collider.CompareTag("Ground"))
            {
                // Если индикатор еще не создан, создаем его
                if (currentIndicator == null)
                {
                    currentIndicator = Instantiate(attackAreaPrefab, hit.point, Quaternion.identity);
                    currentIndicator.transform.localScale = new Vector3(indicatorScale, indicatorScale, indicatorScale);
                }
                else
                {
                    // Обновляем позицию индикатора
                    currentIndicator.transform.position = hit.point;
                }

                // Обновляем направление индикатора на основании направления врага
                currentIndicator.transform.forward = transform.forward;
            }
        }
    }

    public void ShowIndicator()
    {
        if (currentIndicator != null)
        {
            currentIndicator.SetActive(true);
        }
    }

    public void HideIndicator()
    {
        if (currentIndicator != null)
        {
            currentIndicator.SetActive(false);
        }
    }
}

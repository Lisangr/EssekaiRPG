using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDirectionIndicator : MonoBehaviour
{
    public GameObject attackAreaPrefab; // ������ ���������� �����
    private GameObject currentIndicator; // ������� �������� ���������
    public float indicatorScale = 1.0f; // ������� ����������

    void Update()
    {
        UpdateAttackIndicator();
    }

    public void UpdateAttackIndicator()
    {
        // ���������� ��� ���� �� ������� �����
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, Mathf.Infinity))
        {
            // ���������, ��� ��� ����� � ����������� � ����� Ground
            if (hit.collider.CompareTag("Ground"))
            {
                // ���� ��������� ��� �� ������, ������� ���
                if (currentIndicator == null)
                {
                    currentIndicator = Instantiate(attackAreaPrefab, hit.point, Quaternion.identity);
                    currentIndicator.transform.localScale = new Vector3(indicatorScale, indicatorScale, indicatorScale);
                }
                else
                {
                    // ��������� ������� ����������
                    currentIndicator.transform.position = hit.point;
                }

                // ��������� ����������� ���������� �� ��������� ����������� �����
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

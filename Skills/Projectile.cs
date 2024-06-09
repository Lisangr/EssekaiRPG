using UnityEngine;

public class Projectile : MonoBehaviour
{   
    public float speed = 10f; // �������� �������� �������
    private Transform target; // ����, � ������� ����� ��������� ������

    void Update()
    {
        if (target != null)
        {
            // ����������� � ����
            Vector3 direction = (target.position - transform.position).normalized;
            // ����������� ������� � ����
            transform.position += direction * speed * Time.deltaTime;

            // �������� �� ���������� ����
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                // �������� ��� ���������� ���� (��������, ����������� �������)
                Destroy(gameObject);
            }
        }
        else
        {
            // ���� ���� �� �����������, ���������� ������
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // �������� �� ������������ � �����
        if (other.transform == target)
        {
            // �������� ��� ������������ � ����� (��������, ��������� �����)
            Debug.Log("Hit the target!");

            // ����������� �������
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
    
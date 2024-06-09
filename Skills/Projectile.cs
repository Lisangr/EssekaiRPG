using UnityEngine;

public class Projectile : MonoBehaviour
{   
    public float speed = 10f; // Скорость движения снаряда
    private Transform target; // Цель, к которой будет двигаться снаряд

    void Update()
    {
        if (target != null)
        {
            // Направление к цели
            Vector3 direction = (target.position - transform.position).normalized;
            // Перемещение снаряда к цели
            transform.position += direction * speed * Time.deltaTime;

            // Проверка на достижение цели
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                // Действие при достижении цели (например, уничтожение снаряда)
                Destroy(gameObject);
            }
        }
        else
        {
            // Если цель не установлена, уничтожаем снаряд
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверка на столкновение с целью
        if (other.transform == target)
        {
            // Действие при столкновении с целью (например, нанесение урона)
            Debug.Log("Hit the target!");

            // Уничтожение снаряда
            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
    
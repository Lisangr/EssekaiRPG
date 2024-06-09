using UnityEngine;

public class LightSpheres : MonoBehaviour
{
    private Transform player;
    public GameObject[] objectsToOrbit; // Массив объектов, которые будут вращаться
    public float orbitSpeed = 2f; // Скорость вращения
    public float orbitRadius = 5f; // Радиус орбиты
    private float[] randomAngles; // Массив для хранения случайных углов

    private void Awake()
    {
        player = FindObjectOfType<Player>().GetComponent<Transform>();
        randomAngles = new float[objectsToOrbit.Length];
        GenerateRandomAngles();
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.LogWarning("Player not found!");
            return;
        }

        if (objectsToOrbit == null || objectsToOrbit.Length == 0)
        {
            Debug.LogWarning("No objects to orbit!");
            return;
        }

        // Цикл по всем объектам, которые должны вращаться
        for (int i = 0; i < objectsToOrbit.Length; i++)
        {
            GameObject obj = objectsToOrbit[i];
            if (obj == null) continue;

            // Вычисляем угол вращения для этого кадра с учетом случайного угла
            float angle = Time.time * orbitSpeed + randomAngles[i];

            // Вычисляем позицию объекта на орбите
            Vector3 orbitPosition = new Vector3(
                Mathf.Cos(angle) * orbitRadius,
                0f,
                Mathf.Sin(angle) * orbitRadius
            );

            // Переводим позицию объекта в мировые координаты
            orbitPosition = player.position + orbitPosition + new Vector3(0, 1, 0);

            // Устанавливаем позицию объекта
            obj.transform.position = orbitPosition;
        }
    }

    // Метод для генерации случайных углов
    private void GenerateRandomAngles()
    {
        for (int i = 0; i < randomAngles.Length; i++)
        {
            randomAngles[i] = Random.Range(0f, Mathf.PI * 2);
        }
    }    
}

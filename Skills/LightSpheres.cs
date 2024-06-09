using UnityEngine;

public class LightSpheres : MonoBehaviour
{
    private Transform player;
    public GameObject[] objectsToOrbit; // ������ ��������, ������� ����� ���������
    public float orbitSpeed = 2f; // �������� ��������
    public float orbitRadius = 5f; // ������ ������
    private float[] randomAngles; // ������ ��� �������� ��������� �����

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

        // ���� �� ���� ��������, ������� ������ ���������
        for (int i = 0; i < objectsToOrbit.Length; i++)
        {
            GameObject obj = objectsToOrbit[i];
            if (obj == null) continue;

            // ��������� ���� �������� ��� ����� ����� � ������ ���������� ����
            float angle = Time.time * orbitSpeed + randomAngles[i];

            // ��������� ������� ������� �� ������
            Vector3 orbitPosition = new Vector3(
                Mathf.Cos(angle) * orbitRadius,
                0f,
                Mathf.Sin(angle) * orbitRadius
            );

            // ��������� ������� ������� � ������� ����������
            orbitPosition = player.position + orbitPosition + new Vector3(0, 1, 0);

            // ������������� ������� �������
            obj.transform.position = orbitPosition;
        }
    }

    // ����� ��� ��������� ��������� �����
    private void GenerateRandomAngles()
    {
        for (int i = 0; i < randomAngles.Length; i++)
        {
            randomAngles[i] = Random.Range(0f, Mathf.PI * 2);
        }
    }    
}

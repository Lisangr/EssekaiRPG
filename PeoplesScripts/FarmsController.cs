using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.AI;

public class FarmsController : MonoBehaviour
{
    private Transform patrolRoute;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;
    private Animator animator;
    public float minWaitTime = 3f;
    public float maxWaitTime = 10f;
    private bool isWaiting = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolRoute = GameObject.Find("FarmPoints").transform;
        animator = GetComponent<Animator>();
        InitializePatrolRoute();
        MoveToNextPatrolLocation();
    }

    void Update()
    {
        if (agent.isOnNavMesh && agent.remainingDistance < 2f && !agent.pathPending && !isWaiting)
        {
            StartCoroutine(WaitAndMove());
        }

        if (agent.velocity.magnitude > 0.1f)
        {
            animator.SetTrigger("Go");
        }
        else
        {
            animator.SetTrigger("Idle");
        }

    }

    private IEnumerator WaitAndMove()
    {        
        isWaiting = true;
        animator.SetTrigger("Idle");
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        MoveToNextPatrolLocation();
        isWaiting = false;
    }

    private void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;

        agent.destination = locations[locationIndex].position; // Устанавливаем пункт назначения для агента
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    private void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
        // Алгоритм Фишера-Йетса для случайной перестановки точек
        for (int i = locations.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = locations[i];
            locations[i] = locations[randomIndex];
            locations[randomIndex] = temp;
            // дополнительная проверка
            if (Vector3.Distance(temp.position, locations[i].position) < 1f)
            {
                locations.Remove(locations[i]);
                i--;
            }
        }
    }

   /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerExitedTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerExitedTrigger = true;
            StopAllCoroutines();
            isWaiting = false;
        }
    }
    /*
    private Transform patrolRoute;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;
    private Animator animator;
    public float minWaitTime = 3f;
    public float maxWaitTime = 10f;
    private bool isWaiting = false;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolRoute = GameObject.Find("Locations").transform;
        animator = GetComponent<Animator>();
        InitializePatrolRoute();
    }

    void Update()
    {
        if (agent.isOnNavMesh && agent.remainingDistance < 3f && !agent.pathPending && !isWaiting)
        {
            StartCoroutine(WaitAndMove());
        }

        if (agent.velocity.magnitude > 0.1f)
        {
            animator.SetTrigger("Go");
        }
        else
        {
            animator.SetTrigger("Idle");
        }
    }

    private IEnumerator WaitAndMove()
    {
        isWaiting = true;
        animator.SetTrigger("Idle");
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        MoveToNextPatrolLocation();
        isWaiting = false;
    }

    private void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;

        agent.destination = locations[locationIndex].position; // Устанавливаем пункт назначения для агента
        locationIndex = (locationIndex + 1) % locations.Count;
    }

    private void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
        // Алгоритм Фишера-Йетса для случайной перестановки точек
        for (int i = locations.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = locations[i];
            locations[i] = locations[randomIndex];
            locations[randomIndex] = temp;
            // дополнительная проверка
            if (Vector3.Distance(temp.position, locations[i].position) < 1f)
            {
                locations.Remove(locations[i]);
                i--;
            }
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerExitedTrigger = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerExitedTrigger = true;
            StopAllCoroutines();
            isWaiting = false;
        }
    }
   
    private Transform patrolRoute;
    public List<Transform> locations;

    private int locationIndex = 0;
    private NavMeshAgent agent;
    private bool playerExitedTrigger = false;
    private Animator animator;
    public float minWaitTime = 3f;
    public float maxWaitTime = 10f;
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolRoute = GameObject.Find("Locations").transform;
        animator = GetComponent<Animator>();
        InitializePatrolRoute();
    }
    void Update()
    {
        if (agent.isOnNavMesh && agent.remainingDistance < 2f && !agent.pathPending)
        {

            StartCoroutine(WaitAndMove());
            //MoveToNextPatrolLocation();
        }
        if (agent.speed > 1f) 
        {
            animator.SetTrigger("Go");
        }
        else if (agent.speed < 1) 
        {
            animator.SetTrigger("Idle");
        }
    }
    public IEnumerator WaitAndMove()
    {
        animator.SetTrigger("Idle");
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        MoveToNextPatrolLocation();
    }

    void MoveToNextPatrolLocation()
    {
        if (locations.Count == 0)
            return;

        agent.destination = locations[locationIndex].position;// Устанавливаем пункт назначения для агента
        locationIndex = (locationIndex + 1) % locations.Count;
    }
    void InitializePatrolRoute()
    {
        foreach (Transform child in patrolRoute)
        {
            locations.Add(child);
        }
        // Алгоритм Фишера-Йетса для случайной перестановки точек
        for (int i = locations.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            Transform temp = locations[i];
            locations[i] = locations[randomIndex];
            locations[randomIndex] = temp;
            //дополнительная проверка
            if (Vector3.Distance(temp.position, locations[i].position) < 1f)
            {
                locations.Remove(locations[i]);
                i--;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerExitedTrigger = false;
        }
    }
    void OnTriggerExit(Collider other)

    {
        if (other.gameObject.tag == "Player")
        {
            playerExitedTrigger = true;
            StopAllCoroutines();
        }
    }*/
}

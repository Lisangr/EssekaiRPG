using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyDetector : MonoBehaviour
{
    public float viewAngle = 90f;
    public float viewDistance = 10f;
    public int rayCount = 50;
    public float rayHeightOffset = 0.5f;
    public List<string> enemyTags = new List<string> { "Enemy" };
    public GameObject[] trees;
    public float chopDistance = 4f;
    public float attackDistance = 1f;

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    private bool playerIsOurTarget;
    private GameObject currentTree;
    private EnemyMainInfo enemyMainInfo;

    public AttackDirectionIndicator attackAreaIndicator; // —сылка на индикатор области атаки

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyMainInfo = GetComponent<EnemyMainInfo>();
        animator = GetComponent<Animator>();
        playerIsOurTarget = false;
    }

    private void Update()
    {
        FindVisibleEnemies();

        if (playerIsOurTarget)
        {
            navMeshAgent.SetDestination(playerTransform.position);

            if (Vector3.Distance(transform.position, playerTransform.position) <= attackDistance)
            {
                navMeshAgent.isStopped = true;
                animator.SetTrigger("Attack");
                enemyMainInfo.OnAttak();
                ShowAttackIndicator();
            }
            else
            {
                navMeshAgent.isStopped = false;
                HideAttackIndicator();
            }
        }
        else
        {
            FindAndChopTree();
            HideAttackIndicator();
        }

        if (navMeshAgent.velocity.magnitude > 0.1f)
        {
            animator.SetTrigger("Walk");
        }
        else
        {
            animator.SetTrigger("Idle");
        }

        if (currentTree != null && Vector3.Distance(transform.position,
            currentTree.transform.position) <= chopDistance)
        {
            animator.SetTrigger("Attack");
            enemyMainInfo.OnAttak();
            ShowAttackIndicator();
        }

        if (attackAreaIndicator != null)
        {
            attackAreaIndicator.UpdateAttackIndicator();
        }
    }

    void FindVisibleEnemies()
    {
        bool enemyFound = false;
        float angleBetweenRays = viewAngle / (rayCount - 1);

        for (int i = 0; i < rayCount; i++)
        {
            float angle = transform.eulerAngles.y - viewAngle / 2 + angleBetweenRays * i;
            Vector3 direction = DirectionFromAngle(angle);
            Vector3 rayOrigin = transform.position + Vector3.up * rayHeightOffset;

            if (Physics.Raycast(rayOrigin, direction, out RaycastHit hit, viewDistance))
            {
                if (hit.transform != null && CheckEnemyTag(hit.transform))
                {
                    OnEnemyHit(hit.transform);
                    enemyFound = true;
                }
            }
        }

        if (!enemyFound)
        {
            playerIsOurTarget = false;
        }
    }

    Vector3 DirectionFromAngle(float angleInDegrees)
    {
        angleInDegrees *= Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(angleInDegrees), 0, Mathf.Cos(angleInDegrees));
    }

    bool CheckEnemyTag(Transform target)
    {
        if (string.IsNullOrEmpty(target.tag))
            return false;

        if (enemyTags.Contains(target.tag))
            return true;

        return false;
    }

    public void OnEnemyHit(Transform enemy)
    {
        if (navMeshAgent != null && playerTransform != null)
        {
            playerIsOurTarget = true;
            currentTree = null;
        }
        else
        {
            playerIsOurTarget = false;
        }
    }

    void FindAndChopTree()
    {
        if (trees.Length == 0) return;

        GameObject closestTree = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject tree in trees)
        {
            float distance = Vector3.Distance(transform.position, tree.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTree = tree;
            }
        }

        if (closestTree != null)
        {
            currentTree = closestTree;
            navMeshAgent.SetDestination(closestTree.transform.position);
        }
    }

    void ShowAttackIndicator()
    {
        if (attackAreaIndicator != null)
        {
            attackAreaIndicator.ShowIndicator();
        }
    }

    void HideAttackIndicator()
    {
        if (attackAreaIndicator != null)
        {
            attackAreaIndicator.HideIndicator();
        }
    }
}
using System.Collections;
using UnityEngine;

public class FinderActivatorForCutScene1 : MonoBehaviour
{
    public Transform destinationPoint;
    public Transform lookingPoint;
    public float speed = 2f;
    public float offset = 0.5f;
    private Animator animator;
    private CapsuleCollider capsuleCollider;
    private bool isMoving = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Walk");
        
        // Получите ссылку на капсул коллайдер
        capsuleCollider = GetComponent<CapsuleCollider>();
        // Отключите коллайдер по умолчанию
        capsuleCollider.enabled = false;
        StartCoroutine(Die());
        
    }

    private void Update()
    {
        if (!isMoving)
        {
            isMoving = true;

            transform.LookAt(lookingPoint);
            StartCoroutine(MoveToDestination());            
        }
    }

    private IEnumerator MoveToDestination()
    {
        while (Vector3.Distance(transform.position, destinationPoint.position) > offset)
        {
            transform.position = Vector3.MoveTowards(transform.position, destinationPoint.position, speed * Time.deltaTime);
            yield return null;
        }
        transform.LookAt(lookingPoint);
        animator.SetTrigger("Summon");
        // Включите коллайдер после достижения точки
        capsuleCollider.enabled = true;
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(32f);
        animator.SetTrigger("Die");
        StopCoroutine(MoveToDestination());
    }
}

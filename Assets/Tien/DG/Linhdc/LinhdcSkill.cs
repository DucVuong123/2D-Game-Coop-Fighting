using System.Collections;
using UnityEngine;

public class LinhdcSkill : MonoBehaviour
{
     public Transform pointA;
    public Transform pointB;
    public float moveSpeed = 3f;

    private Transform user;
    private Vector3 targetPoint;
    private bool movingToB = true;
    private bool isRunning = false;
    private Coroutine patrolCoroutine;

    public void UseSkill(Transform userTransform, Transform target)
    {
        if (userTransform == null || pointA == null || pointB == null)
            return;

        user = userTransform;

        if (!isRunning)
        {
            targetPoint = pointB.position;
            isRunning = true;
            MonoBehaviour mb = user.GetComponent<MonoBehaviour>();
            patrolCoroutine = mb.StartCoroutine(PatrolRoutine());
        }
    }

    public void StopSkill()
    {
        if (!isRunning || user == null) return;
        MonoBehaviour mb = user.GetComponent<MonoBehaviour>();
        if (patrolCoroutine != null)
            mb.StopCoroutine(patrolCoroutine);
        isRunning = false;
    }

    private IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (user == null) yield break;

            user.position = Vector3.MoveTowards(user.position, targetPoint, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(user.position, targetPoint) < 0.1f)
            {
                movingToB = !movingToB;
                targetPoint = movingToB ? pointB.position : pointA.position;

                // Flip hướng patrol
                Vector3 scale = user.localScale;
                scale.x = Mathf.Sign(scale.x) * (movingToB ? 5f : -5f);
                user.localScale = scale;
            }

            yield return null;
        }
    }
}

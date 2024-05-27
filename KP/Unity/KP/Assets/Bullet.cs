using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public LayerMask layerMask;

    RaycastHit hit(Vector3 lastPos, Vector3 newPos)
    {
        RaycastHit hitted;
        Physics.Linecast(lastPos, newPos, out hitted, layerMask);
        return hitted;
    }

    public IEnumerator shoot(Vector3 startPos, Vector3 endPos, float speed)
    {
        float distance = Vector3.Distance(startPos, endPos);
        float time = distance / speed;
        float startTime = Time.time;
        float startWeight = 0f;

        while (Time.time - startTime < time)
        {
            Vector3 lastFramePosition = transform.position;
            startWeight += Time.deltaTime / time;
            transform.position = Vector3.Lerp(startPos, endPos, startWeight);
            RaycastHit newHit = hit(lastFramePosition, transform.position);
            if (newHit.collider != null)
            {
                IDamagable dmgObject = newHit.collider.GetComponent<IDamagable>();
                if (dmgObject != null)
                {
                    dmgObject.TakeDamage(10f, newHit.point, newHit.normal);
                    StopAllCoroutines();
                    Destroy(gameObject, 0.25f);
                }
            }
            yield return null;
        }
        StopAllCoroutines();
        Destroy(gameObject, 0.25f);
    }
}

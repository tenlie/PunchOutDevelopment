using System.Collections;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Rigidbody2D rb2D { get; set; }
    public Vector3 prevPos;

    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        prevPos = transform.position;
        StartCoroutine("RotateObserver");
    }

    IEnumerator RotateObserver()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.01f);
            Vector3 direction = transform.position - prevPos;
            direction.Normalize();
            float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0f, 0f, rotation);
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            prevPos = transform.position;
        }
    }

    /*
    void OnDrawGizmos()
    {
        if (path.Length > 0)
        {
            for (int i = 0; i < path.Length; i++)
            {
                if (path[i] != null)
                {
                    Gizmos.DrawSphere(path[i].position, reachDist);
                }
            }
        }
    }
    */
}

using UnityEngine;

public class PathFollower : MonoBehaviour
{
    public Rigidbody2D rb2D { get; set; }
    
    public Transform[] path;
    public float speed = 5.0f;
    public float reachDist;
    public int currentPoint = 0;

    // Use this for initialization
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //  Vector3 direction = path[currentPoint].position - transform.position; 
        float distance = Vector3.Distance(path[currentPoint].position, transform.position);
        Vector3 newPosition = Vector3.MoveTowards(transform.position, path[currentPoint].position, Time.smoothDeltaTime * speed);
        rb2D.MovePosition(newPosition);

        Vector3 direction = path[currentPoint].position - transform.position;
        direction.Normalize();
        float rotation = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotation);

        if (distance <= reachDist)
            currentPoint++;

        if (currentPoint >= path.Length)
            currentPoint = 0;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementBehavior : MonoBehaviour
{
    private GameObject MyTarget = null;
    public float MySpeed = 20f;
    public float turnRate = 0.1f;
    public float distanceToShoot = 5f;
    public float distanceToStop = 3f;
    public float rotateSpeedRange = 0f;
    public bool rotateActive;
    private float rotateSpeed;
    // Start is called before the first frame update
    void Start()
    {
        MyTarget = GameObject.FindGameObjectWithTag("Player");
        rotateSpeed = Random.Range(-rotateSpeedRange, rotateSpeedRange);
    }

    // Update is called once per frame
    void Update()
    {
        PointAtPosition(MyTarget.transform.position, turnRate * Time.smoothDeltaTime);
        if (Vector2.Distance(MyTarget.transform.position, transform.position) >= distanceToStop)
        {
            transform.position += MySpeed * Time.smoothDeltaTime * transform.up;
            if (rotateActive)
            {
                transform.position += rotateSpeed * Time.smoothDeltaTime * transform.right;
            }
        }
        if (Vector2.Distance(MyTarget.transform.position, transform.position) <= distanceToStop)
        {
            transform.position -= MySpeed * Time.smoothDeltaTime * transform.up;
        }
        
    }

    

    private void PointAtPosition(Vector3 p, float r)
    {
        Vector3 v = p - transform.position;
        transform.up = Vector3.LerpUnclamped(transform.up, v, r);
    }

    private void OnTriggerEnter2D(Collider2D collider)	
	{
        if (collider.gameObject.name.Equals("Circle")) 
        {
            Destroy(gameObject);
        }
    }
}

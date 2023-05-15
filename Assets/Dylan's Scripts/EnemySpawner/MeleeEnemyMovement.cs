using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemyMovement : MonoBehaviour
{
    private GameObject MyTarget = null;
    public float MySpeed = 2f;
    public float turnRate = 0.5f;
    public float attackTurnRate = 0.1f;
    public float distanceToAttack = 10f;
    public float attackSpeed = 10f;
    public float attackStagger = 0.5f;
    private float nextAttack = 0f;
    private bool attacking = false;
    // Start is called before the first frame update
    void Start()
    {
        MyTarget = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (!attacking)
        {
            if (Vector2.Distance(MyTarget.transform.position, transform.position) >= distanceToAttack)
            {
                PointAtPosition(MyTarget.transform.position, turnRate * Time.smoothDeltaTime);
                transform.position += MySpeed * Time.smoothDeltaTime * transform.up;
            
            }
            if (Vector2.Distance(MyTarget.transform.position, transform.position) <= distanceToAttack)
            {
                attacking = true;
                nextAttack = Time.time + attackStagger;
                PointAtPosition(MyTarget.transform.position, 0.1f * Time.smoothDeltaTime);
                transform.position += attackSpeed * Time.smoothDeltaTime * transform.up;
            }
        }
        else
        {
            if (Time.time > nextAttack)
            {
                PointAtPosition(MyTarget.transform.position, 0.1f * Time.smoothDeltaTime);
                transform.position += attackSpeed * Time.smoothDeltaTime * transform.up;
                if (Vector2.Distance(MyTarget.transform.position, transform.position) >= distanceToAttack) 
                {
                    attacking = false;
                }
            }
            else
            {
                PointAtPosition(MyTarget.transform.position, turnRate * Time.smoothDeltaTime);
            }
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

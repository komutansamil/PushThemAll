using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllor : MonoBehaviour
{
    #region Values
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform target;
    [SerializeField] private Manager manager;
    bool isPlayerNearTheEnemy = false;
    Rigidbody rb;
    float speed = 5f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        target = GameObject.Find("Player").transform;
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerNearTheEnemy)
        {
            SetDestination();
        }
    }

    void SetDestination()
    {
        agent.SetDestination(target.position);
        //transform.localPosition += transform.forward * 2f * Time.deltaTime;
        //rb.AddForce(Vector3.forward * Time.deltaTime * speed);

        //Vector3 distance = target.transform.position - transform.position;
        //transform.rotation = Quaternion.LookRotation(distance);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            isPlayerNearTheEnemy = true;
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "EliminaterWall")
        {
            manager.EarnedPoint += 100;
        }
        if (col.gameObject.tag == "Point")
        {
            manager.EarnedPoint += 100;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Manager manager;
    bool isTriggered = false;
    // Update is called once per frame

    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 100f);
    }

    void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.tag == "Player")
        {
            if(!isTriggered)
            {
                manager.EarnedPoint += 30;
                Destroy(this.gameObject);
                isTriggered = true;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{
    [SerializeField] private Manager manager;
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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            manager.EarnedPoint += 30;
            Destroy(this.gameObject);
        }
    }
}

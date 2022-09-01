using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectorWall : MonoBehaviour
{
    [SerializeField] private Manager manager;
    bool isTriggered = false;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!isTriggered)
            {
                manager.IslevelCompleted = true;
                manager.WriteTexts();
                Destroy(col.gameObject);
                isTriggered = true;
            }
        }
    }
}

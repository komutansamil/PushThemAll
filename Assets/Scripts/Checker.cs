using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checker : MonoBehaviour
{
    [SerializeField] private Manager manager;
    bool isTouchedTheEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("Manager").GetComponent<Manager>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (!isTouchedTheEnemy)
        {
            if (col.gameObject.tag == "Enemy")
            {
                Destroy(col.gameObject);
                manager.EarnedPoint += 30;
                manager.WriteTexts();
                isTouchedTheEnemy = true;
            }
        }

    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            isTouchedTheEnemy = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    float _forceValue = 10f;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null)
            {
                Vector3 pos = new Vector3(0, 0, -5);
                hit.collider.gameObject.transform.position = Vector3.MoveTowards(hit.collider.gameObject.transform.position,
                     hit.collider.gameObject.transform.position - pos, 2f);
            }
        }
    }
}

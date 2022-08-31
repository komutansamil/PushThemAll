using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    float _forceValue = 1f;
    // Start is called before the first frame update

    private void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Enemy")
        {
            Rigidbody rb = hit.collider.attachedRigidbody;
            if (rb != null)
            {
                Vector3 forceDirection = hit.collider.gameObject.transform.position - transform.position;
                forceDirection.y = 0f;
                forceDirection.Normalize();

                rb.AddForceAtPosition(forceDirection * _forceValue, transform.position, ForceMode.Impulse);
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllor : MonoBehaviour
{
    #region Values
    float _movementSpeed = 0.2f;
    float _rotationSpeed = 7f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Manager manager;
    [SerializeField] private Animator animator;
    Vector3 distance;
    Vector3 lastPos;
    float X;
    float Y;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        cameraPosition = GameObject.Find("MainCamera").transform;
        distance = cameraPosition.position - transform.position;
        lastPos = transform.position;
        cameraPosition.transform.SetParent(null);
    }

    // Update is called once per frame
    void Update()
    {
        if(manager.IsGameStarted)
        {
            PlayerMovementAndRotation();
            PlayerPushAction();
        }
    }

    void LateUpdate()
    {
        SetCameraPositionOnLate();
    }

    void PlayerMovementAndRotation()
    {
        X += Input.GetAxisRaw("Mouse X");
        Y += Input.GetAxisRaw("Mouse Y");

        if (Input.GetMouseButton(0))
        {
            if (Y > 0 || Y < 0 || X > 0 || X < 0)
            {
                Vector3 direction = new Vector3(X * Time.deltaTime * _movementSpeed,
                    0f, Y * Time.deltaTime * _movementSpeed);
                transform.position += direction;

                Quaternion dir = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, dir, _rotationSpeed * Time.deltaTime);

                lastPos = transform.position;
            }
            else
            {
                transform.position = lastPos;
            }
        }
    }

    void PlayerPushAction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("isIdle", false);
            StartCoroutine(CancelThePushAnimEvent());
        }
    }

    void SetCameraPositionOnLate()
    {
        cameraPosition.position = transform.position + distance;
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "EliminaterWall")
        {
            manager.IsLevelOver = true;
        }
        if (col.gameObject.tag == "Point")
        {
            manager.EarnedPoint += 100;
            Destroy(col.gameObject);
        }
        if (col.gameObject.tag == "Enemy")
        {
            Vector3 pos = new Vector3(0, 0, -5);
            transform.position = Vector3.MoveTowards(transform.position,
                 transform.position + pos, 2f);
            rb.isKinematic = false;
            StartCoroutine(CancelTrigger());
        }
    }

    IEnumerator CancelThePushAnimEvent()
    {
        yield return new WaitForSeconds(0.667f);
        animator.SetBool("isIdle", true);
    }

    IEnumerator CancelTrigger()
    {
        yield return new WaitForSeconds(1.5f);
        if(transform.position.y >= 8)
        {
            rb.isKinematic = true;
        }
        if (transform.position.y < 8)
        {
            manager.IsLevelOver = true;
        }
    }
}

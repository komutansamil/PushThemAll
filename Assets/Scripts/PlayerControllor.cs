using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllor : MonoBehaviour
{
    #region Values
    float _movementSpeed = 7f;
    float _rotationSpeed = 7f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cameraPosition;
    [SerializeField] private Manager manager;
    [SerializeField] private Animator animator;
    Vector3 distance;
    Vector3 lastPos;
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
        PlayerMovementAndRotation();
        PlayerPushAction();
    }

    void LateUpdate()
    {
        SetCameraPositionOnLate();
    }

    void PlayerMovementAndRotation()
    {
        float Z = Input.GetAxisRaw("Vertical");
        float X = Input.GetAxisRaw("Horizontal");

        if (X > 0 || X < 0 || Z > 0 || Z < 0)
        {
            Vector3 direction = new Vector3(X * Time.deltaTime * _movementSpeed,
                0f, Z * Time.deltaTime * _movementSpeed);
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
            //rb.AddForce(-transform.forward * 3f, ForceMode.Impulse);
        }
    }

    IEnumerator CancelThePushAnimEvent()
    {
        yield return new WaitForSeconds(0.667f);
        animator.SetBool("isIdle", true);
    }
}

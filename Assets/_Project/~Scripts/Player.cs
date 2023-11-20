using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    Rigidbody rb;
    Vector3 movDir;
    Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        movDir = cam.transform.forward * vertical + cam.transform.right * horizontal;
        movDir.y = 0;
        movDir.Normalize();
    }

    void FixedUpdate()
    {
        if (movDir.sqrMagnitude > 0)
        {
            rb.velocity = movDir * speed * Time.fixedDeltaTime;
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
}

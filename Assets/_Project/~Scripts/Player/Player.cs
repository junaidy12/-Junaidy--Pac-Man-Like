using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action OnPowerStarted;
    public event Action OnPowerStoped;

    [SerializeField] float speed;
    [SerializeField] float powerUpDuration = 5f;
    [SerializeField] int health;
    [SerializeField] Transform respawnPoint;

    [SerializeField] TMP_Text healthText;

    Rigidbody rb;
    Camera cam;
    
    float horizontal;
    float vertical;
    Vector3 movDir;
    bool isPowerUpActive;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //input vertical
        vertical = Input.GetAxis("Vertical");
        //input horizontal
        horizontal = Input.GetAxis("Horizontal");

        //cam  input vertical
        Vector3 camVertical = cam.transform.forward * vertical;
        //set the Y axis value to 0
        camVertical.y = 0;
        //cam input horizontal
        Vector3 camHorizontal = cam.transform.right * horizontal;
        //set the Y axis value to 0
        camHorizontal.y = 0;

        //set MovementDirection variable
        movDir = camVertical + camHorizontal;
    }

    void FixedUpdate()
    {
        //if there is input from player
        if (movDir.sqrMagnitude > 0)
        {
            //move the player GameObject
            rb.velocity = speed * Time.fixedDeltaTime * movDir;
        }
        else
        {
            //stop the player GameObject
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isPowerUpActive) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().Dead();
        }
    }

    public IEnumerator StartPowerUp()
    {
        OnPowerStarted?.Invoke();
        isPowerUpActive = true;
        yield return new WaitForSeconds(powerUpDuration);
        OnPowerStoped?.Invoke();
        isPowerUpActive = false;
    }

    void UpdateUI()
    {
        healthText.text = $"Health : {health}";
    }

    public void Dead()
    {
        health--;
        if(health <= 0)
        {
            Debug.Log("Lose");
        }
        else
        {
            transform.position = respawnPoint.position;
        }
        UpdateUI();
    }
}

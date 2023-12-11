using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public event Action OnPowerStarted;
    public event Action OnPowerStoped;

    [SerializeField] float speed;
    [SerializeField] float powerUpDuration = 5f;
    [SerializeField][Range(0,1)] float powerUpSpeedMultiplier = .5f;
    [SerializeField] int health;
    [SerializeField] Transform respawnPoint;

    [SerializeField] TMP_Text healthText;

    Rigidbody rb;
    Camera cam;

    float baseSpeed;
    float horizontal;
    float vertical;
    Vector3 movDir;
    bool isPowerUpActive;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;

        baseSpeed = speed;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //input vertical
        vertical = Input.GetAxis("Vertical");
        //input horizontal
        horizontal = Input.GetAxis("Horizontal");

        //normalize input vector
        Vector3 inputNormalize = new Vector3(horizontal, 0, vertical).normalized;

        //cam  input vertical
        Vector3 camVertical = cam.transform.forward * inputNormalize.z;
        //set the Y axis value to 0
        camVertical.y = rb.velocity.y;
        //cam input horizontal
        Vector3 camHorizontal = cam.transform.right * inputNormalize.x;
        //set the Y axis value to 0
        camHorizontal.y = rb.velocity.y;

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
        speed = speed + (speed * powerUpSpeedMultiplier);
        isPowerUpActive = true;
        yield return new WaitForSeconds(powerUpDuration);
        OnPowerStoped?.Invoke();
        speed = baseSpeed;
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
            SceneManager.LoadScene("LoseScene");
        }
        else
        {
            transform.position = respawnPoint.position;
        }
        UpdateUI();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject gameoverText;
    public float speed;

    private GameManager gameManager;
    private Rigidbody playerRb;
    private Vector3 spawnPos;
    private float horizontal;
    private float xLimitPos = 15;
    private float zOffset = 0.8f;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        Fire();
        Health();
        OutOfBounds();
    }

    void Movement()
    {
        horizontal = Input.GetAxis("Horizontal");

        playerRb.AddForce(Vector3.right * horizontal * speed);
    }

    void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spawnPos = new Vector3(transform.position.x, transform.position.y, transform.position.z + zOffset);
            Instantiate(bulletPrefab, spawnPos, bulletPrefab.transform.rotation);
        }
    }

    void Health()
    {
        if (gameManager.playerLife <= 0)
        {
            Time.timeScale = 0;
            gameManager.isGameover = true;
            gameoverText.SetActive(true);
            Destroy(gameObject);
        }
    }

    void OutOfBounds()
    {
        if (transform.position.x < -xLimitPos)
        {
            transform.position = new Vector3(-xLimitPos, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xLimitPos)
        {
            transform.position = new Vector3(xLimitPos, transform.position.y, transform.position.z);
        }
    }
}

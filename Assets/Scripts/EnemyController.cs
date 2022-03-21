using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject exp;
    private Vector3 currentPos;
    private float speed = 3;
    private float xLimit = 14;
    private int health;

    void Start()
    {
        exp = GameObject.FindGameObjectWithTag("Explosion");
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        health = gameManager.enemylife;
    }

    void Update()
    {
        Bounds();
    }

    void LateUpdate()
    {
        Movement();
    }

    void Movement()
    {
        if (gameManager.isLeft == true)
        {
            transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
    }

    void Bounds()
    {
        if (transform.position.x < -xLimit)
        {
            gameManager.isLeft = false;
        }
        else if (transform.position.x > xLimit)
        {
            gameManager.isLeft = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            health--;
            if (health <= 0)
            {
                currentPos = transform.position;
                exp.transform.position = currentPos;
                exp.GetComponent<ParticleSystem>().Play();
                gameManager.score += 25;
                Destroy(gameObject);
            }
            Destroy(collision.gameObject);
        }
    }
}

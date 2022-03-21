using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    private GameManager gameManager;
    private float speed = 5;
    private float zLimit = 10;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        transform.Translate(Vector3.back * Time.deltaTime * speed);

        OutOfBounds();
    }

    void OutOfBounds()
    {
        if (transform.position.x < -zLimit)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            gameManager.playerLife--;
            Destroy(gameObject);
        }
    }
}

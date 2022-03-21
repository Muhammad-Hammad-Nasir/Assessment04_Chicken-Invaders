using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private float speed = 30;
    private float zLimit = 11;

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);

        OutOfBounds();
    }

    void OutOfBounds()
    {
        if (transform.position.z > zLimit)
        {
            Destroy(gameObject);
        }
    }
}

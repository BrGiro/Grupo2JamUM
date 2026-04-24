using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 5f;
    private float despawnX;

    public void Init(float despawnX)
    {
        this.despawnX = despawnX;
    }

    void Update()
    {

        transform.Translate(Vector2.left * speed * Time.deltaTime);
        if (transform.position.x < despawnX)
            Destroy(gameObject);
    }
}

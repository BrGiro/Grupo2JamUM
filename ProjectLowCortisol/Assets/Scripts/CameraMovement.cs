using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private personajePath3 personajePath3;
    Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        rb.velocity = personajePath3.GetCurrentVelocity();
        transform.position = new Vector3(personajePath3.transform.position.x, transform.position.y, transform.position.z);
    }
}

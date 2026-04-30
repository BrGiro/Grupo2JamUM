using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class personajePath3 : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public int pathNum = 0;     // Empieza en el carril de más abajo
    int maxPaths = 1;           // 1 carril es 0, 2 carriles es 1, etc.
    public float speed;

    // Necesitamos que las teclas estén en variables para invertirlas
    KeyCode Down;
    KeyCode Up;

    void Start()
    {
        rb.velocity = new Vector2(speed, 0);

        Down = KeyCode.S;
        Up = KeyCode.W;
    }

    void Update()
    {
        ControlProcess(Down, Up);
    }

    void ControlProcess(KeyCode key1, KeyCode key2)
    {
        // Esta función contiene toda la información de los controles para poder invertirlos 
        
        if (Input.GetKeyDown(key1) && pathNum > 0)
        {
            float newpos = rb.position.y - 2f;
            rb.position = new Vector2(rb.position.x, newpos);
            pathNum--;
        }

        if (Input.GetKeyDown(key2) && pathNum < maxPaths) // Podemos modificar el maxPaths si llegan a haber más carriles
        {
            float newpos = rb.position.y + 2f;
            rb.position = new Vector2(rb.position.x, newpos);
            pathNum++;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)     // Nuestro hermoso detector de colisiones
    {
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Triggered");
            collision.enabled = false;          // Esto permite que el jugador no se pueda volver a chocar con el mismo obstaculo cambiando constantemente de carril
            StartCoroutine(CrashResult(1.5f));  // Subrutina para que el auto pierda velocidad momentaneamente al chocar
            
            // Acá se invierten los controles
            KeyCode BackupKey = Up;
            Up = Down;
            Down = BackupKey;

        }
    }

    IEnumerator CrashResult(float time)
    {
        rb.velocity = new Vector2(speed / 3, 0);
        yield return new WaitForSeconds(time);
        rb.velocity = new Vector2(speed, 0);
    }

}

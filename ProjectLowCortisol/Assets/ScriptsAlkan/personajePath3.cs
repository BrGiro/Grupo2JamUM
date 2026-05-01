using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

public class personajePath3 : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public int pathNum = 0;     // Empieza en el carril de más abajo
    int maxPaths = 1;           // 1 carril es 0, 2 carriles es 1, etc.
    public float speed;
    private Quaternion targetRotation;
    [SerializeField] UIManager uiManager;

    [SerializeField] int scoreIncial;
    [SerializeField] int currrentScore;

    //Referencias a VFX
    [SerializeField] ParticleSystem VFX_Smoke;
    [SerializeField] ParticleSystem VFX_Explotion;
    [SerializeField] AudioSource SFX_Explotion;
    [SerializeField] AudioSource SFX_Poof;
    [SerializeField] ParticleSystem VFX_CursedText;
    [SerializeField] ParticleSystem VFX_BoomText;

    // Necesitamos que las teclas estén en variables para invertirlas
    KeyCode Down;
    KeyCode Up;

    void Start()
    {
        rb.velocity = new Vector2(speed, 0);

        Down = KeyCode.S;
        Up = KeyCode.W;

        currrentScore = scoreIncial;

        uiManager.SetScoreDisplay(scoreIncial.ToString());


        targetRotation = transform.rotation;
    }

    void Update()
    {
        ControlProcess(Down, Up);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 20 * Time.deltaTime);

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
        Debug.Log("Triggered");
        if (collision.CompareTag("Obstacle"))
        {
            Debug.Log("Triggered");
            collision.enabled = false;          // Esto permite que el jugador no se pueda volver a chocar con el mismo obstaculo cambiando constantemente de carril
            targetRotation = DoABarrelRoll();
            AquiExplotanCosas();
            //StartCoroutine(CrashResult(0.5f));  // Subrutina para que el auto pierda velocidad momentaneamente al chocar
            
            // Acá se invierten los controles
            KeyCode BackupKey = Up;
            Up = Down;
            Down = BackupKey;

        }
        if (collision.CompareTag("Velocidad"))
        {
            rb.velocity *= 1.75f;
        }
        if (collision.CompareTag("Velocidad2"))
        {
            rb.velocity *= 2.25f;
        }
        if (collision.CompareTag("Meta"))
        {
            SceneManager.LoadScene("BadEnding");
        }
    }
    public Vector2 GetCurrentVelocity()
    {
        return rb.velocity;
    }
    private Quaternion DoABarrelRoll()
    {
        Quaternion targetRotation = transform.rotation;
        targetRotation *= Quaternion.Euler(0, 0, 180);

        Debug.Log("hice un barrel roll");

        return targetRotation;
        
    }
    private void AquiExplotanCosas()
    {
        VFX_Smoke.Play();
        VFX_Explotion.Play();
        SFX_Explotion.Play();
        SFX_Poof.Play();
        VFX_BoomText.Play();
        VFX_CursedText.Play();

        currrentScore -= 100;
        uiManager.SetScoreDisplay(currrentScore.ToString());
    }
    IEnumerator CrashResult(float time)
    {
        rb.velocity = new Vector2(speed / 3, 0);
        yield return new WaitForSeconds(time);
        rb.velocity = new Vector2(speed, 0);
    }

}

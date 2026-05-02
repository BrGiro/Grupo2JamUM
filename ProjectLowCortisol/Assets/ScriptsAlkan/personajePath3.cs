using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class personajePath3 : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public int pathNum = 0;     // Empieza en el carril de más abajo
    int maxPaths = 1;           // 1 carril es 0, 2 carriles es 1, etc.
    public float speed;
    private Quaternion targetRotation;
    [SerializeField] string goodEndingScene;
    [SerializeField] string badEndingScene;
    [SerializeField] Sprite damagedPlayer;
    [SerializeField] Sprite normalPlayer;
 
    [SerializeField] UIManager uiManager;
    [SerializeField] ScManager sceneManager;
    [SerializeField] MusicManager musicManager;

    [SerializeField] int scoreIncial;
    [SerializeField] int currentScore;
    [SerializeField] int currentChocadosObstacles;

    //Referencias a VFX
    [SerializeField] ParticleSystem VFX_Smoke;
    [SerializeField] ParticleSystem VFX_Explotion;
    [SerializeField] AudioSource SFX_Explotion;
    [SerializeField] AudioSource SFX_Poof;
    [SerializeField] AudioSource SFX_Moto;
    [SerializeField] ParticleSystem VFX_CursedText;
    [SerializeField] ParticleSystem VFX_BoomText;

    // Necesitamos que las teclas estén en variables para invertirlas
    KeyCode Down;
    KeyCode Up;

    void Start()
    {
        rb.velocity = new Vector2(speed, 0);
        musicManager = GameObject.FindGameObjectWithTag("MusicController").GetComponent<MusicManager>();

        Down = KeyCode.S;
        Up = KeyCode.W;
        SFX_Moto = GetComponent<AudioSource>();
        SFX_Moto.volume = musicManager.sfxVolumne;

        currentScore = scoreIncial;

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
            Debug.Log("hey me fui pa abajo");
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
            rb.velocity *= 2f;
        }
        if (collision.CompareTag("Meta"))
        {
            if (currentScore <= 0)
            {
                StartCoroutine(GameOver(true));
            }
            else
            {
                StartCoroutine(GameOver(false));
            }
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
        SFX_Explotion.volume = musicManager.sfxVolumne;
        SFX_Poof.Play();
        SFX_Poof.volume = musicManager.sfxVolumne;
        VFX_BoomText.Play();
        VFX_CursedText.Play();

        currentScore -= 100;
        currentChocadosObstacles++;
        if (currentScore < 0 && normalPlayer != damagedPlayer) gameObject.GetComponent<SpriteRenderer>().sprite = damagedPlayer;
        uiManager.SetScoreDisplay(currentScore.ToString());
    }
    IEnumerator GameOver(bool goodEnding)
    {
        Time.timeScale = 0f;
        uiManager.SetGameOverPanel(true);
        yield return new WaitForSecondsRealtime(1);
        uiManager.SetObstaclesDisplay(currentChocadosObstacles.ToString());
        yield return new WaitForSecondsRealtime(1);
        uiManager.SetGameOverScoreDisplay(currentScore.ToString());        
        yield return new WaitForSecondsRealtime(1);

        if (!goodEnding) 
        {
            yield return new WaitForSecondsRealtime(1);
            sceneManager.LoadNewScene(badEndingScene);
        }
        else
        {
            yield return new WaitForSecondsRealtime(1);
            sceneManager.LoadNewScene(goodEndingScene);
        }
        musicManager.StopMusicGame();
        StopAllCoroutines();
    }
    IEnumerator CrashResult(float time)
    {
        rb.velocity = new Vector2(speed / 3, 0);
        yield return new WaitForSeconds(time);
        rb.velocity = new Vector2(speed, 0);
    }

}

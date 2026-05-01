using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class OpeningMove : MonoBehaviour
{
    public Rigidbody2D rb;
    float movementSpeed = 2f;
    float duration = 5f;
    public float counter = 5;
    
    void Start()
    {
        StartCoroutine(MovementPause());
    }

    void Update()
    {
        if (counter == 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("TutorialScene");
        }
        
        if (duration > 0)
        {
            rb.velocity = new Vector2(0, movementSpeed);
            duration -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(MovementPause());
        }
    }

    IEnumerator MovementPause()
    {
        Time.timeScale = 0f;
        duration = 5f;
        yield return new WaitForSecondsRealtime(3f);
        counter -= 1;
        Time.timeScale = 1f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBack : MonoBehaviour
{
    [SerializeField] string mainMenuScene;

    public void VolverAlFuturo()
    {
        SceneManager.LoadScene(mainMenuScene);
    }
}

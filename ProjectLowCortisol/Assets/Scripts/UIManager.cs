using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] TextMeshProUGUI contadorDisplay;
    [SerializeField] GameObject gameOverPanel;

    [SerializeField]TextMeshProUGUI gameOverScore;
    [SerializeField]TextMeshProUGUI gameOverObstacles;
    public void SetScoreDisplay(string newScore)
    {
        scoreDisplay.text = newScore;
    }
    public void SetContadorDisplay(string newContador)
    {
        contadorDisplay.text = newContador;
    }
    public void SetGameOverPanel(bool active)
    {
        gameOverPanel.SetActive(active);
    }
    public void SetObstaclesDisplay(string newObstacles)
    {
        if (gameOverObstacles != null)
            gameOverObstacles.text += newObstacles;
    }
    public void SetGameOverScoreDisplay(string newScore)
    {
        gameOverScore.text += newScore;
    }
    

}

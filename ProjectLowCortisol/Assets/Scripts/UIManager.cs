using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay;
    [SerializeField] TextMeshProUGUI contadorDisplay;

    public void SetScoreDisplay(string newScore)
    {
        scoreDisplay.text = newScore;
    }
    public void SetContadorDisplay(string newContador)
    {
        contadorDisplay.text = newContador;
    }

}

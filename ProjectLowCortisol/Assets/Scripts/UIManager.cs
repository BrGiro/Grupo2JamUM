using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreDisplay;

    public void SetScoreDisplay(string newScore)
    {
        scoreDisplay.text = newScore;
    }

}

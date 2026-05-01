using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartCountdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText1;
    public TextMeshProUGUI countdownText2;
    public TextMeshProUGUI countdownText3;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountdownStart());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CountdownStart()
    {
        yield return new WaitForSecondsRealtime(1f);
        countdownText1.gameObject.SetActive(false);
        countdownText2.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        countdownText2.gameObject.SetActive(false);
        countdownText3.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
    }
}

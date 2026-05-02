using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BotonClick : MonoBehaviour
{
    public AudioSource bubbleClick;
   public void PlayBubbleClick()
   {
        bubbleClick.Play();
   }
}

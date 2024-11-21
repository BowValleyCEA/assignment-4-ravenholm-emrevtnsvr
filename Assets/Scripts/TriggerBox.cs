
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBox : MonoBehaviour
{
    public AudioSource playMusic;
     void OnTriggerEnter(Collider other)
     { 
      playMusic.Play(); 
     }
  
}

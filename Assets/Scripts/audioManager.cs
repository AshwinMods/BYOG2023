using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioManager : MonoBehaviour
{
  public void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
  
}

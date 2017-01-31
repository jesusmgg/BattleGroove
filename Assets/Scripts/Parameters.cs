using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parameters : MonoBehaviour
{
    public bool AIPlayer;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController gameController;

    private void Awake()
    {
        if (gameController!=null)
        {
            Destroy(gameObject);
        }
        else
        {
            
        }
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
}

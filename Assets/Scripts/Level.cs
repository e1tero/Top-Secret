using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class Level : MonoBehaviour
{
    public float timer;
    public float gameTimer;
   
    private ILevel levelMission;
    private Coroutine gameCoroutine;
    private void Start()
    {
        levelMission = GetComponent<ILevel>();
        levelMission.InitMission();
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float projectileDuration;
    public float timer;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= projectileDuration)
        {
            Destroy(gameObject);
        }
    }
}

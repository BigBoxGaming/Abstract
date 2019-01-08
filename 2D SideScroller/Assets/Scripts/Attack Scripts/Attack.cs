using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{

    public float damage;
    public float coolDownTime;
    public string attackName;
    public string attackType;
    private float _timer;
    


    //cooldown time has been reached
    public bool readyForUse;
    

    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= coolDownTime)
        {
            readyForUse = true;
        }
        else
        {
            readyForUse = false;
        }
    }
    public float GetDamage()
    {
        return damage;
    }
}

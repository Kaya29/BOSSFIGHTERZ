using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NormalBoss : BossZ
{
    // Start is called before the first frame update
    void Start()
    {
        health = 500f;
        shield = 100;
        speed = 10.0f;
        maxLook = 100;
        minLook =50;
        minRockthrow = 30;
        maxattack = 10;
        maxtime = 60;
        StartCoroutine(rockthrow());
    }



    // Update is called once per frame
    void Update()
    {
        BossMov();
    }

    public override void BossMov()
    {
        base.BossMov();
    }

    IEnumerator rockthrow()
    {

        while (time <= maxtime)
        {
            yield return new WaitForSeconds(1);
            time++;
            Debug.Log(time);
            if (time == maxtime)
            {
                rockThrow = true;
                time = 0f;
            }
        }

    }
}

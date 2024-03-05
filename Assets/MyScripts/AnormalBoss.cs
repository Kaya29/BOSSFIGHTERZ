using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnormalBoss : BossZ
{
    // Start is called before the first frame update
    void Start()
    {
        health = 1500;
        shield = 150;
        speed = 20.0f;
        maxLook = 100;
        minLook = 55;
        minRockthrow = 35;
        maxattack = 90;
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

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma_script : MonoBehaviour
{
    public float speed;
    private float Dist;
    private Vector3 VectorSpeed;

    private GameObject Player;
    private GameController_script gameController_Script;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");
        gameController_Script = GameObject.Find("GameController").GetComponent<GameController_script>();

        VectorSpeed = gameObject.transform.position - Player.transform.position;
        Dist = Vector3.Distance(gameObject.transform.position, Player.transform.position);
        Dist = 85 / Dist;

        GetComponent<Rigidbody>().velocity = VectorSpeed * speed * Dist;

        Vector3 targetPoint = Player.transform.position;// Определение целевой ротации.
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20);// Поворот к целевой точке.
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController_Script.getIsStarted())
        {
            GetComponent<Rigidbody>().velocity = VectorSpeed * speed * Dist;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = VectorSpeed * 0;

            if (gameController_Script.GetIsGameEnd())
            {
                Destroy(gameObject);
            }
        }
    }
}

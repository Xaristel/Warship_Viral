﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketPlayer_script : MonoBehaviour
{
    public float speed;
    private float averageSpeed;
    private List<GameObject> EnemiesList = new List<GameObject>();
    private GameObject[] TempList;
    private GameObject Target = null;
    private float minDist;
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        EnemiesList.Clear();
        TempList = GameObject.FindGameObjectsWithTag("LightEnemy");
        if (TempList != null)
            foreach (var item in TempList)
            {
                EnemiesList.Add(item);
            }
        TempList = GameObject.FindGameObjectsWithTag("StandartEnemy");
        if (TempList != null)
            foreach (var item in TempList)
            {
                EnemiesList.Add(item);
            }

        TempList = GameObject.FindGameObjectsWithTag("HardEnemy");
        if (TempList != null)
            foreach (var item in TempList)
            {
                EnemiesList.Add(item);
            }

        TempList = GameObject.FindGameObjectsWithTag("InsectoidEnemy");
        if (TempList != null)
            foreach (var item in TempList)
            {
                EnemiesList.Add(item);
            }

        TempList = GameObject.FindGameObjectsWithTag("Boss");
        if (TempList != null)
            foreach (var item in TempList)
            {
                EnemiesList.Add(item);
            }

        TempList = GameObject.FindGameObjectsWithTag("Asteroid");
        if (TempList != null)
            foreach (var item in TempList)
            {
                EnemiesList.Add(item);
            }

        minDist = float.MaxValue;
        float tempDist;
        if (EnemiesList != null)
        {
            foreach (var item in EnemiesList)
            {
                if (item != null)
                {
                    tempDist = Vector3.Distance(gameObject.transform.position, item.transform.position);
                    if (tempDist < minDist)
                    {
                        minDist = tempDist;
                        Target = item;
                    }
                }

            }
            minDist = 85 / minDist;
        }
        else
        {
            GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 40;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Target != null)
        {
            Quaternion targetRotation = Quaternion.LookRotation(Target.transform.position - transform.position);
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20); // Поворот к целевой точке.
                                                                                                      // averageSpeed = 
            GetComponent<Rigidbody>().velocity = gameObject.transform.forward * speed; //* minDist;
        }
        else
        {
            Start();
            GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 40;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "PlayerShot" && other.tag != "EnemyShot" && other.tag != "Player" && other.tag != "GameBorder")
        {
            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
        }
    }
}
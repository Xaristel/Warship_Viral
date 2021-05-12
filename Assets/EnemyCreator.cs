using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    //public GameObject Enemy0;
    public int enemyType;

    protected GameController_script gameController_Script;
    // Start is called before the first frame update
    void Start()
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gameController_Script.getIsStarted())
        {
            return;
        }
        
        if (true) //спавн новых врагов
        {
            //Instantiate(кто, где, вращение);
        }
    }

    //GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemy1"); сбор всех объектов с тегом 
} 

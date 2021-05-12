using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator_script : MonoBehaviour
{
    public GameObject Boss1;
    public GameObject Boss2;
    public GameObject Boss3;
    public GameObject InsectoidBoss;

    public GameObject BombEnemy1;
    public GameObject LightEnemy1;
    public GameObject LightEnemy2;

    public GameObject StandartEnemy1;
    public GameObject StandartEnemy2;
    public GameObject StandartEnemy3;

    public GameObject HardEnemy1;
    public GameObject HardEnemy2;
    public GameObject HardEnemy3;

    protected GameController_script gameController_Script;

    public double NextWaveDelay; //3
    public double NextWaveTime = 0;
    private int spawnType;

    public int GetSpawnType()
    {
        return spawnType;
    }
    void Start()    // Start is called before the first frame update
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();
    }

    void Update()   // Update is called once per frame
    {
        if (!gameController_Script.getIsStarted())
        {
            return;
        }
        if (Time.time > NextWaveTime)
        {
            NextWaveTime = Time.time + NextWaveDelay; //Random.Range(1, 2); TODO Insectoid Level
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        spawnType = Random.Range(1, 9);

        switch (spawnType)
        {
            case 1: // <---->1
                {
                    var newPosition = new Vector3(-28, 0, 90);
                    Instantiate(StandartEnemy1, newPosition, Quaternion.identity);

                    newPosition = new Vector3(-12, 0, 90);
                    Instantiate(StandartEnemy1, newPosition, Quaternion.identity);

                    newPosition = new Vector3(12, 0, 90);
                    Instantiate(StandartEnemy1, newPosition, Quaternion.identity);

                    newPosition = new Vector3(28, 0, 90);
                    Instantiate(StandartEnemy1, newPosition, Quaternion.identity);
                    break;
                }
            case 2: // <---->2
                {
                    var newPosition = new Vector3(0, 0, 90);
                    Instantiate(StandartEnemy2, newPosition, Quaternion.identity);

                    newPosition = new Vector3(-20, 0, 90);
                    Instantiate(StandartEnemy2, newPosition, Quaternion.identity);

                    newPosition = new Vector3(20, 0, 90);
                    Instantiate(StandartEnemy2, newPosition, Quaternion.identity);
                    break;
                }
            case 3: //
                {
                    var newPosition = new Vector3(-28, 0, 90);
                    Instantiate(StandartEnemy3, newPosition, Quaternion.identity);

                    newPosition = new Vector3(-18, 0, 90);
                    Instantiate(StandartEnemy3, newPosition, Quaternion.identity);

                    newPosition = new Vector3(18, 0, 90);
                    Instantiate(StandartEnemy3, newPosition, Quaternion.identity);

                    newPosition = new Vector3(28, 0, 90);
                    Instantiate(StandartEnemy3, newPosition, Quaternion.identity);
                    break;
                }
            case 4: //
                {
                    var newPosition = new Vector3(-30, 0, 90);
                    Instantiate(LightEnemy1, newPosition, Quaternion.identity);

                    newPosition = new Vector3(-22, 0, 90);
                    Instantiate(LightEnemy1, newPosition, Quaternion.identity);

                    newPosition = new Vector3(0, 0, 90);
                    Instantiate(LightEnemy1, newPosition, Quaternion.identity);

                    newPosition = new Vector3(22, 0, 90);
                    Instantiate(LightEnemy1, newPosition, Quaternion.identity);

                    newPosition = new Vector3(30, 0, 90);
                    Instantiate(LightEnemy1, newPosition, Quaternion.identity);
                    break;
                }
            case 5: //
                {
                    var newPosition = new Vector3(-30, 0, 90);
                    Instantiate(LightEnemy2, newPosition, Quaternion.identity);

                    newPosition = new Vector3(-22, 0, 90);
                    Instantiate(LightEnemy2, newPosition, Quaternion.identity);

                    newPosition = new Vector3(-14, 0, 90);
                    Instantiate(LightEnemy2, newPosition, Quaternion.identity);

                    newPosition = new Vector3(14, 0, 90);
                    Instantiate(LightEnemy2, newPosition, Quaternion.identity);

                    newPosition = new Vector3(22, 0, 90);
                    Instantiate(LightEnemy2, newPosition, Quaternion.identity);

                    newPosition = new Vector3(30, 0, 90);
                    Instantiate(LightEnemy2, newPosition, Quaternion.identity);
                    break;
                }

            case 6: //
                {
                    var newPosition = new Vector3(-15, 0, 90);
                    Instantiate(HardEnemy1, newPosition, Quaternion.identity);

                    newPosition = new Vector3(15, 0, 90);
                    Instantiate(HardEnemy1, newPosition, Quaternion.identity);
                    break;
                }
            case 7: //
                {
                    var newPosition = new Vector3(-20, 0, 90);
                    Instantiate(HardEnemy2, newPosition, Quaternion.identity);

                    newPosition = new Vector3(0, 0, 90);
                    Instantiate(HardEnemy2, newPosition, Quaternion.identity);

                    newPosition = new Vector3(20, 0, 90);
                    Instantiate(HardEnemy2, newPosition, Quaternion.identity);
                    break;
                }

            case 8: //
                {
                    var newPosition = new Vector3(0, 0, 90);
                    Instantiate(HardEnemy3, newPosition, Quaternion.identity);
                    break;
                }
            default:
                {
                    break;
                }
        }
    }

    void MediumLevel()
    {

    }

    void HardLevel()
    {

    }
}

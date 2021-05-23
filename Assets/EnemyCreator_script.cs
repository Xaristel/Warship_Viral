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

    public GameObject HPKit;

    protected GameController_script gameController_Script;

    public double NextWaveDelay = 14; //14 - default
    public double NextWaveTime = 0;
    public double NextHPKitDelay = 60; //60 - default
    public double NextHPKitTime = 0;

    public int AddHP = 4;
    private int spawnType;

    public int GetSpawnType()
    {
        return spawnType;
    }
    void Start()    // Start is called before the first frame update
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();

        NextHPKitTime = Time.time + NextHPKitDelay;

        switch (gameController_Script.getMode())
        {
            case 1:
                {
                    AddHP = 4;
                    break;
                }
            case 2:
                {
                    AddHP = 6;
                    break;
                }
            case 3:
                {
                    AddHP = 8;
                    break;
                }
        }
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

            switch (gameController_Script.getMode())
            {
                case 1:
                    {
                        EasyLevel();
                        break;
                    }
                case 2:
                    {
                        MediumLevel();
                        break;
                    }
                case 3:
                    {
                        HardLevel();
                        break;
                    }
            }
        }

        if (Time.time > NextHPKitTime)
        {
            NextHPKitDelay = Random.Range(50, 90);
            NextHPKitTime = Time.time + NextHPKitDelay;
            SpawnHPKit();
        }
    }

    void EasyLevel() // 35% - 50% - 15%
    {
        int randomSpawnNumber = Random.Range(0, 101);

        if (randomSpawnNumber >= 0 && randomSpawnNumber < 12)
        {
            spawnType = 1; //standart
        }
        else if (randomSpawnNumber >= 12 && randomSpawnNumber < 24)
        {
            spawnType = 2; //standart
        }
        else if (randomSpawnNumber >= 24 && randomSpawnNumber < 36)
        {
            spawnType = 3; //standart
        }
        else if (randomSpawnNumber >= 36 && randomSpawnNumber < 62)
        {
            spawnType = 4; //easy
        }
        else if (randomSpawnNumber >= 62 && randomSpawnNumber < 88)
        {
            spawnType = 5; //easy
        }
        else if (randomSpawnNumber >= 88 && randomSpawnNumber < 93)
        {
            spawnType = 6; //hard
        }
        else if (randomSpawnNumber >= 93 && randomSpawnNumber < 97)
        {
            spawnType = 7; //hard
        }
        else if (randomSpawnNumber >= 97 && randomSpawnNumber < 101)
        {
            spawnType = 8; //hard
        }

        SpawnEnemy();
    }

    void MediumLevel() // 45% - 30% - 25%
    {
        int randomSpawnNumber = Random.Range(0, 101);

        if (randomSpawnNumber >= 0 && randomSpawnNumber < 15)
        {
            spawnType = 1; //standart
        }
        else if (randomSpawnNumber >= 15 && randomSpawnNumber < 30)
        {
            spawnType = 2; //standart
        }
        else if (randomSpawnNumber >= 30 && randomSpawnNumber < 45)
        {
            spawnType = 3; //standart
        }
        else if (randomSpawnNumber >= 45 && randomSpawnNumber < 60)
        {
            spawnType = 4; //easy
        }
        else if (randomSpawnNumber >= 60 && randomSpawnNumber < 75)
        {
            spawnType = 5; //easy
        }
        else if (randomSpawnNumber >= 75 && randomSpawnNumber < 83)
        {
            spawnType = 6; //hard
        }
        else if (randomSpawnNumber >= 83 && randomSpawnNumber < 91)
        {
            spawnType = 7; //hard
        }
        else if (randomSpawnNumber >= 91 && randomSpawnNumber < 101)
        {
            spawnType = 8; //hard
        }

        SpawnEnemy();
    }

    void HardLevel() // 45% - 15% - 40%
    {
        int randomSpawnNumber = Random.Range(0, 101);

        if (randomSpawnNumber >= 0 && randomSpawnNumber < 15)
        {
            spawnType = 1; //standart
        }
        else if (randomSpawnNumber >= 15 && randomSpawnNumber < 30)
        {
            spawnType = 2; //standart
        }
        else if (randomSpawnNumber >= 30 && randomSpawnNumber < 45)
        {
            spawnType = 3; //standart
        }
        else if (randomSpawnNumber >= 45 && randomSpawnNumber < 53)
        {
            spawnType = 4; //easy
        }
        else if (randomSpawnNumber >= 53 && randomSpawnNumber < 61)
        {
            spawnType = 5; //easy
        }
        else if (randomSpawnNumber >= 61 && randomSpawnNumber < 74)
        {
            spawnType = 6; //hard
        }
        else if (randomSpawnNumber >= 74 && randomSpawnNumber < 87)
        {
            spawnType = 7; //hard
        }
        else if (randomSpawnNumber >= 87 && randomSpawnNumber < 101)
        {
            spawnType = 8; //hard
        }

        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
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

    public void SpawnHPKit()
    {
        var newPosition = new Vector3(Random.Range(-35, 35), 0, 90);
        Instantiate(HPKit, newPosition, Quaternion.identity);
    }

    public void AddHPForEnemies()
    {
        StandartEnemy1.GetComponent<StandartEnemy1_script>().EnemyLife += AddHP;
        StandartEnemy2.GetComponent<StandartEnemy2_script>().EnemyLife += AddHP;
        StandartEnemy3.GetComponent<StandartEnemy3_script>().EnemyLife += AddHP;

        LightEnemy1.GetComponent<LightEnemy1_script>().EnemyLife += AddHP;
        LightEnemy2.GetComponent<LightEnemy2_script>().EnemyLife += AddHP;

        HardEnemy1.GetComponent<HardEnemy1_script>().EnemyLife += AddHP;
        HardEnemy2.GetComponent<HardEnemy2_script>().EnemyLife += AddHP;
        HardEnemy3.GetComponent<HardEnemy3_script>().EnemyLife += AddHP;
    }

    public void SetDefaultSettings()
    {
        StandartEnemy1.GetComponent<StandartEnemy1_script>().EnemyLife = 5;
        StandartEnemy2.GetComponent<StandartEnemy2_script>().EnemyLife = 5;
        StandartEnemy3.GetComponent<StandartEnemy3_script>().EnemyLife = 5;

        LightEnemy1.GetComponent<LightEnemy1_script>().EnemyLife = 1;
        LightEnemy2.GetComponent<LightEnemy2_script>().EnemyLife = 2;

        HardEnemy1.GetComponent<HardEnemy1_script>().EnemyLife = 15;
        HardEnemy2.GetComponent<HardEnemy2_script>().EnemyLife = 15;
        HardEnemy3.GetComponent<HardEnemy3_script>().EnemyLife = 25;

        NextWaveDelay = 14;
        NextHPKitDelay = 60;
    }
}

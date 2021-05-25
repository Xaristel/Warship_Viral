using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemy1_script : MonoBehaviour
{
    public GameObject Gun1;
    public GameObject Gun2; //где создать
    public GameObject RocketGun1;
    public GameObject RocketGun2;
    public GameObject Shot;
    public GameObject Rocket; //что создать
    public GameObject EnemyExplosion;

    Rigidbody ship;

    public float speed = 20; //скорость

    public float shotDelayRocket1 = 4;
    public float nextShotRocket1 = 0;
    public float shotDelayLazer1 = 3;
    public float nextShotLazer1 = 0;

    public float shotDelayRocket2 = 5;
    public float nextShotRocket2 = 0;
    public float shotDelayLazer2 = 4;
    public float nextShotLazer2 = 0;

    public int gameMode = 0;
    public int spawnType = 0;
    public float moveHorizontal = 0;
    public float moveVertical = -1;
    private bool endOfStartMoving = true;
    private float lastX = 0;
    public int EnemyLife = 20;

    protected GameController_script gameController_Script;
    protected EnemyCreator_script enemyCreator_Script;
    private GameObject Player;

    void Start() // Start is called before the first frame update
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();
        enemyCreator_Script = GameObject.FindGameObjectWithTag("EnemyCreator").GetComponent<EnemyCreator_script>();

        Player = GameObject.Find("Player");

        ship = GetComponent<Rigidbody>(); //this ship
        gameMode = gameController_Script.getMode();
        spawnType = enemyCreator_Script.GetSpawnType();

        ship.rotation = Quaternion.Euler(0, 180, 0);
        lastX = ship.position.x;
        nextShotRocket1 = Time.time + 3;
        nextShotLazer1 = Time.time + 3;
        nextShotRocket2 = Time.time + 4;
        nextShotLazer2 = Time.time + 4;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController_Script.getIsStarted())
        {
            GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 0;

            if (gameController_Script.GetIsGameEnd())
            {
                Destroy(gameObject); //destroy enemy
                Instantiate(EnemyExplosion, ship.transform.position, Quaternion.identity);
            }

            return;
        }

        //полет и наклонение
        switch (spawnType)
        {
            case 6:
                {
                    if (ship.position.z > 30)
                    {
                        moveHorizontal = 0;
                        moveVertical = -1;
                        ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;
                    }
                    else
                    {
                        if (endOfStartMoving) //конец движения корабля к начальной точке 
                        {
                            moveHorizontal = 1;
                            speed = 5;
                            endOfStartMoving = false;
                        }

                        if (ship.position.x <= lastX - 10) //ограничение движения и смена направления при достижении границы
                        {
                            moveHorizontal = 1;
                            lastX = ship.position.x + 10;
                        }

                        if (ship.position.x >= lastX + 10)
                        {
                            moveHorizontal = -1;
                            lastX = ship.position.x - 10;
                        }

                        ship.velocity = new Vector3(moveHorizontal, 0, 0) * speed; //установление скорости перемещения
                    }

                    break;
                }
            case 2:
                {
                    break;
                }
            case 3:
                {
                    break;
                }
        }

        if (Time.time > nextShotLazer1) // если текущее время больше предыдущего на shotDelay
        {
            Instantiate(Shot, Gun1.transform.position, Quaternion.identity);
            nextShotLazer1 = Time.time + shotDelayLazer1;
        }

        if (Time.time > nextShotRocket1) // если текущее время больше предыдущего на shotDelay
        {
            Instantiate(Rocket, RocketGun1.transform.position, Quaternion.identity);
            nextShotRocket1 = Time.time + shotDelayRocket1;
        }

        if (Time.time > nextShotLazer2) // если текущее время больше предыдущего на shotDelay
        {
            Instantiate(Shot, Gun2.transform.position, Quaternion.identity);
            nextShotLazer2 = Time.time + shotDelayLazer2;
        }

        if (Time.time > nextShotRocket2) // если текущее время больше предыдущего на shotDelay
        {
            Instantiate(Rocket, RocketGun2.transform.position, Quaternion.identity);
            nextShotRocket2 = Time.time + shotDelayRocket2;
        }

        if (Player != null)
        {
            var rigidbody = GetComponent<Rigidbody>();
            // Определение целевой ротации.
            Vector3 targetPoint = Player.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            // Поворот корабля к целевой точке.
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime) * Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -15 * 2 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerShot")
        {
            EnemyLife--;

            if (EnemyLife == 0)
            {
                Destroy(gameObject); //destroy enemy
                Instantiate(EnemyExplosion, ship.transform.position, Quaternion.identity);
                gameController_Script.IncreaseScore("HardEnemy");
            }
        }
    }
}

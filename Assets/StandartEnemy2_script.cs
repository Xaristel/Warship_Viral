using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartEnemy2_script : MonoBehaviour
{
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject Rocket1;
    public GameObject Rocket2;

    public GameObject Shot;
    public GameObject Rocket;
    public GameObject EnemyExplosion;

    private Rigidbody ship;

    private float speed = 20;

    private float shotDelayLazer1 = 4;
    private float shotDelayRocket1 = 6;
    private float nextShotLazer1 = 0;
    private float nextShotRocket1 = 0;

    private float shotDelayLazer2 = 5;
    private float shotDelayRocket2 = 7;
    private float nextShotLazer2 = 0;
    private float nextShotRocket2 = 0;

    private int gameMode = 0;
    private int enemyNumber = 0;
    private float moveHorizontal = 0;
    private float moveVertical = -1;
    public int EnemyLife = 5;

    protected GameController_script gameController_Script;
    protected GameObject Player;

    void Start()
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();
        Player = GameObject.FindGameObjectWithTag("Player");

        ship = GetComponent<Rigidbody>();
        ship.rotation = Quaternion.Euler(0, 180, 0);

        nextShotLazer1 = Time.time + 3;
        nextShotRocket1 = Time.time + 3;
        nextShotLazer2 = Time.time + 4;
        nextShotRocket2 = Time.time + 4;

        if (ship.transform.position.x > -21 && ship.transform.position.x < -19)
        {
            enemyNumber = 1;
            moveHorizontal = 1;
        }
        else if (ship.transform.position.x > -1 && ship.transform.position.x < 1)
        {
            enemyNumber = 2;
        }
        else if (ship.transform.position.x > 19 && ship.transform.position.x < 21)
        {
            moveHorizontal = -1;
            enemyNumber = 3;
        }
    }

    void Update()
    {
        if (!gameController_Script.getIsStarted())
        {
            GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 0;

            if (gameController_Script.GetIsGameEnd())
            {
                Destroy(gameObject);
                Instantiate(EnemyExplosion, ship.transform.position, Quaternion.identity);
            }

            return;
        }

        switch (enemyNumber)
        {
            case 1:
                {
                    if (ship.position.z > 65)
                    {
                        moveVertical = -1;
                    }
                    if (ship.position.z < 0)
                    {
                        moveVertical = 1;
                    }
                    if (ship.position.x < -35)
                    {
                        moveHorizontal = 1;
                    }
                    if (ship.position.x > -5)
                    {
                        moveHorizontal = -1;
                    }
                    ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

                    break;
                }
            case 2:
                {
                    if (ship.position.z < -15)
                    {
                        moveVertical = 1;
                    }
                    else if (ship.position.z > 65)
                    {
                        moveVertical = -1;
                    }
                    ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

                    break;
                }
            case 3:
                {
                    if (ship.position.z > 65)
                    {
                        moveVertical = -1;
                    }
                    if (ship.position.z < 0)
                    {
                        moveVertical = 1;
                    }
                    if (ship.position.x < 5)
                    {
                        moveHorizontal = 1;
                    }
                    if (ship.position.x > 35)
                    {
                        moveHorizontal = -1;
                    }
                    ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

                    break;
                }
            default:
                break;
        }

        if (Time.time > nextShotLazer1)
        {
            Instantiate(Shot, Gun1.transform.position, Quaternion.identity);
            nextShotLazer1 = Time.time + shotDelayLazer1;
        }

        if (Time.time > nextShotLazer2)
        {
            Instantiate(Shot, Gun2.transform.position, Quaternion.identity);
            nextShotLazer2 = Time.time + shotDelayLazer2;
        }

        if (Time.time > nextShotRocket1)
        {
            switch (gameMode)
            {
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        Instantiate(Rocket, Rocket1.transform.position, Quaternion.identity);
                        break;
                    }
                case 3:
                    {
                        Instantiate(Rocket, Rocket1.transform.position, Quaternion.identity);
                        break;
                    }
            }
            nextShotRocket1 = Time.time + shotDelayRocket1;
        }

        if (Time.time > nextShotRocket2)
        {
            switch (gameMode)
            {
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        Instantiate(Rocket, Rocket2.transform.position, Quaternion.identity);
                        break;
                    }
                case 3:
                    {
                        Instantiate(Rocket, Rocket2.transform.position, Quaternion.identity);
                        break;
                    }
            }
            nextShotRocket2 = Time.time + shotDelayRocket2;
        }

        if (Player != null)
        {
            Vector3 targetPoint = Player.transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime) * Quaternion.Euler(0.0f, 0.0f, ship.velocity.x * -15 * 2 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerShot")
        {
            EnemyLife--;
            Destroy(other.gameObject);

            if (EnemyLife == 0)
            {
                Destroy(gameObject);
                Instantiate(EnemyExplosion, ship.transform.position, Quaternion.identity);
                gameController_Script.IncreaseScore("StandartEnemy");
            }
        }
    }
}

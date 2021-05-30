using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartEnemy1_script : MonoBehaviour
{
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject Shot;
    public GameObject EnemyExplosion;

    private Rigidbody ship;

    private float speed = 20;

    private float shotDelay1 = 4;
    private float nextShot1 = 0;
    private float shotDelay2 = 5;
    private float nextShot2 = 0;

    private float moveHorizontal = 0;
    private float moveVertical = -1;
    public int EnemyLife = 5;
    private int enemyNumber = 0;

    protected GameController_script gameController_Script;
    protected GameObject Player;

    void Start()
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();
        Player = GameObject.Find("Player");

        ship = GetComponent<Rigidbody>();
        ship.rotation = Quaternion.Euler(0, 180, 0);

        nextShot1 = Time.time + shotDelay1;
        nextShot2 = Time.time + shotDelay2;

        if (ship.transform.position.x > -29 && ship.transform.position.x < -27)
        {
            enemyNumber = 1;
        }
        else if (ship.transform.position.x > -13 && ship.transform.position.x < -11)
        {
            enemyNumber = 2;
        }
        else if (ship.transform.position.x > 11 && ship.transform.position.x < 13)
        {
            enemyNumber = 3;
        }
        else if (ship.transform.position.x > 27 && ship.transform.position.x < 29)
        {
            enemyNumber = 4;
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
                    if (ship.position.z < -20)
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
            case 4:
                {
                    if (ship.position.z < -20)
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
            default:
                break;
        }

        if (Time.time > nextShot1)
        {
            Instantiate(Shot, Gun1.transform.position, Quaternion.identity);
            nextShot1 = Time.time + shotDelay1;
        }
        if (Time.time > nextShot2)
        {
            Instantiate(Shot, Gun2.transform.position, Quaternion.identity);
            nextShot2 = Time.time + shotDelay2;
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

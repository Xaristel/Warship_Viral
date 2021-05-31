using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemy1_script : MonoBehaviour
{
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject RocketGun1;
    public GameObject RocketGun2;
    public GameObject Shot;
    public GameObject Rocket;
    public GameObject EnemyExplosion;

    private Rigidbody ship;

    private float speed = 20;

    private float shotDelayRocket1 = 4;
    private float nextShotRocket1 = 0;
    private float shotDelayLazer1 = 3;
    private float nextShotLazer1 = 0;

    private float shotDelayRocket2 = 5;
    private float nextShotRocket2 = 0;
    private float shotDelayLazer2 = 4;
    private float nextShotLazer2 = 0;

    private int gameMode = 0;
    private int spawnType = 0;
    private float moveHorizontal = 0;
    private float moveVertical = -1;
    private bool endOfStartMoving = true;
    private float lastX = 0;
    public int EnemyLife = 20;

    protected GameController_script gameController_Script;
    protected GameObject Player;

    void Start()
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();

        Player = GameObject.Find("Player");

        ship = GetComponent<Rigidbody>();
        gameMode = gameController_Script.getMode();

        ship.rotation = Quaternion.Euler(0, 180, 0);
        lastX = ship.position.x;
        nextShotRocket1 = Time.time + 3;
        nextShotLazer1 = Time.time + 3;
        nextShotRocket2 = Time.time + 4;
        nextShotLazer2 = Time.time + 4;
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

        if (ship.position.z > 30)
        {
            moveHorizontal = 0;
            moveVertical = -1;
            ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;
        }
        else
        {
            if (endOfStartMoving)
            {
                moveHorizontal = 1;
                speed = 5;
                endOfStartMoving = false;
            }

            if (ship.position.x <= lastX - 10)
            {
                moveHorizontal = 1;
                lastX = ship.position.x + 10;
            }

            if (ship.position.x >= lastX + 10)
            {
                moveHorizontal = -1;
                lastX = ship.position.x - 10;
            }

            ship.velocity = new Vector3(moveHorizontal, 0, 0) * speed;
        }

        if (Time.time > nextShotLazer1)
        {
            Instantiate(Shot, Gun1.transform.position, Quaternion.identity);
            nextShotLazer1 = Time.time + shotDelayLazer1;
        }

        if (Time.time > nextShotRocket1)
        {
            Instantiate(Rocket, RocketGun1.transform.position, Quaternion.identity);
            nextShotRocket1 = Time.time + shotDelayRocket1;
        }

        if (Time.time > nextShotLazer2)
        {
            Instantiate(Shot, Gun2.transform.position, Quaternion.identity);
            nextShotLazer2 = Time.time + shotDelayLazer2;
        }

        if (Time.time > nextShotRocket2)
        {
            Instantiate(Rocket, RocketGun2.transform.position, Quaternion.identity);
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
                gameController_Script.IncreaseScore("HardEnemy");
            }
        }
    }
}

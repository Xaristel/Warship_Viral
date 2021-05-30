using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemy3_script : MonoBehaviour
{
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject Shot;
    public GameObject EnemyExplosion;
    public GameObject Shield;

    private Rigidbody ship;

    private float speed = 10;

    private float shotDelayLazer1 = 4;
    private float nextShotLazer1 = 0;
    private float shotDelayLazer2 = 5;
    private float nextShotLazer2 = 0;

    private float moveHorizontal = 0;
    private float moveVertical = -1;
    private bool endOfStartMoving = true;
    public int EnemyLife = 30;

    protected GameController_script gameController_Script;
    protected GameObject Player;

    void Start()
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();

        Player = GameObject.FindGameObjectWithTag("Player");

        ship = GetComponent<Rigidbody>();
        ship.rotation = Quaternion.Euler(0, 180, 0);

        nextShotLazer1 = Time.time + 3;
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

            if (ship.position.x <= -30)
            {
                moveHorizontal = 1;
            }
            if (ship.position.x >= 30)
            {
                moveHorizontal = -1;
            }

            ship.velocity = new Vector3(moveHorizontal, 0, 0) * speed;
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
            Shield.SetActive(true);
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

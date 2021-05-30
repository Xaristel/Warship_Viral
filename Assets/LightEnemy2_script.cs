using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy2_script : MonoBehaviour
{
    public GameObject RocketGun1;
    public GameObject RocketGun2;
    public GameObject Rocket;
    public GameObject EnemyExplosion;

    private Rigidbody ship;
    private float speed = 50;

    private float nextShotRocket1 = 0;
    private float nextShotRocket2 = 0;

    private float moveVertical = -1;
    public int EnemyLife = 2;

    protected GameController_script gameController_Script;
    protected EnemyCreator_script enemyCreator_Script;
    protected GameObject Player;

    void Start()
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();
        Player = GameObject.FindGameObjectWithTag("Player");

        ship = GetComponent<Rigidbody>();
        ship.rotation = Quaternion.Euler(0, 180, 0);
        ship.velocity = new Vector3(0, 0, moveVertical) * speed;

        nextShotRocket1 = Time.time + 0.3F;
        nextShotRocket2 = Time.time + 1.3F;
    }

    void Update()
    {
        if (!gameController_Script.getIsStarted())
        {
            ship.velocity = gameObject.transform.forward * 0;

            if (gameController_Script.GetIsGameEnd())
            {
                Destroy(gameObject);
                Instantiate(EnemyExplosion, ship.transform.position, Quaternion.identity);
            }

            return;
        }
        else
        {
            ship.velocity = new Vector3(0, 0, moveVertical) * speed;
        }

        if (Time.time > nextShotRocket1)
        {
            Instantiate(Rocket, RocketGun1.transform.position, Quaternion.identity);
            nextShotRocket1 = float.MaxValue;
        }

        if (Time.time > nextShotRocket2)
        {
            Instantiate(Rocket, RocketGun2.transform.position, Quaternion.identity);
            nextShotRocket2 = float.MaxValue;
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
                gameController_Script.IncreaseScore("LightEnemy");
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemy2_script : MonoBehaviour
{
    public GameObject RocketGun1;
    public GameObject RocketGun2; //где создать
    public GameObject Rocket; //что создать
    public GameObject EnemyExplosion;
    public GameObject Shield;

    Rigidbody ship;

    public float speed = 40; //скорость

    public float shotDelayRocket1 = 4;
    public float nextShotRocket1 = 0;
    public float shotDelayRocket2 = 5;
    public float nextShotRocket2 = 0;

    public int gameMode = 0;
    public int spawnType = 0;
    public float moveHorizontal = 0;
    public float moveVertical = -1;
    private bool endOfStartMoving = true;
    private float lastX = 0;
    public int EnemyLife = 2;

    protected GameController_script gameController_Script;
    protected EnemyCreator_script enemyCreator_Script;
    protected GameObject Player;

    void Start() // Start is called before the first frame update
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();
        enemyCreator_Script = GameObject.FindGameObjectWithTag("EnemyCreator").GetComponent<EnemyCreator_script>();

        Player = GameObject.FindGameObjectWithTag("Player");

        ship = GetComponent<Rigidbody>();
        gameMode = gameController_Script.getMode();
        spawnType = enemyCreator_Script.GetSpawnType();

        ship.rotation = Quaternion.Euler(0, 180, 0);
        lastX = ship.position.x;
        nextShotRocket1 = Time.time + 3;
        nextShotRocket2 = Time.time + 4;
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
            case 5:
                {
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

        if (Time.time > nextShotRocket1) // если текущее время больше предыдущего на shotDelay
        {
            Instantiate(Rocket, RocketGun1.transform.position, Quaternion.identity);
            nextShotRocket1 = Time.time + shotDelayRocket1;
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
            // Поворот к целевой точке.
            gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime) * Quaternion.Euler(0.0f, 0.0f, rigidbody.velocity.x * -15 * 2 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerShot")
        {
            EnemyLife--;
            Shield.SetActive(true);
            Destroy(other.gameObject); //destroy shot

            if (EnemyLife == 0)
            {
                Destroy(gameObject); //destroy enemy
                Instantiate(EnemyExplosion, ship.transform.position, Quaternion.identity);
                gameController_Script.IncreaseScore("LightEnemy");
            }
        }
    }
}

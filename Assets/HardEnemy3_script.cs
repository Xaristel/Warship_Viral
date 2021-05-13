using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemy3_script : MonoBehaviour
{
    public GameObject Gun1;
    public GameObject Gun2; //где создать
    public GameObject Shot;
    public GameObject EnemyExplosion;
    public GameObject Shield;

    Rigidbody ship;

    public float speed = 15; //скорость

    public float shotDelayLazer1 = 4;
    public float nextShotLazer1 = 0;
    public float shotDelayLazer2 = 5;
    public float nextShotLazer2 = 0;

    public int gameMode = 0;
    public int spawnType = 0;
    public float moveHorizontal = 0;
    public float moveVertical = -1;
    private bool endOfStartMoving = true;
    private int EnemyLife = 30;

    protected GameController_script gameController_Script;
    protected EnemyCreator_script EnemyCreator_Script;
    protected GameObject Player;

    void Start() // Start is called before the first frame update
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();
        EnemyCreator_Script = GameObject.FindGameObjectWithTag("EnemyCreator").GetComponent<EnemyCreator_script>();

        Player = GameObject.FindGameObjectWithTag("Player");

        ship = GetComponent<Rigidbody>();
        gameMode = gameController_Script.getMode();
        spawnType = EnemyCreator_Script.GetSpawnType();

        ship.rotation = Quaternion.Euler(0, 180, 0);
        nextShotLazer1 = Time.time + 3;
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
            case 8:
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

        if (Time.time > nextShotLazer2) // если текущее время больше предыдущего на shotDelay
        {
            Instantiate(Shot, Gun2.transform.position, Quaternion.identity);
            nextShotLazer2 = Time.time + shotDelayLazer2;
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
                gameController_Script.IncreaseScore("HardEnemy");
            }
        }
    }
}

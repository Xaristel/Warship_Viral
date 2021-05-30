using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardEnemy2_script : MonoBehaviour
{
    public GameObject Gun1;
    public GameObject Gun2;
    public GameObject PlasmaGun1;
    public GameObject PlasmaGun2;
    public GameObject Shot;
    public GameObject Plasma;
    public GameObject EnemyExplosion;

    private Rigidbody ship;

    private float speed = 15;

    private float shotDelayLazer1 = 3;
    private float nextShotLazer1 = 0;
    private float shotDelayLazer2 = 4;
    private float nextShotLazer2 = 0;

    private float nextShotPlasma1 = 0;
    private float nextShotPlasma2 = 0;
    private float ShotDelayPlasma1 = 6;
    private float ShotDelayPlasma2 = 7;

    private float moveHorizontal = 0;
    private float moveVertical = -1;
    public int EnemyLife = 20;

    protected GameController_script gameController_Script;
    protected EnemyCreator_script enemyCreator_Script;
    protected GameObject Player;

    void Start()
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();
        enemyCreator_Script = GameObject.FindGameObjectWithTag("EnemyCreator").GetComponent<EnemyCreator_script>();

        Player = GameObject.FindGameObjectWithTag("Player");

        ship = GetComponent<Rigidbody>();
        ship.rotation = Quaternion.Euler(0, 180, 0);

        nextShotPlasma1 = Time.time + 3;
        nextShotLazer1 = Time.time + 3;
        nextShotPlasma2 = Time.time + 4;
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

        if (ship.position.z < 0)
        {
            moveVertical = 1;
        }
        else if (ship.position.z > 65)
        {
            moveVertical = -1;
        }
        ship.velocity = new Vector3(moveHorizontal, 0, moveVertical) * speed;

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

        if (Time.time > nextShotPlasma1)
        {
            Instantiate(Plasma, PlasmaGun1.transform.position, Quaternion.identity);
            nextShotPlasma1 = Time.time + ShotDelayPlasma1;
        }

        if (Time.time > nextShotPlasma2)
        {
            Instantiate(Plasma, PlasmaGun1.transform.position, Quaternion.identity);
            nextShotPlasma2 = Time.time + ShotDelayPlasma2;
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

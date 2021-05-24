using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_script : MonoBehaviour
{
    public float speed; //скорость
    public float tilt; //наклон
    public float xMin, xMax, zMin, zMax; //ограничение движения
    public float shotDelay;
    public float nextShot;
    public float rocketDelay;
    public float nextRocket;

    private GameController_script gameController_Script;
    private int GunLevel = 1;
    private int Life = 10;
    private float defendDelay = 1;
    private float defendTime = 0;

    public GameObject Gun1;//где создать
    public GameObject Gun2;
    public GameObject AddGun1;
    public GameObject AddGun2;
    public GameObject AddGun3;
    public GameObject AddGun4;
    public GameObject Rocket1;
    public GameObject Rocket2;
    public GameObject Engine1;
    public GameObject Engine2;
    public GameObject Engine3;
    public GameObject Engine4;
    public GameObject Engine5;
    public GameObject CenterGun;
    public GameObject Shot;
    public GameObject Rocket;
    public GameObject PlayerExplosion;
    public GameObject Shield;

    Rigidbody ship;

    public Rigidbody GetRigidbody()
    {
        return ship;
    }

    public int GetGunLevel()
    {
        return GunLevel;
    }

    public void SetGunLevel(int level)
    {
        GunLevel = level;
    }

    public void IncreaseGunLevel()
    {
        GunLevel++;
    }
    public int GetPlayerLife()
    {
        return Life;
    }

    public void SetPlayerLife(int life)
    {
        Life = life;
    }

    public void DecreasePlayerLife()
    {
        Life--;
    }

    public void IncreasePlayerLife()
    {
        Life++;
    }

    void Start() //вызывается при создании объекта 
    {
        gameController_Script = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController_script>();
        ship = GetComponent<Rigidbody>();
    }

    private bool moveAllowed = false;
    public float deltaX, deltaZ;


    void Update() //вызывается на каждый кадр
    {
        if (!gameController_Script.getIsStarted())
        {
            if (Life < 0)
            {
                Life = 0;
            }
            return;
        }

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    {
                        if (GetComponent<BoxCollider>() == Physics2D.OverlapPoint(touchPos))
                        {
                            // get the offset between position you touches
                            // and the center of the game object
                            deltaX = touchPos.x - transform.position.x;
                            deltaZ = touchPos.z - transform.position.z;
                            // if touch begins within the ball collider
                            // then it is allowed to move
                            moveAllowed = true;
                            // restrict some rigidbody properties so it moves
                            // more  smoothly and correctly
                            ship.velocity = new Vector3(0, 0, 0);
                        }
                        break;
                    }

                case TouchPhase.Moved:
                    {
                        if (GetComponent<BoxCollider>() == Physics2D.OverlapPoint(touchPos) && moveAllowed)
                        {
                            ship.MovePosition(new Vector3(touchPos.x - deltaX, ship.position.y, touchPos.z - deltaZ));
                        }

                        break;
                    }

                case TouchPhase.Ended:
                    {
                        // restore initial parameters
                        // when touch is ended
                        moveAllowed = false;
                        break;
                    }

            }
        }
        //ограничение движения
        var xPosition = Mathf.Clamp(ship.position.x, xMin, xMax);
        var zPosition = Mathf.Clamp(ship.position.z, zMin, zMax);

        ship.position = new Vector3(xPosition, 0, zPosition);


        if (Time.time > nextShot) // если текущее время больше предыдущего на shotDelay
        {
            switch (GunLevel)
            {
                case 1:
                    {
                        Instantiate(Shot, Gun1.transform.position, Quaternion.identity);
                        Instantiate(Shot, Gun2.transform.position, Quaternion.identity);
                        break;
                    }
                case 2:
                    {
                        Instantiate(Shot, Gun1.transform.position, Quaternion.identity);
                        Instantiate(Shot, Gun2.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun1.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun2.transform.position, Quaternion.identity);
                        break;
                    }
                case 3:
                    {
                        Instantiate(Shot, Gun1.transform.position, Quaternion.identity);
                        Instantiate(Shot, Gun2.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun1.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun2.transform.position, Quaternion.identity);
                        break;
                    }
                case 4:
                    {
                        Instantiate(Shot, Gun1.transform.position, Quaternion.identity);
                        Instantiate(Shot, Gun2.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun1.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun2.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun3.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun4.transform.position, Quaternion.identity);
                        break;
                    }
                case 5:
                    {
                        Instantiate(Shot, Gun1.transform.position, Quaternion.identity);
                        Instantiate(Shot, Gun2.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun1.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun2.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun3.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun4.transform.position, Quaternion.identity);
                        break;
                    }
                default:
                    {
                        Instantiate(Shot, Gun1.transform.position, Quaternion.identity);
                        Instantiate(Shot, Gun2.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun1.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun2.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun3.transform.position, Quaternion.identity);
                        Instantiate(Shot, AddGun4.transform.position, Quaternion.identity);
                        break;
                    }
            }
            nextShot = Time.time + shotDelay;
        }

        if (Time.time > nextRocket) // если текущее время больше предыдущего на shotDelay
        {
            switch (GunLevel)
            {
                case 1:
                    {
                        break;
                    }
                case 2:
                    {
                        break;
                    }
                case 3:
                    {
                        Instantiate(Rocket, Rocket1.transform.position, Quaternion.identity);
                        Instantiate(Rocket, Rocket2.transform.position, Quaternion.identity);
                        break;
                    }
                case 4:
                    {
                        Instantiate(Rocket, Rocket1.transform.position, Quaternion.identity);
                        Instantiate(Rocket, Rocket2.transform.position, Quaternion.identity);
                        break;
                    }
                case 5:
                    {
                        Instantiate(Rocket, Rocket1.transform.position, Quaternion.identity);
                        Instantiate(Rocket, Rocket2.transform.position, Quaternion.identity);
                        break;
                    }
                default:
                    {
                        Instantiate(Rocket, Rocket1.transform.position, Quaternion.identity);
                        Instantiate(Rocket, Rocket2.transform.position, Quaternion.identity);
                        break;
                    }
            }
            nextRocket = Time.time + rocketDelay;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HP Kit")
        {
            IncreasePlayerLife();
            Shield.SetActive(true);
        }

        if (Time.time < defendTime + defendDelay)
        {
            if (other.tag == "EnemyShot" || other.tag == "Asteroid" || other.tag == "InsectoidEnemy" && gameController_Script.getIsStarted())
            {
                Shield.SetActive(true);
                Destroy(other.gameObject);
            }
            return;
        }

        if (other.tag == "EnemyShot" || other.tag == "Asteroid" || other.tag == "InsectoidEnemy" && gameController_Script.getIsStarted())
        {
            DecreasePlayerLife();
            Shield.SetActive(true);
            Destroy(other.gameObject);
            defendTime = Time.time;
        }

        if (GetPlayerLife() == 0)
        {
            gameObject.SetActive(false);
            Instantiate(PlayerExplosion, ship.transform.position, Quaternion.identity);
        }
    }
}
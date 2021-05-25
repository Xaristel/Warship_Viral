using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot_Script : MonoBehaviour
{
    public float speed;
    public GameObject Explosion;
    private GameController_script gameController_Script;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = gameObject.transform.forward * speed;
        gameController_Script = GameObject.Find("GameController").GetComponent<GameController_script>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameController_Script.getIsStarted())
        {
            GetComponent<Rigidbody>().velocity = gameObject.transform.forward * 0;

            if (gameController_Script.GetIsGameEnd())
            {
                Destroy(gameObject);
            }
        }
        else
        {
            GetComponent<Rigidbody>().velocity = gameObject.transform.forward * speed;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "LightEnemy" && other.tag == "StandartEnemy" && other.tag == "HardEnemy" && other.tag == "Boss")
        {
            Instantiate(Explosion, gameObject.transform.position, Quaternion.identity);
        }
    }
}

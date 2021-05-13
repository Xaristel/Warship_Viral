using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot_Script : MonoBehaviour
{
    public float speed;
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
}

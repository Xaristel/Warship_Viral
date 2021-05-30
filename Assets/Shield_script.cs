using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_script : MonoBehaviour
{
    float ShieldTime = 0;
    bool isTimeToFalse = true;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy && isTimeToFalse)
        {
            ShieldTime = Time.time;
            isTimeToFalse = false;
        }

        if (Time.time > ShieldTime + 1.2)
        {
            gameObject.SetActive(false);
            ShieldTime = float.MaxValue;
            isTimeToFalse = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyShot" || other.tag == "Asteroid" || other.tag == "InsectoidEnemy")
        {
            Destroy(other.gameObject);
        }
    }
}

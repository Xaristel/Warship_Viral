using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield_script : MonoBehaviour
{
    float ShieldDelay = 0;
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
            ShieldDelay = Time.time;
            isTimeToFalse = false;
        }

        if (Time.time > ShieldDelay + 1)
        {
            gameObject.SetActive(false);
            ShieldDelay = float.MaxValue;
            isTimeToFalse = true;
        }
    }
}

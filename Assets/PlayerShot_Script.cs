using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot_Script : MonoBehaviour
{
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = gameObject.transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

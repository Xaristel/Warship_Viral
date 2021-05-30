using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPKit_script : MonoBehaviour
{
    private int speed = -20;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 1) * speed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma_script : MonoBehaviour
{
    public float speed;
    private float Dist;

    protected GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player");

        Dist = Vector3.Distance(gameObject.transform.position, Player.transform.position);
        Dist = 85 / Dist;

        GetComponent<Rigidbody>().velocity = (gameObject.transform.position - Player.transform.position) * speed * Dist;

        Vector3 targetPoint = Player.transform.position;// Определение целевой ротации.
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20);// Поворот к целевой точке.
    }

    // Update is called once per frame
    void Update()
    {

    }
}

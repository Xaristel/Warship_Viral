using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerGun_Script : MonoBehaviour
{
    public float speed;
    public Vector3 VectorSpeed;

    private GameObject Player;

    private float Dist;

    void Start()
    {
        Player = GameObject.Find("Player");

        VectorSpeed = gameObject.transform.position - Player.transform.position;
        Dist = Vector3.Distance(gameObject.transform.position, Player.transform.position);
        Dist = 85 / Dist;

        GetComponent<Rigidbody>().velocity = VectorSpeed * speed * Dist;

        Vector3 targetPoint = Player.transform.position; // Определение целевой ротации.
        Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

        gameObject.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20); // Поворот к целевой точке.
    }

    void Update()
    {
    }
}
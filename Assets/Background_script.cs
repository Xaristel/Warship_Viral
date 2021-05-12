using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_script : MonoBehaviour
{
    public float speed;

    private Vector3 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()                    // пока нет возможности реализовать перемещение фона
    {
        //var shift = Mathf.Repeat(Time.time * speed, 150);
        //transform.position = startPosition + Vector3.back * shift;
    }
}

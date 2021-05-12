using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Border_script : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        Destroy(other.gameObject);
    }
}

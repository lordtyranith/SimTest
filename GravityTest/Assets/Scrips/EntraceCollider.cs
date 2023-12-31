using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntraceCollider : MonoBehaviour
{
    public GameObject objectToActivate;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objectToActivate.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            objectToActivate.SetActive(false);
        }
    }
}
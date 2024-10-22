using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Coroutine Coroutine;
    private void OnEnable()
    {
        Coroutine = StartCoroutine(ActiveTime());
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    IEnumerator ActiveTime()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        gameObject.SetActive(false);
    }
}

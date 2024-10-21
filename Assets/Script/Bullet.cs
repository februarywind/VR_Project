using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    Coroutine Coroutine;
    private void OnEnable()
    {
        particle = GameObject.FindWithTag("GunParticle").GetComponent<ParticleSystem>();
        Coroutine = StartCoroutine(ActiveTime());
        transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Monster monster = other.transform.root.GetComponent<Monster>();
    //    if (monster == null) return;
    //    monster.HitDmg(other.transform.GetComponent<HitPoint>().HitPointDmg);
    //    particle.gameObject.transform.position = other.transform.position;
    //    particle.Play();
    //    gameObject.SetActive(false);
    //    StopCoroutine(Coroutine);
    //}
    IEnumerator ActiveTime()
    {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}

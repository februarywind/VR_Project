using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    private void OnEnable()
    {
        particle = GameObject.FindWithTag("GunParticle").GetComponent<ParticleSystem>();
    }
    private void OnTriggerEnter(Collider other)
    {
        Monster monster = other.transform.root.GetComponent<Monster>();
        if (monster == null) return;
        monster.HitDmg(other.transform.GetComponent<HitPoint>().HitPointDmg);
        particle.gameObject.transform.position = other.transform.position;
        particle.Play();
    }
}

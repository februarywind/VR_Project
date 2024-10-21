using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using static UnityEngine.ParticleSystem;

public class HandGun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] XRSocketInteractor socket;
    [SerializeField] Magazine magazine;
    [SerializeField] Text ammoText;
    [SerializeField] ParticleSystem particle;

    private void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(SetMagazine);
        socket.selectExited.AddListener(ReSetMagazine);
        particle = GameObject.FindWithTag("GunParticle").GetComponent<ParticleSystem>();
    }

    public void Fire()
    {
        if (magazine == null || magazine.bulletCount < 1) return;
        PoolManager.instance.Create(PoolEnum.Bullet, firePoint).GetComponent<Rigidbody>().AddForce(firePoint.forward * 70, ForceMode.Impulse);
        if (Physics.Raycast(firePoint.position, firePoint.forward, out RaycastHit ray))
        {
            particle.gameObject.transform.position = ray.point + (Vector3.up * 0.1f);
            particle.Play();
            Monster monster = ray.transform.root.GetComponent<Monster>();
            if (monster != null)
                monster.HitDmg(ray.transform.GetComponent<HitPoint>().HitPointDmg);
        }
        magazine.bulletCount--;
        ammoTextUpdate();
        AudioManager.instance.PlaySfx(SfxAudio.HandGun);
    }
    void SetMagazine(SelectEnterEventArgs args)
    {
        magazine = socket.GetOldestInteractableSelected().transform.gameObject.GetComponent<Magazine>();
        ammoTextUpdate();
    }
    void ReSetMagazine(SelectExitEventArgs args)
    {
        magazine = null;
    }
    void ammoTextUpdate()
    {
        ammoText.text = $"{magazine.bulletCount}/{magazine.MaxbulletCount}";
    }
}

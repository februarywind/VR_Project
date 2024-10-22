using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class Machinegun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] XRSocketInteractor socket;
    [SerializeField] Magazine magazine;
    [SerializeField] Text ammoText;
    [SerializeField] ParticleSystem particle;
    [SerializeField] float FireSpeed;

    Coroutine FireCoroutine;
    WaitForSeconds fireWait;

    private void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(SetMagazine);
        socket.selectExited.AddListener(ReSetMagazine);
        particle = GameObject.FindWithTag("GunParticle").GetComponent<ParticleSystem>();
        fireWait = new WaitForSeconds(FireSpeed);
    }
    public void FireStart()
    {
        FireCoroutine = StartCoroutine(FireTrigger());
    }
    public void FireEnd() 
    { 
        StopCoroutine(FireCoroutine);
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
        if (magazine.bulletCount == 0) magazine.ZeroBullet();
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
    IEnumerator FireTrigger()
    {
        while (true)
        {
            Fire();
            yield return fireWait;
        }
    }
}

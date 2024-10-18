using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public class HandGun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform firePoint;
    [SerializeField] XRSocketInteractor socket;
    [SerializeField] Magazine magazine;
    [SerializeField] Text ammoText;

    private void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(SetMagazine);
        socket.selectExited.AddListener(ReSetMagazine);
    }

    public void Fire()
    {
        if (magazine == null || magazine.bulletCount < 1) return;
        PoolManager.instance.Create(PoolEnum.Bullet, firePoint).GetComponent<Rigidbody>().AddForce(firePoint.forward * 10, ForceMode.Impulse);
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

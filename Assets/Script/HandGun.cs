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
    }

    public void Fire()
    {
        if (magazine.bulletCount < 1) return;
        Instantiate(bullet, firePoint.position, firePoint.rotation, null).GetComponent<Rigidbody>().AddForce(firePoint.forward * 10, ForceMode.Impulse);
        magazine.bulletCount--;
        ammoTextUpdate();
    }
    void SetMagazine(SelectEnterEventArgs args)
    {
        magazine = socket.GetOldestInteractableSelected().transform.gameObject.GetComponent<Magazine>();
        ammoTextUpdate();
    }
    void ammoTextUpdate()
    {
        ammoText.text = $"{magazine.bulletCount}/{magazine.MaxbulletCount}";
    }
}

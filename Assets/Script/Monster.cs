using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] int maxHp;
    [SerializeField] int hp;
    [SerializeField] float moveSpeed;
    [SerializeField] Animator animator;
    [SerializeField] bool dead;
    [SerializeField] BoxCollider[] boxColliders;

    private void Awake()
    {
        target = GameObject.FindWithTag("Core").transform;
        animator = GetComponent<Animator>();
        boxColliders = GetComponentsInChildren<BoxCollider>();
    }
    private void OnEnable()
    {
        hp = maxHp;
        dead = false;
        foreach (var item in boxColliders)
            item.enabled = true;
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }

    private void Update()
    {
        if (dead) return;
        transform.LookAt(new Vector3(target.position.x,0, target.position.z));
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }

    public void HitDmg(int dmg)
    {
        hp -= dmg;
        if (hp < 0) Dead();
    }
    void Dead()
    {
        foreach (var item in boxColliders)
            item.enabled = false;
        animator.SetTrigger("Dead");
        dead = true;
        GameManager.instance.monsterSpawner.KillMonster();
        StartCoroutine(DeadCoroutine());
    }
    IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}

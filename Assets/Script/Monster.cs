using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform playerTr;
    [SerializeField] int maxHp;
    [SerializeField] int hp;
    [SerializeField] float moveSpeed;
    [SerializeField] Animator animator;
    [SerializeField] bool dead;

    private void Awake()
    {
        player = Camera.main.gameObject;
        playerTr = player.transform;
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        hp = maxHp;
        dead = false;
    }

    private void Update()
    {
        if (dead) return;
        transform.LookAt(new Vector3(playerTr.position.x,0,playerTr.position.z));
        transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }

    public void HitDmg(int dmg)
    {
        hp -= dmg;
        if (hp < 0) Dead();
    }
    void Dead()
    {
        animator.SetTrigger("Dead");
        dead = true;
        StartCoroutine(DeadCoroutine());
    }
    IEnumerator DeadCoroutine()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour {
    [SerializeField]
    private int maxHP; //設定最大HP 改変させない

    private int hp;             //現在hp
    private bool isDead;        //死亡処理されたか?
    public GameObject deadEffect;

    [SerializeField]
    private float unDamageTime; //無敵時間
    private bool isDamage;
    private float timer;

    [SerializeField]
    private PlayerHPUI hpui;
    private GameManager gameManager;
    private SpriteRenderer sp;

	// Use this for initialization
	void Start () {
        isDead = false;
        isDamage = false;
        hp = maxHP;

        hpui.SetMaxHP(maxHP);

        gameManager = FindObjectOfType<GameManager>();
        sp = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //hpが0かつ死亡処理されてない
		if(hp<=0 && !isDead)
        {
            isDead = true;
            Dead();

            //外に出た時と同じように通知
            gameManager.PlayerOutNotice(gameObject.GetComponent<Collider2D>());
        }

        if(isDamage) timer += Time.deltaTime;

        if(timer >= 1f)
        {
            isDamage = false;
        }
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="damage">マイナスの値で回復もできる。</param>
    public void Damage(int damage)
    {
        if (isDamage) return;

        isDamage = true;
        timer = 0f;

        //加算してジャッジ。
        hp = ((hp -= damage) < maxHP) ? hp : maxHP;

        hpui.SetHP(hp);
    }

    void Dead()
    {
        Instantiate(deadEffect, transform.position, Quaternion.identity);
        sp.sprite = null;  
    }

    void StageOut()
    {
        isDead = true;
    }
}

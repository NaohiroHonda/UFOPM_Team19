using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {


    [SerializeField]
    private GameObject breakParticle;

    public float maxhp = 5;
    private float hp;
    public Sprite s2;
    private SpriteRenderer sr;

    // Use this for initialization
    void Start ()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        hp = maxhp;
	}
	
	// Update is called once per frame
	void Update () {
        if (hp <= maxhp / 2)
            sr.sprite = s2;
        if (hp <= 0)
            DestroyWall();
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        //当たったら破壊
        if(other.gameObject.tag == "Player")
        {
            hp -= 2.5f;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Beam1"|| col.gameObject.tag == "Beam2")
            hp -= 2.5f;
    }

    void DestroyWall()
    {
        //パーティクル発生
        GameObject particle = Instantiate(breakParticle);
        particle.transform.position = this.transform.position + new Vector3(0, 0, 0.2f);
        Destroy(particle, 1f); //1秒後に破壊予約

        Destroy(this.gameObject);
    }
}

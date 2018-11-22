using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour {

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
            Destroy(gameObject);
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        //当たったら破壊
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Beam1"|| col.gameObject.tag == "Beam2")
            hp -= 2.5f;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamLine : MonoBehaviour {

    private LineRenderer beamLinre;
    private Vector3 first;
    private Rigidbody2D rig;
    public float range=50;
    private bool isReflect;

    private AudioSource[] audio;
    private AudioClip breakSE;
	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody2D>();
        beamLinre = transform.Find("line").GetComponent<LineRenderer>();
        beamLinre.SetPosition(0, transform.position);
        first = transform.position;

        audio = GetComponents<AudioSource>();
        breakSE = audio[0].clip;
	}
	
	// Update is called once per frame
	void Update () {
        beamLinre.SetPosition(0,(Vector2)transform.position- rig.velocity.normalized * 3);
        beamLinre.SetPosition(1, transform.position);
        if (Vector3.Distance(first, transform.position) >= range)
            Destroy(gameObject);
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Wall")
            Destroy(gameObject);
        if(col.gameObject.tag == "Player")
        {
            CompletePlayerController player = col.gameObject.GetComponent<CompletePlayerController>();
            if ((int)player.index == 1 && gameObject.tag == "Beam2")
                Destroy(gameObject);
            if ((int)player.index == 2 && gameObject.tag == "Beam1")
                Destroy(gameObject);
        }
        if (col.gameObject.tag == ("Guard"))
        {
            if (!isReflect)
            {
                if (rig != null)
                    rig.velocity *= -1;
                isReflect = true;
                if (gameObject.tag == "Beam1")
                    gameObject.tag = "Beam2";
                else if (gameObject.tag == "Beam2")
                    gameObject.tag = "Beam1";
            }
            else
            {
                audio[0].PlayOneShot(audio[0].clip);
             //   Destroy(gameObject);
            }
        }
    }
}

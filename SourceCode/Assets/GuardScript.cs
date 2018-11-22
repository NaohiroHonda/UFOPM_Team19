using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardScript : MonoBehaviour {
    
    private SpriteRenderer sr;

    // Use this for initialization
    void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float alfa = sr.color.a;
        alfa -= 1f;
        if (alfa < 0)
            alfa = 1;
        sr.color = new Color(1, 1, 1, alfa);
    }
}

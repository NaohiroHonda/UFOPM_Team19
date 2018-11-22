using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamepadInput;

public class AimCtrl : MonoBehaviour {

    public GamePad.Index index;
    public Transform player;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.RotateAround(player.position,Vector3.up, 10);
	}
}

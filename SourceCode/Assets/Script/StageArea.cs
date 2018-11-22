using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageArea : MonoBehaviour {

    private GameManager gameManager;

	// Use this for initialization
	void Start () {
        gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Playerがステージ外に出た時に検知
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Playerが出た時に処理する
            gameManager.PlayerOutNotice(other);
        }
    }
}

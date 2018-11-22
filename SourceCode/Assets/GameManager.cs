using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UICanvasに直接貼り付けて制御。
public class GameManager : MonoBehaviour {
    [SerializeField]
    private GameObject player1, player2;

    [SerializeField]
    private Text winText;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayerOutNotice(Collider2D outPlayer)
    {
        //player1が出た?
        if(outPlayer.gameObject == player1)
        {
            winText.text = "2P win!";
        }
        //else if(outPlayer.gameObject == player2)
        else
        {
            winText.text = "1P win!";
        }
    }
}

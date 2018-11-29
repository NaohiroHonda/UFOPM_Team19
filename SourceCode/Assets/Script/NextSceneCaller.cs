using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GamepadInput;

public class NextSceneCaller : MonoBehaviour {

    [SerializeField]
    private string nextScene;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Any))
        {
            SceneManager.LoadScene(nextScene);
        }
	}
}

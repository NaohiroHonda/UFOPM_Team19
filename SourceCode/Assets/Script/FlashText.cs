using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FlashText : MonoBehaviour {

    private Text text;
    private float alpha;
    private Color color;

    [SerializeField]
    private float speed = 1.5f;
    private float time;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();

        color = text.color;
	}
	
	// Update is called once per frame
	void Update () {
        time += speed * Time.deltaTime;

        alpha = Mathf.Pow(Mathf.Sin(time), 2);


        text.color = new Color(color.r, color.g, color.b, alpha);
	}
}

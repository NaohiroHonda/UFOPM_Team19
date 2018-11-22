using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPUI : MonoBehaviour {

    private float oldhp, hp, maxHP;

    [SerializeField]
    private float moveTime = 2f;

    private float time;
    private bool isMoving;

    [SerializeField]
    private Color maxColor = new Color(0f, 1f, 0f, 0.5f), zeroColor = new Color(1f, 0f, 0f, 0.5f);

    private Color colorNow, oldColor;

    private Image uiImage;

    [SerializeField]
    private GameObject player;

    private void Awake()
    {
        
    }

    // Use this for initialization
    void Start () {
        uiImage = gameObject.GetComponentInChildren<Image>();

        colorNow = maxColor;
        uiImage.color = maxColor;
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.position = player.transform.position;

        if (isMoving) MoveHPBar();
	}

    public void SetMaxHP(float maxHP)
    {
        this.maxHP = maxHP;

        oldhp = maxHP;
        hp = maxHP;
    }

    public void SetHP(float hp)
    {
        oldhp = this.hp;
        this.hp = hp;

        oldColor = colorNow;

        colorNow = Color.Lerp(zeroColor, maxColor, hp / maxHP);
        isMoving = true;
        time = 0f;
    }

    private void MoveHPBar()
    {
        time += Time.deltaTime;

        float timeRatio = time / moveTime;
        float fillRatio = (timeRatio * (hp - oldhp) + oldhp) / maxHP;

        
        uiImage.fillAmount = fillRatio;
        SetColor(timeRatio);


        if(time >= moveTime)
        {
            isMoving = false;
        }
    }

    private void SetColor(float ratio)
    {
        Color newColor = Color.Lerp(oldColor, colorNow, ratio);
        newColor.r = Mathf.Sin(newColor.r * Mathf.PI / 2f);
        newColor.g = Mathf.Sin(newColor.g * Mathf.PI / 2f);

        uiImage.color = newColor;
    }
}

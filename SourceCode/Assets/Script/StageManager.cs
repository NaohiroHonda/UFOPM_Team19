using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour {

    [SerializeField]
    private GameObject ground;    //地面背景Chip
    [SerializeField]
    private GameObject wall;      //壁Object
    [SerializeField]
    private GameObject stageArea; //ステージ範囲の定義Object


    [SerializeField]
    private int _stageSizeX, _stageSizeY; //ステージの大きさ(強制int

    [SerializeField]
    private Vector2 _chipSize;            //Chipのサイズ

    private Vector2 _center;              //中心位置補正用

    //最速で起こしてObj生成
    private void Awake()
    {
        _center.x = _chipSize.x * _stageSizeX / 2f;
        _center.y = _chipSize.y * _stageSizeY / 2f;

        //この後の計算の都合で半ブロック分ずれるので補正
        _center -= _chipSize / 2f;

        MakeStage();
    }

    //地面を生成しつつ全周に壁を張る
    void MakeStage()
    {
        Vector2 pos;

        //ステージの作成
        for(int x = 0; x < _stageSizeX; x++)
        {
            pos.x = _chipSize.x * x - _center.x;

            for(int y = 0; y < _stageSizeY; y++)
            {
                pos.y = _chipSize.y * y - _center.y;

                //生成
                MakeObjectInChildren(ground, pos);

                if (IsEdge(x, y)) MakeObjectInChildren(wall, pos);
            }
        }

        //周囲判定Objの作成/サイズ調整
        GameObject area = Instantiate(stageArea);
        area.transform.position = Vector3.zero;
        area.transform.localScale = new Vector3(_chipSize.x * _stageSizeX, _chipSize.y * _stageSizeY, 0f);
        area.transform.parent = this.gameObject.transform;
    }

    void MakeObjectInChildren(GameObject makeObj, Vector2 position)
    {
        GameObject obj = Instantiate(makeObj);
        obj.transform.position = new Vector3(position.x, position.y);
        obj.transform.parent = this.gameObject.transform;
    }

    bool IsEdge(int x, int y)
    {
        return (x == 0 || x == _stageSizeX - 1 || y == 0 || y == _stageSizeY - 1);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

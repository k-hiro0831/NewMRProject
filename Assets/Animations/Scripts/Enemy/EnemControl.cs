using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemControl : MonoBehaviour{

    [SerializeField, Header("敵のフェーズ状態管理")]
    EnemyPhase enemyPhase = EnemyPhase.phase1;

    [SerializeField,Header("各種類の敵")]
    private GameObject[] _enemys = new GameObject[3];
    [SerializeField, Header("敵の生成位置")]
    private GameObject[] _position = new GameObject[3];
    [SerializeField, Header("敵のフェーズ別の数")]
    private int[] _enemysNumber = new int[0];
    [SerializeField, Header("敵に使用するオブジェクト")]
    private List<GameObject> _enemyList = new List<GameObject>();
    [SerializeField, Header("親オブジェクトの大本になるやつ")]
    private GameObject _parentBase;
    private GameObject _parent;
    private GameObject _parent2;
    private GameObject _parent3;
    private GameObject _parent4;
    [SerializeField, Header("敵フェーズのテキスト")]
    private Text _PhaseText;

    /// <summary>
    /// 敵生成位置
    /// </summary>
    private int _enemyPos;

    /// <summary>
    /// ObjectPool
    /// </summary>
    private ObjectPool _poolPhase1 = new ObjectPool();

    /// <summary>
    /// ObjectPool
    /// </summary>
    private ObjectPool _poolPhase2 = new ObjectPool();

    /// <summary>
    /// ObjectPool
    /// </summary>
    private ObjectPool _poolPhase3 = new ObjectPool();

    /// <summary>
    /// 敵のフェーズ状態管理
    /// </summary>
    private enum EnemyPhase {
        phase1, phase2, phase3, phase4
    }


    // Start is called before the first frame update
    private void Start(){
        StartMethod();
    }

    // Update is called once per frame
    private void Update(){
    }

    private void FixedUpdate(){
        //敵のフェーズ状態管理分岐
        switch (enemyPhase) {
            case EnemyPhase.phase1:
                //第一フェーズの敵が全滅したら
                if (_enemyList.Count == 0) {
                    //敵を生成してからテキストを変更して第二フェーズへ移行
                    EnemyPhase2();
                    _PhaseText.text = "敵フェーズ:2";
                    enemyPhase = EnemyPhase.phase2;
                }
                break;
            case EnemyPhase.phase2:
                //第二フェーズの敵が全滅したら
                if (_enemyList.Count == 0)
                {
                    //敵を生成してからテキストを変更して第三フェーズへ移行
                    EnemyPhase3();
                    _PhaseText.text = "敵フェーズ:3";
                    enemyPhase = EnemyPhase.phase3;
                }
                break;
            case EnemyPhase.phase3:
                //第二フェーズの敵が全滅したら
                if (_enemyList.Count == 0)
                {
                    //敵を生成してからテキストを変更して第三フェーズへ移行
                    _PhaseText.text = "敵フェーズ:4(現在ここまで)";
                    enemyPhase = EnemyPhase.phase4;
                }
                break;
            case EnemyPhase.phase4:
                break;
        }
    }

    private void StartMethod() {
        //親オブジェクトに各敵を登録
        Parent();
        //最初にオブジェクトを作っておく
        _poolPhase1.Pool(_parent, _enemys[0], _enemysNumber[0]);
        _poolPhase2.Pool(_parent2, _enemys[1], _enemysNumber[1]);
        _poolPhase3.Pool(_parent3, _enemys[2], _enemysNumber[2]);
        //第一フェーズの敵を生成
        EnemyPhase1();
        //テキストを変更
        _PhaseText.text = "敵フェーズ:1";
    }

    private void EnemyPhase1()
    {
        for (_enemyPos = 0; _enemyPos < _enemysNumber[0]; _enemyPos++) {
            GameObject _enemyPrefab = _poolPhase1.ReturnObj();
            _enemyPrefab.transform.position = _position[_enemyPos].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(-90f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
        }
    }

    private void EnemyPhase2() {
        for (_enemyPos = _enemyPos; _enemyPos < _enemysNumber[0]+ _enemysNumber[1]; _enemyPos++)
        {
            GameObject _enemyPrefab = _poolPhase2.ReturnObj();
            _enemyPrefab.transform.position = _position[_enemyPos].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
        }
    }

    private void EnemyPhase3()
    {
        for (_enemyPos = _enemyPos; _enemyPos < _enemysNumber[0] + _enemysNumber[1]+ _enemysNumber[2]; _enemyPos++)
        {
            GameObject _enemyPrefab = _poolPhase3.ReturnObj();
            _enemyPrefab.transform.position = _position[_enemyPos].transform.position;
            _enemyPrefab.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            _enemyList.Add(_enemyPrefab);
        }
    }

    public void Removed(GameObject obj){
        _enemyList.Remove(obj);
    }

    private void Parent() {
        _parent = _parentBase.transform.GetChild(0).gameObject;
        _parent2 = _parentBase.transform.GetChild(1).gameObject;
        _parent3 = _parentBase.transform.GetChild(2).gameObject;
        _parent4 = _parentBase.transform.GetChild(3).gameObject;
    }
}

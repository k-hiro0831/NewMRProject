using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemControl : MonoBehaviour{

    [SerializeField, Header("�G�̃t�F�[�Y��ԊǗ�")]
    EnemyPhase enemyPhase = EnemyPhase.phase1;

    [SerializeField,Header("�e��ނ̓G")]
    private GameObject[] _enemys = new GameObject[3];
    [SerializeField, Header("�G�̐����ʒu")]
    private GameObject[] _position = new GameObject[3];
    [SerializeField, Header("�G�̃t�F�[�Y�ʂ̐�")]
    private int[] _enemysNumber = new int[0];
    [SerializeField, Header("�G�Ɏg�p����I�u�W�F�N�g")]
    private List<GameObject> _enemyList = new List<GameObject>();
    [SerializeField, Header("�e�I�u�W�F�N�g�̑�{�ɂȂ���")]
    private GameObject _parentBase;
    private GameObject _parent;
    private GameObject _parent2;
    private GameObject _parent3;
    private GameObject _parent4;
    [SerializeField, Header("�G�t�F�[�Y�̃e�L�X�g")]
    private Text _PhaseText;

    /// <summary>
    /// �G�����ʒu
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
    /// �G�̃t�F�[�Y��ԊǗ�
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
        //�G�̃t�F�[�Y��ԊǗ�����
        switch (enemyPhase) {
            case EnemyPhase.phase1:
                //���t�F�[�Y�̓G���S�ł�����
                if (_enemyList.Count == 0) {
                    //�G�𐶐����Ă���e�L�X�g��ύX���đ��t�F�[�Y�ֈڍs
                    EnemyPhase2();
                    _PhaseText.text = "�G�t�F�[�Y:2";
                    enemyPhase = EnemyPhase.phase2;
                }
                break;
            case EnemyPhase.phase2:
                //���t�F�[�Y�̓G���S�ł�����
                if (_enemyList.Count == 0)
                {
                    //�G�𐶐����Ă���e�L�X�g��ύX���đ�O�t�F�[�Y�ֈڍs
                    EnemyPhase3();
                    _PhaseText.text = "�G�t�F�[�Y:3";
                    enemyPhase = EnemyPhase.phase3;
                }
                break;
            case EnemyPhase.phase3:
                //���t�F�[�Y�̓G���S�ł�����
                if (_enemyList.Count == 0)
                {
                    //�G�𐶐����Ă���e�L�X�g��ύX���đ�O�t�F�[�Y�ֈڍs
                    _PhaseText.text = "�G�t�F�[�Y:4(���݂����܂�)";
                    enemyPhase = EnemyPhase.phase4;
                }
                break;
            case EnemyPhase.phase4:
                break;
        }
    }

    private void StartMethod() {
        //�e�I�u�W�F�N�g�Ɋe�G��o�^
        Parent();
        //�ŏ��ɃI�u�W�F�N�g������Ă���
        _poolPhase1.Pool(_parent, _enemys[0], _enemysNumber[0]);
        _poolPhase2.Pool(_parent2, _enemys[1], _enemysNumber[1]);
        _poolPhase3.Pool(_parent3, _enemys[2], _enemysNumber[2]);
        //���t�F�[�Y�̓G�𐶐�
        EnemyPhase1();
        //�e�L�X�g��ύX
        _PhaseText.text = "�G�t�F�[�Y:1";
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

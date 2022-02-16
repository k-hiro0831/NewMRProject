using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private AudioSource _damageSound = null;
    [SerializeField]
    private GameObject _damageUI;
    private PlayerDamageEffect _damageScript;

    private bool _hit = false;
    private bool _atk = false;
    private bool _rec = false;
    [SerializeField]
    private GameObject _hissatsu;

    private bool _hissatsuNow;

    /// <summary>
    /// プレイヤーHP外部参照元
    /// </summary>
    private float _playerHitPoint = 100;
    /// <summary>
    /// プレイヤーHP外部参照用
    /// </summary>
    public float _playerHp{
        get { return _playerHitPoint; }
    }
    [SerializeField]
    private HpGauge _hpGauge;

    private void Start()
    {
       _damageScript = _damageUI.GetComponent<PlayerDamageEffect>();
    }

    private void Update()
    {
        //ここ呼び出すと必殺技
        if (Input.GetKeyDown(KeyCode.P) && !_hissatsuNow)
        {
            _hissatsu.gameObject.SetActive(true);
            _hissatsuNow = true;
            Invoke("Col", 0.5f);
            Invoke("Col2", 3.0f);
        }
    }

    private void Col()
    {
        _hissatsu.GetComponent<Collider>().enabled = true;
    }

    private void Col2()
    {
        _hissatsuNow = false;
        _hissatsu.GetComponent<Collider>().enabled = false;
        _hissatsu.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other){
        if (other.gameObject.tag == "EnemyAtk")
        {
            //Atk();
        }
    }

    public void Recovery()
    {
        if (!_rec)
        {
            _rec = true;
            _hpGauge.PlusHp(20);
            Invoke("Rec", 2.0f);
        }
    }

    public void Atk(int _value)
    {
        if (!_hit)
        {
            _hit = true;
            _hpGauge.MinusHp(_value);
            _damageSound.Play();
            _damageScript.StartEffect();
            Invoke("Hit", 2.0f);
        }
    }

    private void HPDown(){
        _playerHitPoint = _playerHitPoint - 1;
    }

    private void Hit(){
        _hit = false;
    }

    private void Rec()
    {
        _rec = false;
    }
}

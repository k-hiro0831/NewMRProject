using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _bulleds = new GameObject[1];
    [SerializeField]
    private GameObject _bulletRay;

    private Vector3 _right = new Vector3(0.02f, 0.0f, 0.0f);
    private Vector3 _left = new Vector3(-0.02f, 0.0f, 0.0f);

    //武器切替（テスト用）
    [SerializeField]
    private Text _weapon;
    private enum Atk {
        pistol,syuriken
    }
    Atk atk = Atk.pistol;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("AutoBullet");
        _weapon.text = "銃";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("right"))
        {
            transform.Rotate(0, 0.2f, 0);
        }

        if (Input.GetKey("left"))
        {
            transform.Rotate(0, -0.2f, 0);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position += _left;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position += _right;
        }

        switch (atk) {
            case Atk.pistol:
                if (Input.GetKeyDown(KeyCode.Q)) {
                    _weapon.text = "手裏剣";
                    atk = Atk.syuriken;
                }
                break;
            case Atk.syuriken:
                if (Input.GetKeyDown(KeyCode.Q)){
                    _weapon.text = "銃";
                    atk = Atk.syuriken;
                }
                break;
        }
    }

    private IEnumerator AutoBullet() {

        while (true)
        {
            yield return new WaitForSeconds(1);
            if (atk == Atk.pistol){
                BulletInstantiate();
            }
            if (atk == Atk.syuriken) {
                SyurikenInstantiate();
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy") {

        }
    }

    private void BulletInstantiate()
    {
        Instantiate(_bulleds[0], _bulletRay.transform.position, transform.rotation);
    }

    private void SyurikenInstantiate()
    {
        Instantiate(_bulleds[1], _bulletRay.transform.position, transform.rotation);
    }
}

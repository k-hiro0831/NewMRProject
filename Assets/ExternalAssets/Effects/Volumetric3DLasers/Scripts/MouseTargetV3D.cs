using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTargetV3D : MonoBehaviour {

    public Transform targetCursor;
    public float speed = 1f;

    private Vector3 mouseWorldPosition;

    private GameObject _player;

    private Vector3 _pos;

    bool _atk;

    bool _check;

    bool _atkBool;

    private Player _playerSc;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("MainCamera");
        _playerSc = _player.GetComponent<Player>();
    }

    public void Atk(bool atk)
    {
        _atk = atk;
    }

    // Positioning cursor prefab
    void FixedUpdate () {

        if (_atk && !_check)
        {
            _pos = _player.transform.position;
            _check = true;
        }

        if (!_atk)
        {
            _check = false;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            mouseWorldPosition = hit.point;
        }

        //Quaternion toRotation = Quaternion.LookRotation(mouseWorldPosition - transform.position);
        //transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        //targetCursor.position = mouseWorldPosition;

        Quaternion toRotation = Quaternion.LookRotation(_pos - transform.position);
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, speed * Time.deltaTime);
        targetCursor.position = _pos;

        if (!_atkBool && _check)
        {
            _atkBool = true;
            Invoke("AtkBoolReset", 3.0f);
            _playerSc.Atk(7);
        }
        

        //float disA = Vector3.Distance(_pos, particles[i].position);
        //float disB = Vector3.Distance(_player.transform.position, _pos);

        //if (disA < 0.9f && disB < 0.9f && !_atkBool)
        //{
        //    _atkBool = true;
        //    //_playerSc.Atk(10);
            
        //}

    }

    private void AtkBoolReset()
    {
        _atkBool = false;
    }
}

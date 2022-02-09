using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> _pool = new List<GameObject>();
    private int _currentCount;
    private int _max;
    private Vector3 _originPosition;
    private Vector3 _originScale;

    private GameObject _parent;
    private GameObject _prefab;

    public void Pool(GameObject newParent,GameObject obj,int number) {
        int count = number;
        _parent = newParent;
        _prefab = obj;
        for (int i = 0 ; i < count; i++)
        {
            GameObject go = GameObject.Instantiate(_prefab, _originPosition, Quaternion.identity) as GameObject;
            go.transform.SetParent(_parent.transform, false);
            go.SetActive(false);
            _pool.Add(go);
        }
    }

    public GameObject ReturnObj()
    {
        foreach (GameObject obj in _pool)
        {
            if (obj.activeSelf == false)
            {

                obj.SetActive(true);
                //obj.GetComponent<NavMeshAgent>().enabled = true;
                return obj;
            }
        }

        GameObject newObj = GameObject.Instantiate(_prefab, _originPosition, Quaternion.identity) as GameObject;
        newObj.transform.SetParent(_parent.transform, false);
        newObj.SetActive(false);
        _pool.Add(newObj);

        return newObj;
    }

    public GameObject ReturnObjEnemy()
    {
        foreach (GameObject obj in _pool) {
            if (obj.activeSelf == false) {
                
                obj.SetActive(true);
                //obj.GetComponent<NavMeshAgent>().enabled = true;
                return obj;
            }
        }

        GameObject newObj = GameObject.Instantiate(_prefab, _originPosition, Quaternion.identity) as GameObject;
        newObj.transform.SetParent(_parent.transform, false);
        newObj.SetActive(false);
        _pool.Add(newObj);

        return newObj;
    }
}

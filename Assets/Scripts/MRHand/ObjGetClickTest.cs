using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjGetClickTest : MonoBehaviour
{

    [SerializeField]
    private GameObject TestObj = default;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void SetObject(GameObject obj)
    {
        print("�I�u�W�F�N�g�Z�b�g");
        print("�I�u�W�F�N�g" + obj);
        TestObj = obj;
    }
}

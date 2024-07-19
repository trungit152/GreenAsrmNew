using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test1 : MonoBehaviour
{
    public GameObject pref;
    public List<GameObject> listTest;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            int a = listTest.Count - 1;
            GameObject obj = listTest[a];
            GameObject objClone = Instantiate(pref, new Vector3(0, 5, 0), Quaternion.identity);
            objClone.GetComponent<HingeJoint2D>().connectedBody = obj.GetComponent<Rigidbody2D>();
            objClone.transform.SetParent(transform);
            listTest.Add(objClone);
            //objClone.transform.parent = null;
        }
    }
}

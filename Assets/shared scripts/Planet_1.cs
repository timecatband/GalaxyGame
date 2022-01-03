using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet_1 : MonoBehaviour
{
    public GameObject terrain; 
    // Start is called before the first frame update
    void gravity()
    {
        Physics.gravity = new Vector3(0, -1f, 0);
    }
    void Start()
    {
        terrain = GameObject.Find("Terrain");
        gravity();
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}

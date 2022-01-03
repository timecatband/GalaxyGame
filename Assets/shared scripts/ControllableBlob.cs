using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableBlob : MonoBehaviour
{
    Rigidbody mRigidbody;
    public float mThrust = .0001f;
    // Start is called before the first frame update
    void Start()
    {
        mRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            mRigidbody.MovePosition(transform.position + (transform.up * mThrust));
        }
    }
}

using UnityEngine;
using System.Collections;

public class RotaterMotion : MonoBehaviour {

    public float speed = 12f;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Quaternion temp = transform.rotation;
        temp = Quaternion.Euler(0, 0, speed * Time.deltaTime);
        rb.MoveRotation(Quaternion.Slerp(transform.rotation, temp * transform.rotation, 4));
        //rb.MoveRotation(temp * transform.rotation);
	}
}

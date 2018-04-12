using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour {

    private GameObject tank;

	// Use this for initialization
	void Start () {
        tank = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

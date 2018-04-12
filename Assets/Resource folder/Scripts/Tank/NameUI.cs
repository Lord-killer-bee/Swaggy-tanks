using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NameUI : MonoBehaviour {
    private Transform cam;
    private NetworkManager nm;
	// Use this for initialization
	void OnEnable () {
        cam = GameObject.FindGameObjectWithTag("MainCamera").transform;
        nm = GameObject.FindGameObjectWithTag("Network manager").GetComponent<NetworkManager>();
        gameObject.GetComponent<Text>().text = nm.playerName;
    }
	
	// Update is called once per frame
	void Update () {
	    gameObject.GetComponent<RectTransform>().LookAt(transform.position - cam.transform.position );
	}
}

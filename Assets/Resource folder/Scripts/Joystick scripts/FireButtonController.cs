using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FireButtonController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {

    [HideInInspector]
    public bool buttonClicked;

	// Use this for initialization
	void Start () {
	
	}
	
    public void OnPointerDown(PointerEventData ped)
    {
        buttonClicked = true;

    }

    public void OnPointerUp(PointerEventData ped)
    {
        buttonClicked = false;

    }


}

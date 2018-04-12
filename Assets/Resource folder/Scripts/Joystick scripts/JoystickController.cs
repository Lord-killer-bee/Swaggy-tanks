using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickController : MonoBehaviour,IPointerDownHandler,IPointerUpHandler,IDragHandler {

    private Image bgImage;
    private Image jsKnob;


    [HideInInspector]public Vector3 inputVector;

    void Start()
    {
        bgImage = GetComponent<Image>();
        jsKnob = transform.GetChild(0).GetComponent<Image>();
    }

   


    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector3.zero;
        jsKnob.rectTransform.anchoredPosition = Vector2.zero;

    }

    public void OnDrag(PointerEventData ped)
    {
        Vector2 pos;

        if(RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImage.rectTransform
                                                                    ,ped.position
                                                                    ,ped.pressEventCamera
                                                                    ,out pos))
        {
            pos.x = pos.x / bgImage.rectTransform.rect.width;
            pos.y = pos.y / bgImage.rectTransform.rect.height;

            inputVector = new Vector3(pos.x * 2 - 1,pos.y * 2 - 1);
            inputVector = inputVector.magnitude < 1 ?  inputVector : inputVector.normalized;

            jsKnob.rectTransform.anchoredPosition = new Vector2(inputVector.x * bgImage.rectTransform.rect.width / 3
                                                                ,inputVector.y * bgImage.rectTransform.rect.height / 3);


        }
                                                                    

    }


}

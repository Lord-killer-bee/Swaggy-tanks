using UnityEngine;
using System.Collections;

public class CameraAnims : MonoBehaviour {

    public Animation anim;
    public AnimationClip clip1;
    public AnimationClip clip2;
    public Transform transformpoint2;
    public Transform transformpoint3;
    public GameObject rotater;
    private bool waited;
    private bool stopped;

    // Use this for initialization
    void Start()
    {
        anim.Play(clip1.name);
    }
	// Update is called once per frame
	void Update (){

        DelayAtEnd(clip1.name);

        if (!anim.IsPlaying(clip1.name)) { 
            if(waited == false && stopped == false)
            {
                gameObject.transform.position = transformpoint2.position;
                gameObject.transform.rotation = transformpoint2.rotation;
                if(anim.isPlaying == false)
                {
                    stopped = true;
                }
                anim.Play(clip2.name);
            }

            DelayAtEnd(clip2.name);
            if (!anim.isPlaying)
            {
                gameObject.transform.position = transformpoint3.position + new Vector3(0, 3, 0);
            }

        }
        if (!anim.IsPlaying(clip1.name) && !anim.IsPlaying(clip2.name)) {
            gameObject.transform.SetParent(rotater.transform);
            gameObject.transform.localPosition = new Vector3(0, 3, 0);
            
            gameObject.transform.LookAt(rotater.transform);
   

        }


        }


    void DelayAtEnd(string clip)
    {
        if (anim[clip].normalizedTime > 0.7f && anim[clip].normalizedTime < 1f)
        {
            anim[clip].speed = 0.1f;
        }
    }
    
}

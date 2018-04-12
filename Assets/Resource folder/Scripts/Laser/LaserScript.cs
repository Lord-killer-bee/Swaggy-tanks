using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour {

    private LineRenderer lr;
    private Transform laserTransform;
    private TankShooting ts;

    private ParticleSystem laserEffects;


	// Use this for initialization
	void Start () {
        lr = gameObject.GetComponent<LineRenderer>();
        laserEffects = gameObject.GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        //its important to enable the laser upon switching to laser mode.
        ts = GameObject.FindGameObjectWithTag("Player").GetComponent<TankShooting>();
        laserTransform = ts.m_FireTransform;
    }
	
	// Update is called once per frame
	void Update () {
        if(ts.laserFired == true)
        {
           
            laserEffects.Play();
          
                RaycastHit hit;
                if (Physics.Raycast(ts.transform.position,ts.transform.forward, out hit))
                {
                    if (hit.collider)
                    {
                        lr.SetPosition(1, new Vector3(0, 0, hit.distance));
                    }
                    else
                    {
                        lr.SetPosition(1, new Vector3(0, 0, 2000));
                    }
                }
            
        }
	}
}

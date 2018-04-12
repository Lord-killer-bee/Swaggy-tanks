using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
    private TankShooting ts;

	void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            ts = collision.gameObject.GetComponent<TankShooting>();
            ts.laserAmmo++;
        }
    }

}

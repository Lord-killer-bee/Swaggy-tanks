using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {


    private TankShooting ts;

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Player")
        {
            ts = target.gameObject.GetComponent<TankShooting>();
            ts.ammo = 20;
        }
    }
}

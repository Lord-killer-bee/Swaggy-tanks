using UnityEngine;
using System.Collections;

public class Nitro : MonoBehaviour {

    private TankMovement tm;

	void OnCollisionEnter(Collision target)
    {
        if(target.gameObject.tag == "Player")
        {
            tm = target.gameObject.GetComponent<TankMovement>();
            tm.nitro = true;
            tm.speed = 20f;
        }
    }
}

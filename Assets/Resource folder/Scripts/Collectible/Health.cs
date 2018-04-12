using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    private TankHealth th;
    private float HpBonus = 25;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision target)
    {
        
        if (target.gameObject.tag == "Player")
        {
            Debug.Log("collided");
            th = target.gameObject.GetComponent<TankHealth>();

            if(th.health > 100 - HpBonus)
            {
                th.health = 100;
                th.healthSlider.value = th.health;

            }else
            {
                th.health += HpBonus;
                th.healthSlider.value = th.health;
            }

        }
    }
}

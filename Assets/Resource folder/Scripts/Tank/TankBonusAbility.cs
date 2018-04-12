using UnityEngine;
using System.Collections;

public class TankBonusAbility : MonoBehaviour {

    private TankData td;

    int tempLevel = 0;
    int abilityIndex = 0;

    // Use this for initialization
    void Start () {
        td = GetComponent<TankData>();
	}
	
	// Update is called once per frame
	void Update () {
        CheckLevel();
	
	}

    void CheckLevel()
    {

        if (tempLevel < td.currentLevel)
        {
            abilityIndex = Random.Range(1, 3);
            ActivateAbility(abilityIndex);
        }
        tempLevel = td.currentLevel;
    }

    void ActivateAbility(int index)
    {
        switch (index)
        {
            case 1:
                //health
                gameObject.GetComponent<TankHealth>().health = 100;
                Debug.Log("Health acquired");
                break;

            case 2:
                //nitro
                if (gameObject.GetComponent<TankMovement>().nitro == false)
                {
                    gameObject.GetComponent<TankMovement>().speed = 20f;
                    gameObject.GetComponent<TankMovement>().nitro = true;
                    Debug.Log("nitro acquired");
                }
                else
                {
                    ActivateAbility(Random.Range(1, 3));
                }
                break;


            case 3:
                //ammo
                gameObject.GetComponent<TankShooting>().ammo = 20;
                Debug.Log("ammo acquired");
                break;
        }
    }

}

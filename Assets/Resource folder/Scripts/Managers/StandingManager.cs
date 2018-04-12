using UnityEngine;
using System.Collections;

public class StandingManager : MonoBehaviour {

    public static StandingManager instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    [HideInInspector]public int healthStanding = 5;
    [HideInInspector]public int ammoStanding = 5;
    [HideInInspector]public int nitroStanding = 10;
    [HideInInspector]public int laserStanding = 15;
    [HideInInspector]public int nukeStanding = 25;
    [HideInInspector]public int killStanding = 50;
    [HideInInspector]public int deathStanding = 25;

    [HideInInspector]public int baseLevelStanding = 50;
    [HideInInspector]public int standingIncrease = 25;
   
}

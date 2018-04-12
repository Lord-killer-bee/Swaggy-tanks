using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class AmmoSwitcher : Photon.MonoBehaviour,IPointerClickHandler {

    private Button switchingButton;
    public TankShooting ts;
    public int ammoCode = 0;

    [SerializeField]
    private Text ammoText;


    // Use this for initialization
    void Start () {
        switchingButton = gameObject.GetComponent<Button>();
	}
	

	// Update is called once per frame
	void Update () {
	


	}

    public void OnPointerClick(PointerEventData ped)
    {

        if (ammoCode == 1 && ts.laserAlreadyFired == true)
        {
            //do not execute switch
        }
        else {
            SwitchAmmo();
        }
    }

    void SwitchAmmo()
    {
        ammoCode++;
        Debug.Log(ammoCode);
        if(ammoCode >= 3)
        {
            ammoCode = 0;
        }

        switch (ammoCode)
        {
            case 0:
                //regular ammo
                ammoText.text = "Ammo: " + ts.ammo.ToString() + " / 20";
                ts.ammoFired = true;
                ts.laserFired = false;
                ts.nukeFired = false;
                break;

            case 1:
                //laser ammo
                ammoText.text = "Laser Ammo: " + ts.laserAmmo;
                ts.ammoFired = false;
                ts.laserFired = true;
                ts.nukeFired = false;

                break;

            case 2:
                //nuke ammo
                ammoText.text = "Nuke Ammo: " + ts.nukeAmmo.ToString();
                ts.ammoFired = false;
                ts.laserFired = false;
                ts.nukeFired = true;
                break;
        }
    }

}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TankShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public GameObject m_Shell;
    public GameObject nuke;
    public GameObject laser;
    public Transform m_FireTransform;
    public Slider m_AimSlider;
    public AudioSource m_ShootingAudio;
    public AudioClip m_ChargingClip;
    public AudioClip m_FireClip;
    public AudioClip DryAmmo;
    public float m_MinLaunchForce = 15f;
    public float m_MaxLaunchForce = 30f;
    public float m_MaxChargeTime = 0.75f;

    public int ammo = 20;
    public int nukeAmmo = 0;
    public int laserAmmo = 0;

    private AmmoSwitcher aS;

    //private string m_FireButton;   
    private FireButtonController fireButton;
    private float m_CurrentLaunchForce;
    private float m_ChargeSpeed;
    public bool m_Fired, firedWithMax;
    public bool ammoFired, laserFired, nukeFired;
    public bool laserAlreadyFired = false;
    public float laserTimer = 5f;

    private void OnEnable()
    {
        ammoFired = true;
        m_CurrentLaunchForce = m_MinLaunchForce;
        m_AimSlider.value = m_MinLaunchForce;
        aS = GameObject.FindGameObjectWithTag("Ammo switcher").GetComponent<AmmoSwitcher>();
        aS.ts = gameObject.GetComponent<TankShooting>();

    }


    private void Start()
    {
        //m_FireButton = "Fire" + m_PlayerNumber;
        fireButton = GameObject.Find("Fire button").GetComponent<FireButtonController>();
        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime;

    }


    private void Update()
    {
        if(laserAlreadyFired == true)
        {
            laserTimer -= 0.01f;
            //Debug.Log(laserTimer);
            if (laserTimer < 0)
            {
                laserFired = false;
                laserAlreadyFired = false;
                laserTimer = 5f;
                Destroy(GameObject.FindGameObjectWithTag("laser beam"));
            }
        }

        if ((ammo <= 0 && ammoFired == true) || (nukeAmmo <= 0 && nukeFired == true)|| (laserAmmo <= 0 && laserFired == true))
        {
            if (ammo <= 0 && ammoFired == true && fireButton.buttonClicked == true)
            {
                //play dry audio
                fireButton.buttonClicked = false;
                m_ShootingAudio.clip = DryAmmo;
                m_ShootingAudio.Play();
            }
            if (nukeAmmo <= 0 && nukeFired == true && fireButton.buttonClicked == true)
            {
                //play dry audio
                fireButton.buttonClicked = false;
                m_ShootingAudio.clip = DryAmmo;
                m_ShootingAudio.Play();
            }
            if (laserAmmo <= 0 && laserFired == true && fireButton.buttonClicked == true)
            {
                //play dry audio
                fireButton.buttonClicked = false;
                m_ShootingAudio.clip = DryAmmo;
                m_ShootingAudio.Play();
            }
        }
        
        else
        {

            if (fireButton.buttonClicked == true && m_CurrentLaunchForce >= m_MaxLaunchForce && firedWithMax == false)
            {
                //fire the shot when max is reached & set firewithmax to be true.
                m_CurrentLaunchForce = m_MaxLaunchForce;
                Fire();
                //gameObject.GetPhotonView().RPC("FirePN", PhotonTargets.Others, m_FireTransform.position, m_FireTransform.rotation,m_FireTransform.forward, m_CurrentLaunchForce);
                m_Fired = false;
                firedWithMax = true;

            }

            else if (fireButton.buttonClicked == true && m_Fired == false)
            {
                //the button is clicked and shot not fired.
                m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;
                m_AimSlider.value = m_CurrentLaunchForce;

                if (m_ShootingAudio.clip != m_ChargingClip)
                {
                    m_ShootingAudio.clip = m_ChargingClip;
                    m_ShootingAudio.Play();
                }
                if (m_CurrentLaunchForce >= m_MaxLaunchForce)
                {
                    m_CurrentLaunchForce = m_MaxLaunchForce;
                    Fire();
                    //gameObject.GetPhotonView().RPC("FirePN", PhotonTargets.Others, m_FireTransform.position,m_FireTransform.rotation, m_FireTransform.forward, m_CurrentLaunchForce);
                    m_Fired = false;
                    firedWithMax = true;
                }

            }

            else if (m_CurrentLaunchForce > m_MinLaunchForce && fireButton.buttonClicked == false)
            {
                //button released, so fire the shots.
                Fire();
                //gameObject.GetPhotonView().RPC("FirePN", PhotonTargets.Others, m_FireTransform.position, m_FireTransform.rotation, m_FireTransform.forward, m_CurrentLaunchForce);
                m_Fired = false;
                firedWithMax = false;

            }

        }
    }


    private void Fire()
    {

      

        if (nukeFired == true)
        {
            nukeFired = false;
            nukeAmmo--;
            GameObject nukeInstance = Instantiate(nuke, m_FireTransform.position, m_FireTransform.rotation) as GameObject;
            nukeInstance.name = gameObject.GetPhotonView().viewID.ToString();
            nukeInstance.GetComponent<Rigidbody>().velocity = m_CurrentLaunchForce * m_FireTransform.forward;

            gameObject.GetPhotonView().RPC("FirePN", PhotonTargets.Others, m_FireTransform.position, m_FireTransform.rotation, m_FireTransform.forward, m_CurrentLaunchForce);

            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();

            m_CurrentLaunchForce = m_MinLaunchForce;
            m_AimSlider.value = m_CurrentLaunchForce;
            fireButton.buttonClicked = false;
            
        }

        else if(ammoFired == true)
        {
            ammo--;
            // Instantiate and launch the shell.
            m_Fired = true;
            GameObject shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as GameObject;
            shellInstance.name = gameObject.GetPhotonView().viewID.ToString();
            shellInstance.GetComponent<Rigidbody>().velocity = m_CurrentLaunchForce * m_FireTransform.forward;

            gameObject.GetPhotonView().RPC("FirePN", PhotonTargets.Others, m_FireTransform.position, m_FireTransform.rotation, m_FireTransform.forward, m_CurrentLaunchForce);

            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();

            m_CurrentLaunchForce = m_MinLaunchForce;
            m_AimSlider.value = m_CurrentLaunchForce;
            fireButton.buttonClicked = false;
        }
        else if(laserFired == true && laserAlreadyFired == false)
        {
            laserAmmo--;
            Debug.Log(laserAmmo);
            GameObject laserInstance = Instantiate(laser, m_FireTransform.position, m_FireTransform.rotation) as GameObject;
            laserInstance.transform.SetParent(gameObject.transform);
            laserAlreadyFired = true;
            m_CurrentLaunchForce = m_MinLaunchForce;
            m_AimSlider.value = m_CurrentLaunchForce;
            fireButton.buttonClicked = false;
        }
       

    }

    [PunRPC]
    public void FirePN(Vector3 firePoint,Quaternion fireRotation, Vector3 forward, float launchForce)
    {
        GameObject shellInstance = Instantiate(m_Shell, firePoint, fireRotation) as GameObject;
        shellInstance.GetComponent<Rigidbody>().velocity = launchForce * forward;


        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }

    [PunRPC]
    public void FireNukePN(Vector3 firePoint, Quaternion fireRotation, Vector3 forward, float launchForce)
    {
        GameObject nukeInstance = Instantiate(nuke, firePoint, fireRotation) as GameObject;
        nukeInstance.GetComponent<Rigidbody>().velocity = launchForce * forward;

        m_ShootingAudio.clip = m_FireClip;
        m_ShootingAudio.Play();
    }


}

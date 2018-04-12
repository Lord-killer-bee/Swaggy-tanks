using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour {

    public float health = 100f;
    public Slider healthSlider;

    [SerializeField]
    private ParticleSystem tankExplosion;

    [SerializeField]private Image fill;
    private Color minColor = Color.green;
    private Color maxColor = Color.red;

    private bool hit = false;

	// Use this for initialization
	void Start () {
        healthSlider.value = health;
	}
	
    void Update()
    {
        if(hit == true)
        {
            //gameObject.GetPhotonView().RPC("SliderSync", PhotonTargets.OthersBuffered, healthSlider.value);
            hit = false;
        }

        if(healthSlider.value <= 0)
        {
            
            if (GetComponent<TankData>().myStanding > 25)
            {
                GetComponent<TankData>().myStanding -= StandingManager.instance.deathStanding;
            }
            else
            {
                GetComponent<TankData>().myStanding = 0;
            }
            gameObject.GetPhotonView().RPC("DestroyTank", PhotonTargets.AllBuffered);
        }

    }

    [PunRPC]
	public void ApplyDamage(float damage)
    {
        hit = true;
        healthSlider.value = healthSlider.value - damage;
		health -= damage;
        fill.color = Color.Lerp(minColor, maxColor, 15 * Time.deltaTime);
    }

    [PunRPC]
    public void DestroyTank()
    {
        tankExplosion.Play();
        tankExplosion.transform.SetParent(null);
        Destroy(gameObject);
    }

   /* [PunRPC]
    public void SliderSync(int value)
    {
        healthSlider.value = value;
        fill.color = Color.Lerp(minColor, maxColor, 15 * Time.deltaTime);
    }*/
}

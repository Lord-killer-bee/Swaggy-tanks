using UnityEngine;
using System.Collections;
using System;

public class ShellExplosion : MonoBehaviour {

    private TankHealth tankHealth;

    [SerializeField]
    private ParticleSystem explosionShell;


    public float explosionRadius = 5f;
    public float explosiveForce = 3000f;

    [SerializeField]
    private AudioSource explosionSound;

    private GameObject owner;
    private float damageBuff;

    void OnEnable()
    {
        tankHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<TankHealth>();
    }

    void OnCollisionEnter(Collision target)
    {
        

        explosionSound.Play();
        Collider[] targets = Physics.OverlapSphere(transform.position, explosionRadius);

        

        explosionShell.Play();
        
        explosionShell.transform.SetParent(null);
        

        for (int i = 0; i < targets.Length; i++)
        {

            if(targets[i].gameObject.tag == "Player")
            {
                Vector3 distance = transform.position - targets[i].transform.position;

                float temp = distance.magnitude;
                temp = temp / explosionRadius;
                    if (temp > 1)
                    {
                        temp = 1f;
                    }

                if (targets[i].gameObject.tag == "Player")
                {
                    targets[i].GetComponent<Rigidbody>().AddExplosionForce(explosiveForce, transform.position, explosionRadius);
                   
                    targets[i].gameObject.GetPhotonView().RPC("ApplyDamage", PhotonTargets.AllBuffered, DamageBuff((temp * tankHealth.health) / 3));
        
                    if(targets[i].gameObject.GetComponent<TankHealth>().health <= 0)
                    {
						Debug.Log ("id sent");
                        GameObject.Find("Gameplay manager").GetPhotonView().RPC("SendIDofShell", PhotonTargets.MasterClient,int.Parse(gameObject.name));
                        targets[i].gameObject.GetPhotonView().RPC("DestroyTank", PhotonTargets.AllBuffered);
                    }
                }
                else
                {
                    targets[i].GetComponent<Rigidbody>().AddExplosionForce((1 - temp) * explosiveForce, transform.position, explosionRadius);
                    targets[i].gameObject.GetPhotonView().RPC("ApplyDamage", PhotonTargets.AllBuffered, DamageBuff(((1 - temp) * tankHealth.health)/3));
                    if (targets[i].gameObject.GetComponent<TankHealth>().health <= 0)
                    {
						Debug.Log ("id sent");
                        GameObject.Find("Gameplay manager").GetPhotonView().RPC("SendIDofShell", PhotonTargets.MasterClient, int.Parse(gameObject.name));
                        targets[i].gameObject.GetPhotonView().RPC("DestroyTank", PhotonTargets.AllBuffered);
                    }
                }
            }
        }
        //gameObject.SetActive (false);
        Destroy(GameObject.Find("ShellExplosion"), 1f);
        Destroy(gameObject);
        
    }

    float DamageBuff(float damage)
    {
        int id;
        if(int.TryParse(gameObject.name,out id))
        {
            owner = PhotonView.Find(id).gameObject;
        }
        else
        {
            return 0;
        }

        damageBuff = (owner.GetComponent<TankData>().currentLevel) * (damage * 0.05f);

        damage += damageBuff;

        return damage;
    }
}

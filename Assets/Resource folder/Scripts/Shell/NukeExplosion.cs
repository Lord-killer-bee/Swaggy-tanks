using UnityEngine;
using System.Collections;

public class NukeExplosion : MonoBehaviour {
    private TankHealth th;

    [SerializeField]
    private ParticleSystem nukeExplosionShell;


    public float nukeExplosionRadius = 10f;
    public float nukeExplosiveForce = 20000f;

    [SerializeField]
    private AudioSource nukeExplosionSound;

    void OnEnable()
    {
        th = GameObject.FindGameObjectWithTag("Player").GetComponent<TankHealth>();
    }

    void OnCollisionEnter(Collision target)
    {
        nukeExplosionSound.Play();
        Collider[] targets = Physics.OverlapSphere(transform.position, nukeExplosionRadius);



        nukeExplosionShell.Play();

        nukeExplosionShell.transform.SetParent(null);
        Destroy(gameObject);

        for (int i = 0; i < targets.Length; i++)
        {

            if (targets[i].gameObject.tag == "Player")
            {
                Vector3 distance = transform.position - targets[i].transform.position;

                float temp = distance.magnitude;
                temp = temp / nukeExplosionRadius;
                if (temp > 1)
                {
                    temp = 1f;
                }

                if (targets[i].gameObject.tag == "Player")
                {
                    targets[i].GetComponent<Rigidbody>().AddExplosionForce(nukeExplosiveForce, transform.position, nukeExplosionRadius);
                    targets[i].gameObject.GetPhotonView().RPC("ApplyDamage", PhotonTargets.AllBuffered, temp * th.health);
                }
                else
                {
                    targets[i].GetComponent<Rigidbody>().AddExplosionForce((1 - temp) * nukeExplosiveForce, transform.position, nukeExplosionRadius);
                    targets[i].gameObject.GetPhotonView().RPC("ApplyDamage", PhotonTargets.AllBuffered, (1 - temp) * th.health);
                }
            }
        }
        Destroy(GameObject.Find("Nuke Explosion"), 1f);
    }
}

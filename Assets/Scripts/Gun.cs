using UnityEngine;
using UnityEngine.AI;

public class Gun : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource source;

    public GameObject explosion;
    public float damage = 10f;
    public float range = 100f;
    public Camera camera;
    NavMeshAgent nav;

    void FixedUpdate()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot() 
    {
        
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();

            if (target != null)
            {
                target.TakeDamage(damage);
            }

            Instantiate(explosion, hit.point, Quaternion.LookRotation(hit.normal));

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                source.Play();
            }
        }

        
    }
}

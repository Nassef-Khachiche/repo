using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class Target : MonoBehaviour
{
    [SerializeField] public bool rush = false;

    public AudioClip clip;
    public AudioSource source;

    public GameObject robot;
    public GameObject player;
    public GameObject hostage;

    bool dead = false;
    bool isHit = false;

    public float health = 40f;
    public ParticleSystem blood;

    // Follow player
    public bool inRange;
    public Transform target;
    NavMeshAgent nav;

    void Start()
    {
        robot.GetComponent<Animator>().Play("Z_Idle");
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (rush == true)
        {
            isHit = true;
        }


        if (isHit == true && dead != true)
        {
            nav.SetDestination(target.position);
            robot.GetComponent<Animator>().Play("Z_Run");
        }
        else
        {
            nav.SetDestination(transform.position);
        }
    }

    public void TakeDamage(float amount) 
    {
        health -= amount;
        isHit = true;
        if (dead != true)
        {
            blood.Play();
            robot.GetComponent<Animator>().Play("Z_Run");
        }

        if (health <= 10f)
        {
            dead = true;
            isHit = false;
            robot.GetComponent<Animator>().Play("Z_FallingForward");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player" && dead != true)
        {
            source.Play();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}

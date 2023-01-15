using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Camera_Movement : MonoBehaviour
{
    // Hostage
    [SerializeField] public GameObject Hostage_Walking;
    [SerializeField] public GameObject Hostage_Sitting;
    [SerializeField] public GameObject Jeep;
    [SerializeField] public GameObject Army;
    NavMeshAgent nav;

    public AudioClip clip;
    public AudioSource source;

    //sd is movement speed
    private float MoveX, MoveZ;
    public float sd;
    public float JumpForce;
    public bool IsGrounded;
    public Rigidbody rb;

    //ms is mouse speed
    public int ms;
    public Transform cam;
    private float xRotation = 0f;

    void Start()
    {
        Hostage_Walking.SetActive(false);
        Jeep.SetActive(false);
        Army.SetActive(false);

        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
        // Walk
        float mouseX = Input.GetAxis("Mouse X") * ms * 10 * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * ms * 10 * Time.deltaTime;

        // Rotate the player relevant to the mouse rotational Y
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -25f, 25f);

        // Look Up or Down (Camera)
        cam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        this.gameObject.transform.Rotate(Vector3.up * mouseX);

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        MoveX = Input.GetAxis("Horizontal");
        MoveZ = Input.GetAxis("Vertical");

        rb.MovePosition(transform.position + (transform.forward * MoveZ * sd / 2) + (transform.right * MoveX * sd / 2));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Hostage_Sitting")
        {
            source.Play();

            Hostage_Walking.SetActive(true);
            Hostage_Sitting.SetActive(false);

            // Escape
            Jeep.SetActive(true);
            Army.SetActive(true);
        }

        if (collision.transform.tag == "Jeep" )
        {
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }
    }
}

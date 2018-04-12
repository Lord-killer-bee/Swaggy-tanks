using UnityEngine;
using System.Collections;

public class TankMovement : MonoBehaviour {

    public float speed = 12f;

    [HideInInspector]
    public bool nitro;

    [SerializeField]
    private float nitroTimer = 10f;

    private JoystickController joystickController;

    [SerializeField]
    private AudioSource drivingAudioSource;

    [SerializeField]
    private AudioClip idleClip;

    [SerializeField]
    private AudioClip drivingClip;

    private Rigidbody tank;

    // Use this for initialization
    void Start () {
        tank = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        Move();
	
	}

    void OnEnable()
    {
        //this is to attach the joystick script to the tank when the tank is spawned.
        //when instantiating through PUN view IDs change and external objects do not get referenced.
        //so a manual assignment is followed here.
        joystickController = GameObject.FindGameObjectWithTag("Joystick").GetComponent<JoystickController>();
    }

    void Update()
    {
        if(nitro == true)
        {
            nitroTimer -= Time.deltaTime;
            if(nitroTimer <= 0)
            {
                nitro = false;
                nitroTimer = 10f;
                speed = 12f;
            }
        }

        if(joystickController.inputVector.magnitude > 0.1f)
        {
            if(drivingAudioSource.clip != drivingClip)
            {
                drivingAudioSource.clip = drivingClip;
                drivingAudioSource.Play();
            }
        }
        else if (drivingAudioSource.clip != idleClip)
        {
            drivingAudioSource.clip = idleClip;
            drivingAudioSource.Play();
        }
    }

    void Move()
    {
        if(joystickController.inputVector.magnitude > 0)
        {
            Vector3 position = new Vector3(joystickController.inputVector.x * speed * Time.deltaTime,
                                           0,
                                           joystickController.inputVector.y * speed * Time.deltaTime);
            Quaternion newRotation = Quaternion.LookRotation(position);
            tank.MovePosition(transform.position + position);
            tank.transform.rotation = Quaternion.Slerp(tank.transform.rotation, newRotation, Time.deltaTime * 8);


        }
  

    }







}

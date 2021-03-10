using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    Rigidbody playerRigidbody;
    float speed = 3f;
    float rotationSpeed = 3f;
    public Camera cam;
    float cameraRotationLimit = 85f;
    float currentCameraRotationX = 0f;
    public AudioSource walkingSound;


    // Use this for initialization
    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState=CursorLockMode.Locked;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");

        Vector3 _movHorizontal = transform.right * _xMov;
        Vector3 _movVertical = transform.forward * _zMov;


        Vector3 velocity = (_movHorizontal + _movVertical).normalized * speed;
        Move(velocity);

        //Drehung für Gameobject horizontal
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * rotationSpeed;
        Rotate(_rotation);

        //Kamera drehung
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * rotationSpeed;
        CameraRotation(_cameraRotationX);       

        if (Input.GetButtonDown("Fire2"))
           BroadcastMessage("DropHanditem");
        
    }

    private void Move(Vector3 _velocity)
    {
        if (_velocity != Vector3.zero)
        {
           
            playerRigidbody.MovePosition(transform.position + _velocity * Time.fixedDeltaTime);

            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }

            
        }
        else
        {
            walkingSound.Stop();
        }
    }

    private void Rotate(Vector3 _rotation)
    {
        playerRigidbody.MoveRotation(playerRigidbody.rotation * Quaternion.Euler(_rotation));
    }

    private void CameraRotation(float _rotation)
    {
        if (cam != null)
        {
            currentCameraRotationX -= _rotation;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            cam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
        }
    }

}
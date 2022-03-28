using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PlayerController))]
[RequireComponent (typeof (GunController))]
public class Player : LivingEntity
{

    public float moveSpeed = 5;

    public Crosshair crosshair;

    Camera viewCamera;
    PlayerController controller;
    GunController gunController;

    // Start is called before the first frame update
    protected override void Start()
    { 
        // call the Start on the LivingEntity
        base.Start(); 

        controller = GetComponent<PlayerController>();
        gunController = GetComponent<GunController>();
        viewCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement
        Vector3 moveInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw("Vertical")); //Use GetAxisRaw insteed GetAxis avoid default smoothing 
        Vector3 moveVelocity = moveInput.normalized * moveSpeed; // Normalize the moveInput 
        controller.Move(moveVelocity);

        // Look to the cursor
        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.up * gunController.GunHeight); 
        float rayDistance;

        // Calculate mouse position
        if(groundPlane.Raycast(ray, out rayDistance)){
            Vector3 point = ray.GetPoint(rayDistance);
            
            //Debug.DrawLine(ray.origin, point, Color.red);

            controller.LookAt(point);
            crosshair.transform.position = point;
            crosshair.DetectTarget(ray);
        }

        // Weapon
        if(Input.GetMouseButton(0))
        {
            gunController.Shoot();
        }
    }
}

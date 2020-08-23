using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 direction;
    public float forwardSpeed;

    public float maxSpeed;
    private CharacterController controller;

    // se imparte ground in 3:
    private int lane = 1; // dc culoarul este 0-> stanga, 1-> mijloc, 2-> dreapta
    public float laneDistance = 4; // distanta dintre 2 culoare

    // variabile folosite pt jump
    public float jumpForce;
    public float Gravity = -20;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // cresterea treptata a vitezei
        if (forwardSpeed < maxSpeed)
            forwardSpeed += 0.1f * Time.deltaTime;

        direction.z = forwardSpeed;

        // pt ca personajul sa sara

        if (controller.isGrounded) // verificare daca personajul se gaseste pe ground 
                                    // daca da, atunci poate sari
        {
            //direction.y = -2;
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
        else
        {
            direction.y += Gravity * Time.deltaTime; // adaug gravitatea
        }
        

        // in functie de input, verificam pe ce culoar trebuie sa mearga
        if (Input.GetKeyDown(KeyCode.RightArrow)) // dreapta
        {
            lane++;
            if (lane == 3)
                lane = 2;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow)) // stanga
        {
            lane--;
            if (lane == -1)
                lane = 0;
        }

        // calc unde ar trebui sa se afle in viitor
        Vector3 futurePosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lane == 0)
        {
            futurePosition += Vector3.left * laneDistance;
        }
        else if(lane == 2)
        {
            futurePosition += Vector3.right * laneDistance;
        }

        //transform.position = Vector3.Lerp(transform.position,futurePosition,80 * Time.deltaTime); // smoothness

        // rezolvarea problemei coliziunilor (pt a nu mai trece prin obiecte)
        if (transform.position != futurePosition)
        {
            Vector3 dif = futurePosition - transform.position;
            Vector3 moveDirection = dif.normalized * 25 * Time.deltaTime;
            if (moveDirection.sqrMagnitude < dif.sqrMagnitude)
                controller.Move(moveDirection);
            else
                controller.Move(dif);
        }
 
        controller.Move(direction * Time.deltaTime);
    }


    private void Jump()
    {
        direction.y = jumpForce;
    }

    // verifcare: ce loveste personajul
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.CompareTag("Obstacol"))
        {
            PlayerManager.gameOver = true;
        }
    }
}

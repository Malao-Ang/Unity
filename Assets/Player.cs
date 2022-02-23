using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController characterController;
    Animator animator;
    public float speed = 6.0f;
    private Vector3 moveDirection;
    Rigidbody rigidbody;
    private int desiredLane = 1; //0 left 1 midle 2 right
    public float laneDistance = 2.5f;
    public float jumpFroce;
    public float Gravity;
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(! PlayerManager.isGameStarted)
            return;
        moveDirection.z = speed;
        if(characterController.isGrounded){
            moveDirection.y = -1;
            if(SwipeManager.swipeUp){
            Jump();
            }

        }
        else{
            moveDirection.y += Gravity*Time.deltaTime;
        }
        animator.SetBool("Run", true);
        if (SwipeManager.swipeRight)
        {
            desiredLane++;
            if (desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        if (SwipeManager.swipeLeft)
        {
            desiredLane--;
            if (desiredLane == -1)
            {
                desiredLane = 0;
            }
        }
        //calulate
        Vector3 targetPosition =( transform.position.z * transform.forward) +( transform.position.y * transform.up);
        if (desiredLane == 0)
        {
            targetPosition += (Vector3.left * laneDistance);
        }
        else if (desiredLane == 2)
        {
            targetPosition += (Vector3.right * laneDistance);
        }

        //transform.position = Vector3.Lerp(transform.position, targetPosition, 80 * Time.deltaTime);
        if(transform.position == targetPosition){
            return;
        }
        Vector3 diff = targetPosition - targetPosition;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if(moveDir.sqrMagnitude < diff.sqrMagnitude){
            characterController.Move(moveDir);
        }
        else{
            characterController.Move(diff);
        }
        

        
    }
    private void FixedUpdate()
    {
        if(! PlayerManager.isGameStarted)
            return;
        characterController.Move(moveDirection * Time.fixedDeltaTime);
    }

    private void Jump(){
        moveDirection.y = jumpFroce;
        }

    private void OnControllerColliderHit(ControllerColliderHit hit){
        if(hit.transform.tag == "Obstacle"){
            PlayerManager.gameOver = true;
        }
    }



}


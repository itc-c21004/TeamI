using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;
    const int DefaultLine = 3;
    //const float StunDuration = 0.5f;


    CharacterController controller;
   // Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;
    //int life = DefaultLine;
    //float recoverTime = 0.0f;


    public float gravity;
    public float speedZ;
    public float speedX;
    //public float speedJump;
    public float accelerationZ;

    

    //Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //デバッグ
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        //if (Input.GetKeyDown("space")) Jump();

       
        else
        {
            //徐々に加速しZ方向に常に前進
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            //X方向は目標のポジションまでの差分の割合で速度を計算
            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;
        }


        moveDirection.y -= gravity * Time.deltaTime;

        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        if (controller.isGrounded) moveDirection.y = 0;

        //animator.SetBool("run", moveDirection.z > 0.0f);
    }

    //左のレーンに移動を開始
    public void MoveToLeft()
    {
        
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }
    //右のレーンに移動を開始
    public void MoveToRight()
    {
       
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }





}

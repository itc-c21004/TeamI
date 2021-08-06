using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = +2;
    const float LaneWidth = 1.0f;

    CharacterController controller;


    Vector3 moveDirection = Vector3.zero;
    int targetLane;

    public float gravity;
    public float speedZ;
    public float speedX;
    public float accelerrationZ;
    //public float speedJump;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //Z方向に常に全身させる。
        float acceleratedZ = moveDirection.z + (accelerrationZ * Time.deltaTime);
        moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

        //Z方向は目標のポジションまでの差分を計算
        float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedX;

        //重力文の力を毎フレーム追加
        moveDirection.y -= gravity * Time.deltaTime;

        //移動実行
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        //移動後に設置していたらY方向の速度はリセットする。
        if (controller.isGrounded) moveDirection.y = 0;
        
    }

    //左のレーンに移動
    public void MoveToLeft()
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }

    //右のレーンに移動
    public void MoveToRight()
    {
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }

}
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
        //Z�����ɏ�ɑS�g������B
        float acceleratedZ = moveDirection.z + (accelerrationZ * Time.deltaTime);
        moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

        //Z�����͖ڕW�̃|�W�V�����܂ł̍������v�Z
        float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
        moveDirection.x = ratioX * speedX;

        //�d�͕��̗͂𖈃t���[���ǉ�
        moveDirection.y -= gravity * Time.deltaTime;

        //�ړ����s
        Vector3 globalDirection = transform.TransformDirection(moveDirection);
        controller.Move(globalDirection * Time.deltaTime);

        //�ړ���ɐݒu���Ă�����Y�����̑��x�̓��Z�b�g����B
        if (controller.isGrounded) moveDirection.y = 0;
        
    }

    //���̃��[���Ɉړ�
    public void MoveToLeft()
    {
        if (controller.isGrounded && targetLane > MinLane) targetLane--;
    }

    //�E�̃��[���Ɉړ�
    public void MoveToRight()
    {
        if (controller.isGrounded && targetLane < MaxLane) targetLane++;
    }

}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace littleDog
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovement : MonoBehaviour
    {

        private CharacterController controller;
        public float speed = 12f;
        public float gravity = -9.81f;
        public float JumpHight = 3;
        public Boolean jumping = false;
         Vector3 V;
        private void Awake()
        {
            controller = gameObject.GetComponent<CharacterController>();
        }

        // Update is called once per frame
        void Update()
        {
             if (MouseLook.CanMove == false) return;
            if (controller.isGrounded && V.y < 0)
            {
                V.y = -2f;
                jumping = false;
            }
            float X = Input.GetAxis("Horizontal");
            float Z = Input.GetAxis("Vertical");
            Vector3 M = transform.right * X + transform.forward * Z;
            controller.Move(M * speed * Time.deltaTime);
            if (Input.GetButtonDown("Jump") && !jumping)
            {
                V.y = Mathf.Sqrt(JumpHight * -2f * gravity);
                jumping = true;
            }
            V.y += gravity * Time.deltaTime;
            controller.Move(V * Time.deltaTime);


        }

    }
}

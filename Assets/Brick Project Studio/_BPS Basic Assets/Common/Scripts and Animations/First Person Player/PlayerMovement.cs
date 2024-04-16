using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SojaExiles

{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private GameObject playerSound;
        private AudioSource playerAudioSource;

        public CharacterController controller;

        public float speed = 5f;
        public float gravity = -15f;

        private bool activated = true;

        private Vector3 velocity;

        private bool isGrounded;
        private bool isMoving;


        private void Awake()
        {
            playerAudioSource = playerSound.GetComponent<AudioSource>();
        }

        // Update is called once per frame
        void Update()
        {

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            if (activated)
            {
                Vector3 move = transform.right * x + transform.forward * z;

                controller.Move(move * speed * Time.deltaTime);

                velocity.y += gravity * Time.deltaTime;

                controller.Move(velocity * Time.deltaTime);

                if (!isMoving && move != Vector3.zero)
                {
                    isMoving = true;
                    playerAudioSource.Play();
                }
                else if (isMoving && move == Vector3.zero)
                {
                    isMoving = false;
                    playerAudioSource.Stop();
                }
            }

        }
        public void ToggleMovement()
        {
            activated = !activated;
        }
    }

}
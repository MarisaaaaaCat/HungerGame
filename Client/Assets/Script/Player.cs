using System.Collections;
using UnityEngine;

namespace Script
{
    public class Player : MonoBehaviour
    {
        private Camera mainCamera;

        private Rigidbody _rigidbody;
        private float x;
        private float y;
        [SerializeField] private float speed=0.1f;
        [SerializeField] private float jumpSpeed = .1f;
        [SerializeField] private bool ground;
        void Start()
        {
            if (mainCamera == null)
            {
                mainCamera = Camera.main;
                mainCamera.transform.position = this.transform.position + Vector3.back * 10 + Vector3.up * 5f;
            }

            this._rigidbody = this.GetComponent<Rigidbody>();
        }


        void Update()
        {
            if (ground)
            {
                x = Input.GetAxis("Horizontal");
                y = Input.GetAxis("Vertical");
                Vector3 offset = new Vector3(x, 0f, y)*speed;
                this.transform.position += offset;  
            }
           
          
            mainCamera.transform.LookAt(this.transform);
        }

        void FixedUpdate()
        {
            transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        if (Input.GetKey(KeyCode.Space))
        {
                if (ground)
                {   
                    transform.Translate(new Vector3(x, 0f,y)*speed);
                    this._rigidbody.velocity += new Vector3(0, 5, 0);
                    this._rigidbody.AddForce(Vector3.up * jumpSpeed);
                    ground = false;
                   
                }
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            this.ground = true;
            other.gameObject.GetComponent<MeshRenderer>().material.color=Color.yellow;
        }

        private void OnCollisionExit(Collision other)
        {
            other.gameObject.GetComponent<MeshRenderer>().material.color=Color.white;
        }
    }
}
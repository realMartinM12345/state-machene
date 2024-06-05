using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 input;
    private bool isGrounded;
    public LayerMask groundMask;
    public Camera camera;
    [SerializeField] private float groundedAllowance = 0.05f;
    [SerializeField] private Animator animator;

    public float speed = 5f;



    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (animator == null)
        {
            Debug.LogError("don't forget to set animator for your player");
        }
        rb = GetComponent<Rigidbody>();

        if (camera == null )
        {
            camera = Camera.main;
        }
    }
    // Update is called once per frame
    void Update()
    {
        input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        input = camera.transform.TransformDirection(input);
        if (input.magnitude > 1f)
        {
            input.Normalize();
        }

        if (input.magnitude > 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }



            float inputMag = input.magnitude;
        input.y = 0f;
        input.Normalize();
        input *= inputMag;

       







        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);
            Debug.Log("jumped");
        }


        IsGroundedCheck();

        if (input.magnitude > 0f)
        {
            Quaternion rotation = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
        }
    }

    private bool IsGroundedCheck()
    {
        if (Physics.SphereCast(transform.position, 0.5f, Vector3.down, out RaycastHit hit, (1 / 2f) + groundedAllowance, groundMask))
        {

            return isGrounded = true;


        }
        return isGrounded = false;
    }


private void FixedUpdate()
    {
       
        rb.MovePosition(transform.position + input * speed * Time.deltaTime);

        Physics.Raycast(transform.position, -transform.up, 1.05f, groundMask);
    }
}

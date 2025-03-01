using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{


    public float MoveSpeed = 10f;
    //private float SprintSpeed = 20f;
    public float JumpForce = 8f;

    private Rigidbody rb;

    private Vector3 MoveDirections;
    private bool isGrounded = true;

    private int JumpCount = 3;
    public Transform SafeLocation;

    public GameObject GameOverUi;

    private bool isInvincible = false;
    private Collider playerCollider;

    public GameObject InvisUi;
    public TextMeshProUGUI JumpsUi;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    private void Update()
    {
        //HandleJump();
        JumpsUi.text = JumpCount.ToString();
    }

    private void FixedUpdate()
    {
        HandleMovements();

    }

    public void HandleMovements()
    {
        float XMove = Input.GetAxis("Horizontal");
        float ZMove = Input.GetAxis("Vertical");

        MoveDirections = new Vector3(XMove, 0, ZMove).normalized;

       // float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? SprintSpeed : MoveSpeed;

        rb.MovePosition(rb.position + MoveDirections * MoveSpeed * Time.fixedDeltaTime);
    }

   /* public void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && JumpCount > 0)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Asteroid"))
        {
            if (JumpCount <= 0)
            {
                Time.timeScale = 0f;
                GameOverUi.SetActive(true);
            }
            else
            {
                JumpCount--;
                gameObject.transform.position = SafeLocation.position;
                
            }
        }


    }


    public void ActivateInvincibility()
    {
        if (!isInvincible)
        {
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    private IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;
        playerCollider.isTrigger = true;
        InvisUi.SetActive(true);
        yield return new WaitForSeconds(5f);

        playerCollider.isTrigger = false;
        isInvincible = false;
        InvisUi.SetActive(false);
    }
}

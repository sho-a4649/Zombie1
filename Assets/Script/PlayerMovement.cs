using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 5f; // 通常速度
    public float sprintSpeed = 8f; // ダッシュ速度

    [Header("Jump")]
    public float jumpHeight = 2f; // ジャンプの高さ
    public float gravity = -20f; // 重力

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.2f; // 接地当たり判定
    public LayerMask groundMask;

    public CrosshairController crosshair;

    private CharacterController controller;
    private Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        GroundCheck();
        Move();
        Jump();
        ApplyGravity();
    }

    void GroundCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        float targetJumpSpread = !isGrounded ? 30f : 0f;

        crosshair.jumpSpread = Mathf.MoveTowards(crosshair.jumpSpread, targetJumpSpread, 40f * Time.deltaTime);

        /*if (!isGrounded)
        {
            crosshair.jumpSpread = 30f;
        }
        else
        {
            crosshair.jumpSpread = 0f;
        }*/
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;

        controller.Move(move.normalized * speed * Time.deltaTime);
    }

    void Jump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void ApplyGravity()
    {
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}

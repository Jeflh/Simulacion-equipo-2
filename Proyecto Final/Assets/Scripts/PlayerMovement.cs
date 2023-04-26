using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float Speed = 3f;
    private Rigidbody2D PlayerRigidbody;
    private Animator PlayerAnimator;

    private float MoveX, MoveY;
    private Vector2 MoveInput;

    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        // Inputs
        MoveX = Input.GetAxisRaw("Horizontal");
        MoveY = Input.GetAxisRaw("Vertical");
        MoveInput = new Vector2(MoveX, MoveY).normalized;

        PlayerAnimator.SetFloat("Horizontal", MoveX);
        PlayerAnimator.SetFloat("Vertical", MoveY);
        PlayerAnimator.SetFloat("Speed", MoveInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        // Físicas
        PlayerRigidbody.MovePosition(PlayerRigidbody.position + MoveInput * Speed * Time.fixedDeltaTime);
    }
}

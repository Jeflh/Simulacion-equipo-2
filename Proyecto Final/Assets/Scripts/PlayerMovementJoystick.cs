using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementJoystick : MonoBehaviour
{

    [SerializeField] private float Speed = 3f;
    private Rigidbody2D PlayerRigidbody;
    private Animator PlayerAnimator;

    public GameObject MagicPrefab;
    private float LastShoot;

    private float MoveX, MoveY;
    private Vector2 MoveInput;

    public Joystick joystick;

    void Start()
    {
        PlayerRigidbody = GetComponent<Rigidbody2D>();
        PlayerAnimator = GetComponent<Animator>();
    }


    void Update()
    {
        MoveX = Mathf.Round(joystick.Horizontal);
        MoveY = Mathf.Round(joystick.Vertical);

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

    public void ShootButton()
    {
        if (Time.time > LastShoot + 0.50f)
        {
            Shoot();
            LastShoot = Time.time;
        }
    }

    private void Shoot()
    {
        Vector3 direction = MoveInput;
        int rotation = 0;

        if (PlayerAnimator.GetFloat("Speed") < 0.001f)
        {
            direction = new Vector3(0, -1).normalized;
            rotation = -90;
        }

        Debug.Log(MoveInput);
        Debug.Log(MoveX);
        Debug.Log(MoveY);

        if (MoveX == 1 && MoveY == 0) rotation = 0;
        
        else if (MoveX == -1 && MoveY == 0) rotation = 180;
 
        else if (MoveX == 0 && MoveY == 1) rotation = 90;

        else if (MoveX == 0 && MoveY == -1) rotation = -90;

        else if (MoveX == -1 && MoveY == 1) rotation = 140;

        else if (MoveX == 1 && MoveY == 1) rotation = 40;
        
        else if (MoveX == -1 && MoveY == -1) rotation = -140;
        
        else if (MoveX == 1 && MoveY == -1) rotation = -40;
        
        GameObject Magic = Instantiate(MagicPrefab, transform.position + direction * 0.50f, Quaternion.Euler(0, 0, rotation));
        Magic.GetComponent<MagicScript>().SetDirection(direction);
    }
}

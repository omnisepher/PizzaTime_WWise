using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float jumpForce = 0.0f;
    [SerializeField]
    private float speed = 0.0f;
    [SerializeField]
    private float rotationSpeed = 0.0f;
    [SerializeField]
    private GameObject pizza;

    private float BaseSpeedMultiplier = 1;

    private Rigidbody rb;
    private PlayerControlMap playerMap;
    private Animator anim;

    public bool givingPizza = false;
    private float tempRotationSpeed;
    private bool possiblePeople = false;
    private bool canJump = true;

    private float jumpTimer = 0f;
    private float maxJumpTime = 2f;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        if (SceneManager.GetActiveScene().name == "LevelOne")
        {
            anim.SetBool("Dance", false);
        }
        else if (SceneManager.GetActiveScene().name == "MainScreen")
        {
            anim.SetBool("Dance", true);
        }

        pizza.SetActive(false);

        if (!(SceneManager.GetActiveScene().name == "MainScreen"))
        {
            playerMap = new PlayerControlMap();
            playerMap.Basic.Enable();
            playerMap.Basic.Jump.performed += Jump;
            playerMap.Basic.Give.performed += GivePizza;
        }

        tempRotationSpeed = rotationSpeed;
        jumpTimer = maxJumpTime;
    }
    private void Update()
    {
        MoveHandle();
        RotationHandle();
        jumpTimer = jumpTimer > maxJumpTime ? maxJumpTime : jumpTimer + Time.deltaTime;
        if (jumpTimer >= maxJumpTime)
        {
            jumpTimer = maxJumpTime;
            canJump = true;
        }
        else
        {
            jumpTimer += Time.deltaTime;
            canJump = false;
        }
    }

    private void GivePizza(InputAction.CallbackContext context)
    {
        if (!givingPizza && possiblePeople && !GameManager.levelLose && !GameManager.levelClear)
        {
            rotationSpeed = 0;
            pizza.SetActive(true);
            anim.SetTrigger("Give");
            givingPizza = true;

            GameManager.AddTime();
        }
    }

    private void GivePizzaEnd()
    {
        givingPizza = false;
        rotationSpeed = tempRotationSpeed;
        pizza.SetActive(false);
        anim.SetFloat("SpeedMultiplier", BaseSpeedMultiplier);
    }

    private void MoveHandle()
    {
        Vector2 movement = playerMap.Basic.Movement.ReadValue<Vector2>();
        anim.SetFloat("Speed", movement.y);
        if (movement.y > 0)
        {
            anim.SetFloat("SpeedMultiplier", anim.GetFloat("SpeedMultiplier") + 0.1f * Time.deltaTime > 2.0f ? 2.0f : anim.GetFloat("SpeedMultiplier") + 0.1f * Time.deltaTime);
        }
        else
        {
            anim.SetFloat("SpeedMultiplier", BaseSpeedMultiplier);
        }
    }

    private void RotationHandle()
    {
        Vector2 movement = playerMap.Basic.Movement.ReadValue<Vector2>();
        transform.Rotate(0, movement.x * rotationSpeed * Time.deltaTime, 0, Space.Self);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (canJump && !GameManager.levelLose && !GameManager.levelClear)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetTrigger("Jump");
            jumpTimer = 0f;
        }
    }

    public void JumpEnd()
    {
        canJump = true;
        anim.SetFloat("SpeedMultiplier", BaseSpeedMultiplier);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("People"))
        {
            PeopleBase people = other.gameObject.GetComponent<PeopleBase>();
            if (!people.fed)
            {
                possiblePeople = true;
            }
            else
            {
                possiblePeople = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("People"))
        {
            possiblePeople = false;
        }

    }

}

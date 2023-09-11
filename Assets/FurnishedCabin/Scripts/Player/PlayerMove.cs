using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName = "Horizontal";
    [SerializeField] private string verticalInputName = "Vertical";

    [SerializeField] private float movementSpeed = 2f;

    private CharacterController charController;
    private Rigidbody Rigidbody;


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        PlayerMovement();
        Pular();
    }

    private void FixedUpdate()
    {
        Rigidbody.AddForce(new Vector3(0, 100, 0), ForceMode.Impulse);
    }

    private void PlayerMovement()
    {
        var movimentoAtual = movementSpeed;

        if (Input.GetKey(KeyCode.LeftShift))
            movimentoAtual += 2f;

        float vertInput = Input.GetAxis(verticalInputName) * movimentoAtual;
        float horizInput = Input.GetAxis(horizontalInputName) * movimentoAtual;

        Vector3 forwardMovement = transform.forward * vertInput;
        Vector3 rightMovement = transform.right * horizInput;

        //simple move applies delta time automatically
        charController.SimpleMove(forwardMovement + rightMovement);
    }

    private void Pular()
    {
        if (Input.GetKeyDown(KeyCode.A))
            Rigidbody.AddForce(Vector3.up * 5);
    }
}
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoviment : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moviment;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }


    public void SetMoviment(InputAction.CallbackContext value)
    {
        moviment = value.ReadValue<Vector2>();
    }

    public void SetInteract(InputAction.CallbackContext value)
    {
        // interact 
    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(moviment.x, 0, moviment.y) * Time.fixedDeltaTime * 200);
    }
}

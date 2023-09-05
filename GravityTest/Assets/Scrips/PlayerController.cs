using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    public float _playerSpeed;
    public Vector2 _playerPosition;
    public float _playerRotation;
    private Animator _playerAnim;

    // Start is called before the first frame update
    void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();    
        _playerAnim = gameObject.transform.GetChild(0).GetComponent<Animator>(); 
    }

    // Update is called once per frame
    void Update()
    {
        _playerPosition = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(_playerPosition.sqrMagnitude > 0)
        {
            _playerAnim.SetInteger("Moviment", 1);
            
        }
        else
        {
            _playerAnim.SetInteger("Moviment", 0);
        }
    }
    private void FixedUpdate()
    {
        _rb2d.MovePosition(_rb2d.position + _playerPosition * _playerSpeed * Time.fixedDeltaTime);

    }
}

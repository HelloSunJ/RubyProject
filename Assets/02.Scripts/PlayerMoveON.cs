using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveON : MonoBehaviour
{
    public Vector2 MoveInput;
    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnMove(InputValue value)
    {
        MoveInput = value.Get<Vector2>();        
    }
}

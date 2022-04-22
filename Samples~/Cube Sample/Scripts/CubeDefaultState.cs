using StateMachine.Core;
using UnityEngine;

public class CubeDefaultState : CharacterStateBase
{
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.B)) //B for both
        {
            Move<MoveAndRotateState>();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            Move<MoveState>();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Move<RotateState>();
        }
    }
}
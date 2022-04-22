using StateMachine.Core;
using StateMachine.Core.Attributes;
using UnityEngine;

[AllowAbilities(typeof(MoveAbility), typeof(RotateAbility))]
public class MoveAndRotateState : CharacterStateBase
{
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Quit();
        }
    }
}
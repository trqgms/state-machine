using StateMachine.Core;
using StateMachine.Core.Attributes;
using UnityEngine;

[AllowAbilities(typeof(MoveAbility))]
public class MoveState : CharacterStateBase
{
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Quit();
        }
    }
}
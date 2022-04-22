using StateMachine.Core;
using StateMachine.Core.Attributes;
using UnityEngine;

[AllowAbilities(typeof(RotateAbility))]
public class RotateState : CharacterStateBase
{
    public override void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Quit();
        }
    }
}
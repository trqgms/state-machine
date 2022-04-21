using System;
using System.Collections;
using System.Collections.Generic;
using StateMachine.Core;
using UnityEngine;

public class CubeStateMachine : StateMachineBase
{
    [SerializeField] private CubeData cubeData;

    protected override void CreateAbilities()
    {
        DeclareAbility(new RotateAbility(gameObject, cubeData));
        DeclareAbility(new MoveAbility(gameObject, cubeData));
    }

    protected override void DeclareStates()
    {
        CreateState<CubeDefaultState>();
        CreateState<RotateState>();
        CreateState<MoveState>();
        CreateState<MoveAndRotateState>();
    }

    protected override Type DefaultState => typeof(CubeDefaultState);
}
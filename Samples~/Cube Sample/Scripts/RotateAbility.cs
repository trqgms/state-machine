using StateMachine.Abilities;
using UnityEngine;

public class RotateAbility : StateAbility
{
    private readonly GameObject _context;
    private readonly CubeData _data;

    public RotateAbility(GameObject context, CubeData data)
    {
        _context = context;
        _data = data;
    }

    public override void Update()
    {
        _context.transform.Rotate(0, _data.rotationSpeed * Time.deltaTime, 0);
    }
}
using StateMachine.Abilities;
using UnityEngine;

public class MoveAbility : StateAbility
{
    private readonly GameObject _context;
    private readonly CubeData _data;
    private Vector3 _direction = Vector3.right;

    public MoveAbility(GameObject context, CubeData data)
    {
        _context = context;
        _data = data;
    }

    public override void Enter()
    {
        _direction = Random.insideUnitSphere;
        _direction.y = 0;
        _direction.Normalize();
    }

    public override void Update()
    {
        if (Mathf.Abs(_context.transform.position.x) > _data.movementLenght)
        {
            _direction.x = -Mathf.Sign(_context.transform.position.x);
        }

        if (Mathf.Abs(_context.transform.position.z) > _data.movementLenght)
        {
            _direction.z = -Mathf.Sign(_context.transform.position.z);
        }


        _context.transform.position += Time.deltaTime * _direction * _data.movementSpeed;
    }
}
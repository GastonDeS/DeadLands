using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CmdJump : ICommand
{
    private IMovable _movable;
    private Vector3 _force = Vector3.up;

    public CmdJump(IMovable movable)
    {
        _movable = movable;
    }

    public void Execute() => _movable.Jump(_force);


}

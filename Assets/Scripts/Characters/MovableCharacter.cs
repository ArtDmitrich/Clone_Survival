using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableCharacter : Character
{
    protected IMovable Movement { get { return _movement = _movement ?? GetComponent<IMovable>(); } }
    private IMovable _movement;
}

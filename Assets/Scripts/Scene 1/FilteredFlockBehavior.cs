using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public abstract class FilteredFlockBehavior : FlockBehavior
{
    [SerializeField]
    public ContextFilter Filter;
}
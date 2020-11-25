using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FilteredFlockBehavior : FlockBehavior
{
    public ContextFilter Filter { get; set; }

    public FilteredFlockBehavior Init(ContextFilter filter = null)
    {
        Filter = filter;

        return this;
    }
}

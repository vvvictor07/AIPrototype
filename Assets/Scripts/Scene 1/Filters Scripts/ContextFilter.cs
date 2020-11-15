using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ContextFilter : ScriptableObject
{
    // Start is called before the first frame update

    public abstract List<Transform> Filter(FlockAgent agent, List<Transform> original);

}

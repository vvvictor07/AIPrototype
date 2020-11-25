using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask Mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        return original
            .Where(x => Mask == (Mask | (1 << x.gameObject.layer)))
            .ToList();
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        return original.Where(x => mask == (mask | (1 << x.gameObject.layer))).ToList();

        // List<Transform> filtered = new List<Transform>();
        // foreach (Transform item in original)
        // {
        //     if (mask == (mask | (1 << item.gameObject.layer)))
        //     {
        //         filtered.Add(item);
        //     }
        // }
        // return filtered;
    }
}

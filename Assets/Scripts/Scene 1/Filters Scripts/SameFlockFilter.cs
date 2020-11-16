using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        var filtered = new List<Transform>();

        foreach (var item in original)
        {
            var itemAgent = item.GetComponent<FlockAgent>();

            if (itemAgent != null)
            {
                // && itemAgent.AgentFlock == agent.AgentFlock)
                if (itemAgent.ParentFlock == agent.ParentFlock)
                {
                    filtered.Add(item);
                }
            }
        }

        return filtered;
    }
}
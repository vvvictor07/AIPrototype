using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.Scene_1.Filters_Scripts
{
    [CreateAssetMenu(menuName = "Flock/Filter/Different Flock")]
    public class DifferentFlockFilter : ContextFilter
    {
        public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
        {
            return original
                .Where(x => x.GetComponent<FlockAgent>() != null && x.GetComponent<FlockAgent>().ParentFlock != agent.ParentFlock)
                .ToList();
        }
    }
}

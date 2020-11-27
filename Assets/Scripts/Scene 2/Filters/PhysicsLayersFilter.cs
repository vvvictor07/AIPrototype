using System.Collections.Generic;
using System.Linq;

using Assets.Scripts.Scene_1;

using UnityEngine;

namespace Assets.Scripts.Scene_2.Filters
{
    [CreateAssetMenu(menuName = "Life/Filter/PhysicsLayers")]
    public class PhysicsLayersFilter : ContextFilterForLife
    {
        [SerializeField]
        private LayerMask mask;

        public override List<Transform> Filter(List<Transform> original, Life life = null)
        {
            return original
                .Where(x => mask == (mask | (1 << x.gameObject.layer)))
                .ToList();
        }
    }
}

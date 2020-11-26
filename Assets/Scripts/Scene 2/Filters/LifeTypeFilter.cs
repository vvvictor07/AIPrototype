using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace Assets.Scripts.Scene_2.Filters
{
    [CreateAssetMenu(menuName = "Life/Filter/Type")]
    public class LifeTypeFilter : ContextFilterForLife
    {
        [SerializeField]
        private bool sameType = true;

        public override List<Transform> Filter(List<Transform> original, Life life = null)
        {
            if (life == null)
            {
                return original;
            }

            return original
                .Where(x => x.GetComponent<Life>() != null && ((x.GetComponent<Life>().Type == life.Type) == sameType))
                .ToList();
        }
    }
}
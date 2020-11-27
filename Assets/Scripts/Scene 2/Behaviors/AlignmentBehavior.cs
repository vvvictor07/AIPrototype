using System.Collections.Generic;

using UnityEngine;

namespace Assets.Scripts.Scene_2.Behaviors
{
    [CreateAssetMenu(menuName = "Life/Behavior/Alignment")]
    public class AlignmentBehavior : LifeBehavior
    {
        public override Vector2 CalculateMoveSpeed(Life life, List<Transform> context)
        {
            if (context.Count == 0)
            {
                // TODO: the speed of the object is not taken into account, but only its direction with magnitude equals 1
                // maintain same direction
                return life.transform.up;
            }

            // add all directions together and average
            var alignmentMove = Vector2.zero;
            var filteredContext = Filter == null ? context : Filter.Filter(context, life);

            var count = 0;

            foreach (var item in filteredContext)
            {
                // instead of context
                if (Vector2.Distance(item.position, life.transform.position) <= life.Characteristics.SmallRadius)
                {
                    alignmentMove += (Vector2)item.transform.up;
                    count++;
                }
            }

            if (count != 0)
            {
                alignmentMove /= context.Count;
            }

            return alignmentMove;
        }
    }
}
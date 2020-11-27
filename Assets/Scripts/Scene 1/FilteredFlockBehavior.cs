using Assets.Scripts.Scene_1.Filters_Scripts;

using UnityEngine;

namespace Assets.Scripts.Scene_1
{
    public abstract class FilteredFlockBehavior : FlockBehavior
    {
        [SerializeField]
        public ContextFilter Filter;
    }
}
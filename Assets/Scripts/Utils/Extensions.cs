using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class Extensions {
        
        public static Transform GetClosest(this Transform transform, IEnumerable<Transform> others)
        {
            Transform tMin = null;
            var minDist = Mathf.Infinity;
            var currentPos = transform.position;
            foreach (var other in others)
            {
                var dist = Vector3.Distance(other.position, currentPos);
                if (!(dist < minDist)) continue;
                tMin = other;
                minDist = dist;
            }
            return tMin;
        }

    }
}

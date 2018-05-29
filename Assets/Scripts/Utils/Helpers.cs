using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public class Helpers {

        private struct OverlapStruct
        {
            public Vector2 Point;
            public Vector2 Size;
            public CapsuleDirection2D Direction;
            public float Angle;
        }

        public static IEnumerable<Collider2D> OverlapCapsuleAll(Vector2 point, float radius, float range, int direction)
        {
            var overlapStruct = GetOverlapStruct(radius, range, direction);
            return Physics2D.OverlapCapsuleAll(point + overlapStruct.Point, overlapStruct.Size, overlapStruct.Direction, overlapStruct.Angle);
        }

        public static Collider2D OverlapCapsule(Vector2 point, float radius, float range, int direction)
        {
            var overlapStruct = GetOverlapStruct(radius, range, direction);
            return Physics2D.OverlapCapsule(point + overlapStruct.Point, overlapStruct.Size, overlapStruct.Direction, overlapStruct.Angle);
        }

        private static OverlapStruct GetOverlapStruct(float radius, float range, int direction)
        {
            var res = new OverlapStruct
            {
                Point = new Vector2(),
                Size = new Vector2(),
                Direction = CapsuleDirection2D.Vertical,
                Angle = 0f
            };
            switch (direction)
            {
                case 0:
                    res.Point = new Vector2(0, range / 2);
                    res.Size = new Vector2(radius, range);
                    break;
                case 1:
                    res.Point = new Vector2(range / 2, 0);
                    res.Size = new Vector2(range, radius);
                    res.Direction = CapsuleDirection2D.Horizontal;
                    res.Angle = 90f;
                    break;
                case 2:
                    res.Point = new Vector2(0, -(range / 2));
                    res.Size = new Vector2(radius, range);
                    break;
                case 3:
                    res.Point = new Vector2(-(range / 2), 0);
                    res.Size = new Vector2(range, radius);
                    res.Direction = CapsuleDirection2D.Horizontal;
                    res.Angle = 90f;
                    break;
            }
            return res;
        }
        
    }
}

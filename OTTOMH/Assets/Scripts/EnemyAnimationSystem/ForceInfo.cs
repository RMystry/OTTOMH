using UnityEngine;

namespace GGJ
{
    public class ForceInfo
    {
        public Vector3 force;
        public Vector3 point;
        public Collider hitCollider;

        public ForceInfo(Vector3 force, Vector3 point, Collider hitCollider)
        {
            this.force = force;
            this.point = point;
            this.hitCollider = hitCollider;
        }
    }
}

using UnityEngine;

namespace Assets.Scripts.Interactive.Activable
{
    public class Door : Abstract.Activable
    {

        protected override void Unlock()
        {
            Debug.Log("Open door");
            // todo : remove collision and play animation
        }

    }
}

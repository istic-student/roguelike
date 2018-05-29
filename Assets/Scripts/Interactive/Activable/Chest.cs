using UnityEngine;

namespace Assets.Scripts.Interactive.Activable
{
    public class Chest : Abstract.Activable
    {
        
        private Container _container;

        public void Start()
        {
            _container = GetComponent<Container>();
        }

        protected override void Unlock()
        {
            Debug.Log("Open Chest");
            _container.DropAll();
            // todo : play animation
        }

    }
}

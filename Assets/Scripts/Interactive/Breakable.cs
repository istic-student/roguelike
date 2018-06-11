using Assets.Scripts.Abstract;

namespace Assets.Scripts.Interactive
{
    public class Breakable : Health
    {

        private Container _container;

        protected override void Init()
        {
            _container = GetComponent<Container>();
        }

        protected override void Die()
        {
            _container.DropAll();
            Destroy(gameObject);
        }

    }
}

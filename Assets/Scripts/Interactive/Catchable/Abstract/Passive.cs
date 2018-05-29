using Assets.Scripts.Abstract;
using Assets.Scripts.Character.Inventory;

namespace Assets.Scripts.Interactive.Catchable.Abstract
{
    public abstract class Passive : Interactive.Abstract.Catchable
    {

        protected Health Health;
        protected Inventory Inventory;

        public delegate void PassiveIsFinishedHandler(Passive passive);
        public event PassiveIsFinishedHandler PassiveIsFinished;

        public void Active()
        {
            Health = GetComponentInParent<Health>();
            Inventory = GetComponentInParent<Inventory>();
            Action();
        }

        protected abstract void Action();

        protected void OnFinish()
        {
            var handler = PassiveIsFinished;
            if (handler != null) handler(this);
        }

    }
}

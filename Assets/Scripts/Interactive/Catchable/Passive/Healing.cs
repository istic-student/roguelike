namespace Assets.Scripts.Interactive.Catchable.Passive
{
    public class Healing : Abstract.Passive
    {

        public float RestoredHealth = 5f;

        protected override void Action()
        {
            Health.AddHealth(RestoredHealth);
            OnFinish();
        }

    }
}

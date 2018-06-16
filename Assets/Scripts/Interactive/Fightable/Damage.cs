using Assets.Scripts.Abstract;
namespace Assets.Scripts.Interactive.Fightable
    
{
    public class Damage : Abstract.Fightable
    {

        protected Health Health;

        public void TakeDamage(int damage)
        {
            Health.TakeDamage(damage);
        }

    }
}
namespace ForestReturn.Scripts
{
    public interface IDamageable
    {
        public void TakeDamage(int damage);
        public void TakeDamage(int damage, bool ignoreIntangibility, float timeToReduceMoveSpeed);
    }
}
namespace Character
{
    [System.Serializable]
    public struct DataDamage
    {
        public int damage;
        public int trueDamage;

        public DataDamage(int damage,int trueDamage)
        {
            this.damage = damage;
            this.trueDamage = trueDamage;
        }
    }
}
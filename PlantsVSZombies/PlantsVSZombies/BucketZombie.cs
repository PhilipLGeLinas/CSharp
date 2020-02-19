using System;
namespace CptS487_HW4
{
    public class BucketZombie : Zombie
    {
        public override int Health
        {
            get; set;
        }

        public override char Type
        {
            get; set;
        }

        public BucketZombie()
        {
            this.Health = 150;
            this.Type = 'B';
        }

        public override void TakeDamage(int damage)
        {
            this.Health -= damage;
        }
    }
}

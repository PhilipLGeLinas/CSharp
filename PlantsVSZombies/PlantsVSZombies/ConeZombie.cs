using System;
namespace CptS487_HW4
{
    public class ConeZombie : Zombie
    {
        public override int Health
        {
            get; set;
        }

        public override char Type
        {
            get; set;
        }

        public ConeZombie()
        {
            this.Health = 75;
            this.Type = 'C';
        }

        public override void TakeDamage(int damage)
        {
            this.Health -= damage;
        }
    }
}

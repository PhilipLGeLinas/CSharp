using System;
namespace CptS487_HW4
{
    public abstract class Zombie
    {
        public abstract int Health
        {
            get; set;
        }

        public abstract char Type
        {
            get; set;
        }

        public Zombie()
        {
        }

        public abstract void TakeDamage(int damage);

        public void Die()
        {
            Console.WriteLine("A zombie has been killed!");
            Console.ReadLine();
        }
    }
}

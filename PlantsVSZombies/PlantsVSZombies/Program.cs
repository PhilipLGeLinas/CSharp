using System;
using System.Collections.Generic;
using System.Text;

namespace CptS487_HW4
{
    class MainClass
    {
        static ZombieFactory factory = new ZombieFactory();
        static List<Zombie> zombies = new List<Zombie>();

        public static void Main(string[] args)
        {
            while (true)
            {
                String input;
                Console.Clear();
                Console.WriteLine("0. Exit application?");
                Console.WriteLine("1. Create zombies?");
                Console.WriteLine("2. Demo gameplay?");
                input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        Environment.Exit(0);
                        break;
                    case "1":
                        CreateZombies();
                        break;
                    case "2":
                        if (zombies.Count == 0)
                        {
                            Console.WriteLine("You have not created any zombies!");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.Write("Please set a damage value: ");
                            try
                            {
                                int damage = int.Parse(Console.ReadLine());
                                if (damage > 0)
                                {
                                    DemoGameplay(damage);
                                }
                                else
                                {
                                    Console.WriteLine("Damage value must be positive!");
                                    ReadAndClear();
                                }
                            }
                            catch
                            {
                                Console.WriteLine("Damage value must be an integer!");
                                ReadAndClear();
                            }
                        }

                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        ReadAndClear();
                        break;
                }
            }
        }

        public static void CreateZombies()
        {
            while (true)
            {
                String input;
                Console.Clear();
                Console.WriteLine("Which kind?");
                Console.WriteLine("0. Done creating?");
                Console.WriteLine("1. Regular Zombie");
                Console.WriteLine("2. Cone Zombie");
                Console.WriteLine("3. Bucket Zombie");
                Console.WriteLine("4. Door Zombie");
                input = Console.ReadLine();

                if (input == "0")
                {
                    Console.Clear();
                    return;
                }

                Zombie zombie = factory.CreateZombie(input);

                if (zombie != null)
                {
                    zombies.Add(zombie);
                    Console.WriteLine();
                    Console.WriteLine("Zombie added!");
                    ReadAndClear();
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine(input + " is not a valid zombie type!");
                    ReadAndClear();
                }
            }
        }

        public static void DemoGameplay(int damage)
        {
            while (zombies.Count > 0)
            {
                for (int i = 0; i < zombies.Count; i++)
                {
                    PrintHealth();
                    zombies[i].TakeDamage(damage);
                    if (zombies[i].Health <= 50)
                    {
                        zombies[i] = new RegularZombie(zombies[i].Health);
                    }
                    if (zombies[i].Health <= 0)
                    {
                        zombies[i].Die();
                        zombies.Remove(zombies[i]);
                        DemoGameplay(damage);
                    }
                }
            }

            Console.Clear();
        }

        public static void PrintHealth()
        {
            Console.Clear();
            StringBuilder sb = new StringBuilder();
            sb.Append("[");

            for (int i = 0; i < zombies.Count - 1; i++)
            {
                Zombie zombie = zombies[i];
                sb.Append(zombie.Type + "/" + zombie.Health + ", ");
            }

            Zombie lastZombie = zombies[zombies.Count - 1];
            sb.Append(lastZombie.Type + "/" + lastZombie.Health + "]");
            Console.WriteLine(sb.ToString());
            Console.WriteLine();
            Console.ReadLine();
        }

        public static void ReadAndClear()
        {
            Console.ReadLine();
            Console.Clear();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TextRPG2
{
    public enum GameMode
    {
        Game,
        Lobby,
        Town,
        Field
    }
    class Game
    {
        private GameMode mode = GameMode.Lobby;
        private Player player = null;
        private Monster monster = null;
        private Random rand = new Random();

        public void Process()
        {
            switch (mode)
            {
                case GameMode.Lobby:
                    ProcessLobby();
                    break;
                case GameMode.Town:
                    ProcessTown();
                    break;
                case GameMode.Field:
                    ProcessField();
                    break;
            }
        }

        public void ProcessLobby()
        {
            Console.WriteLine("Choose Job");
            Console.WriteLine("[1] Knight");
            Console.WriteLine("[2] Archer");
            Console.WriteLine("[3] Mage");

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    player = new Knight();
                    mode = GameMode.Town;
                    break;
                case "2":
                    player = new Archer();
                    mode = GameMode.Town;
                    break;
                case "3":
                    player = new Mage();
                    mode = GameMode.Town;
                    break;
            }
        }

        private void ProcessTown()
        {
            Console.WriteLine("Entered the Town");
            Console.WriteLine("[1] Go to the Field");
            Console.WriteLine("[2] Go back to the Lobby");

            string input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    mode = GameMode.Field;
                    break;
                case "2":
                    mode = GameMode.Lobby;
                    break;
            }
        }

        public void ProcessField()
        {
            Console.WriteLine("Entered the Town");
            Console.WriteLine("[1] Fight");
            Console.WriteLine("[2] Return to town with a certain probability");

            CreateRandomMonster();

            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    ProcessFight();
                    break;
                case "2":
                    TryEscape();
                    break;
            }
        }

        private void TryEscape()
        {
            int randValue = rand.Next(0, 101);

            if (randValue < 33)
            {
                mode = GameMode.Town;
            }
            else
            {
                ProcessFight();
            }
        }

        private void ProcessFight()
        {
            while (true)
            {
                int damage = player.GetAttack();
                monster.OnDamaged(damage);
                if (monster.IsDead())
                {
                    Console.WriteLine("You win");
                    Console.WriteLine($"The rest of hp : {player.GetHp()}");
                    break;
                }

                damage = monster.GetAttack();
                player.OnDamaged(damage);
                if (player.IsDead())
                {
                    Console.WriteLine("You lose");
                    mode = GameMode.Lobby;
                    break;
                }

            }
        }

        private void CreateRandomMonster()
        {
            int randValue = rand.Next(0, 3);
            switch (randValue)
            {
                case 0:
                    monster = new Slime();
                    Console.WriteLine("Slime has been created!");
                    break;
                case 1:
                    monster = new Orc();
                    Console.WriteLine("Orc has been created!");
                    break;
                case 2:
                    monster = new Skeleton();
                    Console.WriteLine("Skeleton has been created!");
                    break;
            }
        }
    }
}


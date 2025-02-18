﻿
using System;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    public class Fight
    {
        public Fight(Character character1, Character character2)
        {
            if (character1 == null || character2 == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                Character1 = character1;
                Character2 = character2;
            }

        }

        public Character Character1 { get; }
        public Character Character2 { get; }
        /// <summary>
        /// Est-ce la condition de victoire/défaite a été rencontré ?
        /// </summary>
        public bool IsFightFinished()
        {
            return Character1.CurrentHealth <= 0 || Character2.CurrentHealth <= 0;
        }

        /// <summary>
        /// Jouer l'enchainement des attaques. Attention à bien gérer l'ordre des attaques par apport à la speed des personnages
        /// </summary>
        /// <param name="skillFromCharacter1">L'attaque selectionné par le joueur 1</param>
        /// <param name="skillFromCharacter2">L'attaque selectionné par le joueur 2</param>
        /// <exception cref="ArgumentNullException">si une des deux attaques est null</exception>
        public void ExecuteTurn(Skill skillFromCharacter1, Skill skillFromCharacter2)
        {
            if(Character1.Speed > Character2.Speed)
            {
                if(skillFromCharacter1 == null || skillFromCharacter2 == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    if(Character1.CurrentHealth > 0)
                    {
                        bool criticalStrike = Character1.CriticalStrike();
                        Character2.ReceiveAttack(skillFromCharacter1,criticalStrike);
                    }
                    if(Character2.CurrentHealth > 0)
                    {
                        bool criticalStrike = Character2.CriticalStrike();
                        Character1.ReceiveAttack(skillFromCharacter2, criticalStrike);
                    }
                }
            }
            else
            {
                if (skillFromCharacter1 == null || skillFromCharacter2 == null)
                {
                    throw new ArgumentNullException();
                }
                else
                {
                    if (Character2.CurrentHealth > 0)
                    {
                        bool criticalStrike = Character2.CriticalStrike();
                        Character1.ReceiveAttack(skillFromCharacter2, criticalStrike);
                    }
                    if (Character1.CurrentHealth > 0)
                    {
                        bool criticalStrike = Character1.CriticalStrike();
                        Character2.ReceiveAttack(skillFromCharacter1, criticalStrike);
                    }
                }
            }
        }

    }
}

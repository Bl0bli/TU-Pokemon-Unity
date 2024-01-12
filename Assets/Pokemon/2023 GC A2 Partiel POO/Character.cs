using System;
using UnityEngine;

namespace _2023_GC_A2_Partiel_POO.Level_2
{
    /// <summary>
    /// Définition d'un personnage
    /// </summary>
    public class Character
    {
        /// <summary>
        /// Stat de base, HP
        /// </summary>
        int _baseHealth;
        /// <summary>
        /// Stat de base, ATK
        /// </summary>
        int _baseAttack;
        /// <summary>
        /// Stat de base, DEF
        /// </summary>
        int _baseDefense;
        /// <summary>
        /// Stat de base, SPE
        /// </summary>
        int _baseSpeed;
        /// <summary>
        /// Type de base
        /// </summary>
        TYPE _baseType;

        public Character(int baseHealth, int baseAttack, int baseDefense, int baseSpeed, TYPE baseType)
        {
            _baseHealth = baseHealth;
            _baseAttack = baseAttack;
            _baseDefense = baseDefense;
            _baseSpeed = baseSpeed;
            _baseType = baseType;
            CurrentHealth = MaxHealth;
        }
        /// <summary>
        /// HP actuel du personnage
        /// </summary>
        public int CurrentHealth { get; private set; }
        public TYPE BaseType { get => _baseType;}
        /// <summary>
        /// HPMax, prendre en compte base et equipement potentiel
        /// </summary>
        public int MaxHealth
        {
            get
            {
                if (CurrentEquipment != null)
                {
                    return _baseHealth + CurrentEquipment.BonusHealth;
                }
                else
                {
                    return _baseHealth;

                }   
            }
        }
        /// <summary>
        /// ATK, prendre en compte base et equipement potentiel
        /// </summary>
        public int Attack
        {
            get
            {
                if(CurrentEquipment != null)
                {
                    return _baseAttack + CurrentEquipment.BonusAttack;
                }
                else
                {
                    return _baseAttack;
                }
            }
        }
        /// <summary>
        /// DEF, prendre en compte base et equipement potentiel
        /// </summary>
        public int Defense
        {
            get
            {
                if (CurrentEquipment != null)
                {
                    return _baseDefense + CurrentEquipment.BonusDefense;
                }
                else
                {
                    return _baseDefense;
                }
            }
        }
        /// <summary>
        /// SPE, prendre en compte base et equipement potentiel
        /// </summary>
        public int Speed
        {
            get
            {
                if (CurrentEquipment != null)
                {
                    return _baseSpeed + CurrentEquipment.BonusSpeed;
                }
                else
                {
                    return _baseSpeed;
                }

            }
        }
        /// <summary>
        /// Equipement unique du personnage
        /// </summary>
        public Equipment CurrentEquipment { get; private set; }
        /// <summary>
        /// null si pas de status
        /// </summary>
        public StatusEffect CurrentStatus { get; private set; }

        public bool IsAlive()
        {
            return CurrentHealth > 0;
        }


        /// <summary>
        /// Application d'un skill contre le personnage
        /// On pourrait potentiellement avoir besoin de connaitre le personnage attaquant,
        /// Vous pouvez adapter au besoin
        /// </summary>
        /// <param name="s">skill attaquant</param>
        /// <exception cref="NotImplementedException"></exception>
        public void ReceiveAttack(Skill s, bool criticalStrike)
        {
            if (criticalStrike)
            {
                if (CurrentHealth - (s.Power - Defense)*2 > 0)
                {
                    CurrentHealth -= (s.Power - Defense)*2;
                    Debug.Log("Critical Strike");
                }
                else
                {
                    CurrentHealth = 0;
                }
            }
            else
            {
                if (CurrentHealth - (s.Power - Defense) > 0)
                {
                    CurrentHealth -= (s.Power - Defense);
                }
                else
                {
                    CurrentHealth = 0;
                }
            }
        }
        /// <summary>
        /// Equipe un objet au personnage
        /// </summary>
        /// <param name="newEquipment">equipement a appliquer</param>
        /// <exception cref="ArgumentNullException">Si equipement est null</exception>
        public void Equip(Equipment newEquipment)
        {
            if(newEquipment == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                CurrentEquipment = newEquipment;
            }
        }
        /// <summary>
        /// Desequipe l'objet en cours au personnage
        /// </summary>
        public void Unequip()
        {
            if(CurrentEquipment != null)
            {
                CurrentEquipment = null;
            }
        }


        public bool CriticalStrike()
        {
            int attack = Attack;
            int random = UnityEngine.Random.Range(0, 100);
            random += attack / 10;

            return random > 50;
        }
    }
}

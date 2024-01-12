using _2023_GC_A2_Partiel_POO.Level_2;
using NUnit.Framework;
using System;
namespace _2023_GC_A2_Partiel_POO.Tests.Level_2
{
    public class FightMoreTests
    {
        // Tu as probablement remarqué qu'il y a encore beaucoup de code qui n'a pas été testé ...
        // À présent c'est à toi de créer les TU sur le reste et de les implémenter

        // Ce que tu peux ajouter:
        // - Ajouter davantage de sécurité sur les tests apportés
        // - un heal ne régénère pas plus que les HP Max
        // - si on abaisse les HPMax les HP courant doivent suivre si c'est au dessus de la nouvelle valeur
        // - ajouter un equipement qui rend les attaques prioritaires puis l'enlever et voir que l'attaque n'est plus prioritaire etc)
        // - Le support des status (sleep et burn) qui font des effets à la fin du tour et/ou empeche le pkmn d'agir
        // - Gérer la notion de force/faiblesse avec les différentes attaques à disposition (skills.cs)
        // - Cumuler les force/faiblesses en ajoutant un type pour l'équipement qui rendrait plus sensible/résistant à un type

        [Test]
        public void CriticalStrikeIsApply()
        {
            Character pikachu = new Character(200, 10, 30, 20, TYPE.NORMAL);
            Character mewtwo = new Character(200, 20, 0, 200, TYPE.NORMAL);
            Punch p = new Punch();

            mewtwo.ReceiveAttack(p, true);
            Assert.That(mewtwo.CurrentHealth, Is.EqualTo(60));
            pikachu.ReceiveAttack(p, true);
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(120));
        }

        [Test]
        public void CriticalStrikeIsNotApply()
        {
            Character pikachu = new Character(200, 10, 30, 20, TYPE.NORMAL);
            Character mewtwo = new Character(200, 20, 0, 200, TYPE.NORMAL);
            Punch p = new Punch();

            mewtwo.ReceiveAttack(p, false);
            Assert.That(mewtwo.CurrentHealth, Is.EqualTo(130));
            pikachu.ReceiveAttack(p, false);
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(160));
        }
        [Test]
        public void CriticalStrikeRandomIsApply()
        {
            Character pikachu = new Character(100, 1000, 30, 20, TYPE.NORMAL);
            Character bulbizarre = new Character(200, 0, 10, 200, TYPE.NORMAL);
            Fight f = new Fight(pikachu, bulbizarre);
            Punch p = new Punch();

            // Both uses punch
            f.ExecuteTurn(p, p);

            Assert.That(bulbizarre.CurrentHealth, Is.EqualTo(80));
        }

        [Test]
        public void ItHasPrioritarWithEquipment()
        {
            Character pikachu = new Character(10, 1000, 30, 20, TYPE.NORMAL);
            Character bulbizarre = new Character(80, 0, 10, 200, TYPE.NORMAL);
            Character salameche = new Character(80, 1000, 10, 200, TYPE.NORMAL);
            Fight f = new Fight(pikachu, bulbizarre);
            Punch p = new Punch();
            Punch p2 = new Punch();
            Equipment e = new Equipment(0,0,0,2000);
            pikachu.Equip(e);

            // Both uses punch
            f.ExecuteTurn(p, p);
            Assert.That(bulbizarre.CurrentHealth, Is.EqualTo(0));
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(10));
            pikachu.Unequip();
            f = new Fight(pikachu, salameche);
            f.ExecuteTurn(p, p2);
            Assert.That(pikachu.CurrentHealth, Is.EqualTo(0));
            Assert.That(salameche.CurrentHealth, Is.EqualTo(80));
        }

    }
}

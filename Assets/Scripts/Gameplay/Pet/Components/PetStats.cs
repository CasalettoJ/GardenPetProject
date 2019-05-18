using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PetProject
{

    public enum PetStatType
    {
        NONE = 0,
        HAPPINESS = 1,
        ENERGY = 2,
        HUNGER = 3,
        HP = 4,
    }

    public class PetStats : MonoBehaviour
    {
        private PetAbilities _abilities;
        private int _happiness = 100;
        private int _energy = 100;
        private int _hunger = 100;
        private int _hp = 10;
        public string Name { get; private set; }

        public void Start()
        {
            _abilities = GetComponent<PetAbilities>();
            SetName(Name);
        }

        public void SetName(string name)
        {
            string oldName = Name;
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                Name = Random.Range(0, int.MaxValue).ToString(); // TODO: Random name generation system
            } else
            {
                Name = name;
            }
            if (!string.IsNullOrEmpty(oldName) && !string.IsNullOrWhiteSpace(oldName))
            {
                PetNameChangeArgs args = new PetNameChangeArgs(oldName, name, this.gameObject);
                GameEventLibrary.PetNameChangeEvent.FireEvent(args);
            }
        }

        public void UpdateStat(PetStatType type, int amount, GameObject source)
        {
            switch(type)
            {
                case PetStatType.ENERGY:
                    UpdateEnergy(amount);
                    break;
                case PetStatType.HAPPINESS:
                    UpdateHappiness(amount);
                    break;
                case PetStatType.HP:
                    UpdateHP(amount);
                    break;
                case PetStatType.HUNGER:
                    UpdateHunger(amount);
                    break;
                case PetStatType.NONE:
                default:
                    return;
            }
            PetStatUpdateArgs args = new PetStatUpdateArgs(type, amount, transform.gameObject, source);
            GameEventLibrary.PetStatUpdateEvent.FireEvent(args);
        }

        public int GetStat(PetStatType t)
        {
            switch (t)
            {
                case PetStatType.ENERGY:
                    return GetEnergy();
                case PetStatType.HAPPINESS:
                    return GetHappiness();
                case PetStatType.HP:
                    return GetHP();
                case PetStatType.HUNGER:
                    return GetHunger();
                case PetStatType.NONE:
                default:
                    return 0;
            }
        }

        private void UpdateHP(int amount)
        {
            _hp += amount;
        }
        private void UpdateEnergy(int amount)
        {
            _energy += amount;
        }
        private void UpdateHunger(int amount)
        {
            _hunger += amount;
        }
        private void UpdateHappiness(int amount)
        {
            _happiness += amount;
        }

        private int GetHP()
        {
            //TODO: Calculate any mods
            return _hp;
        }

        private int GetHappiness()
        {
            //TODO: Calculate any mods
            return _happiness;
        }

        private int GetHunger()
        {
            //TODO: Calculate any mods
            return _hunger;
        }

        private int GetEnergy()
        {
            //TODO: Calculate any mods
            return _energy;
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PetProject
{

    [System.Serializable]
    public struct PetAbility
    {
        public PetAbilityType Type;
        public int Level;
        public float ModValue;
        public float Experience;
        public PetAbilityGrade Grade;
    }

    public enum PetAbilityType
    {
        NONE = 0,
        RUN = 1,
        FLY = 2,
        STRENGTH = 3,
        LUCK = 4,
        SWIM = 5,
        INT = 6
    }

    public enum PetAbilityGrade
    {
        NONE = 0,
        S = 1,
        A = 2,
        B = 3,
        C = 4,
        D = 5,
        E = 6,
        F = 7
    }

    public class PetAbilities : MonoBehaviour
    {
        public List<PetAbility> Abilities = new List<PetAbility>();

        public PetAbility GetAbility(PetAbilityType t)
        {
            return Abilities.Where(a => a.Type == t).FirstOrDefault();
        }

        public void AddExperienceToAbility(PetAbilityType t, float amount, GameObject source)
        {
            PetAbility ability = GetAbility(t);
            if (ability.Type == PetAbilityType.NONE)
            {
                return;
            }
            PetAbility updatedAbility = CalculateNewLevels(ability, amount);
            Abilities.Remove(ability); //TODO: Use a dictionary or something what the fuck
            Abilities.Add(updatedAbility);
            PetAbilityUpdateArgs args = new PetAbilityUpdateArgs(ability, updatedAbility, transform.gameObject, source);
            GameEventLibrary.PetAbilityUpdateEvent.FireEvent(args);
        }

        private PetAbility CalculateNewLevels(PetAbility a, float newExperience)
        {
            //TODO: Calculate new level and modvalue based on experience and Grade
            int newLevel = Mathf.RoundToInt((a.Experience + newExperience) / 1000);
            int newModValue = Mathf.RoundToInt(newLevel * 10.5f);
            PetAbility newAbility = new PetAbility()
            {
                Experience = a.Experience + newExperience,
                Grade = a.Grade,
                Level = newLevel,
                ModValue = newModValue,
                Type = a.Type
            };
            return newAbility;
        }
    }
}
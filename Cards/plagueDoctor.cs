using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void PlagueDoctor_O0145()
        {
            byte[] portrait;
            byte[] emissive;

            // Update portrait and emission on loadup
            switch (ConfigManager.Instance.NumOfBlessings)
            {
                case 0:
                    portrait = Resources.plagueDoctor;
                    emissive = Resources.plagueDoctor_emission;
                    break;
                case 1:
                    portrait = Resources.plagueDoctor1;
                    emissive = Resources.plagueDoctor1_emission;
                    break;
                case 2:
                    portrait = Resources.plagueDoctor2;
                    emissive = Resources.plagueDoctor2_emission;
                    break;
                case 3:
                    portrait = Resources.plagueDoctor3;
                    emissive = Resources.plagueDoctor3_emission;
                    break;
                case 4:
                    portrait = Resources.plagueDoctor4;
                    emissive = Resources.plagueDoctor4_emission;
                    break;
                case 5:
                    portrait = Resources.plagueDoctor5;
                    emissive = Resources.plagueDoctor5_emission;
                    break;
                case 6:
                    portrait = Resources.plagueDoctor6;
                    emissive = Resources.plagueDoctor6_emission;
                    break;
                case 7:
                    portrait = Resources.plagueDoctor7;
                    emissive = Resources.plagueDoctor7_emission;
                    break;
                case 8:
                    portrait = Resources.plagueDoctor8;
                    emissive = Resources.plagueDoctor8_emission;
                    break;
                case 9:
                    portrait = Resources.plagueDoctor9;
                    emissive = Resources.plagueDoctor9_emission;
                    break;
                case 10:
                    portrait = Resources.plagueDoctor10;
                    emissive = Resources.plagueDoctor10_emission;
                    break;
                default:
                    portrait = Resources.plagueDoctor11;
                    emissive = Resources.plagueDoctor11_emission;
                    break;
            }
            List<Ability> abilities = new()
            {
                Ability.Flying,
                Healer.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Bless.specialAbility
            };
            List<CardAppearanceBehaviour.Appearance> appearances = new()
            {
                //ForcedWhite.appearance
            };
            CardHelper.CreateCard(
                "wstl_plagueDoctor", "Plague Doctor",
                "A worker of miracles. He humbly requests to join you.",
                0, 3, 0, 3,
                portrait, emissive,
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: appearances, onePerDeck: true, riskLevel: 1);
        }
    }
}
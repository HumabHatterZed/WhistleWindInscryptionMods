using InscryptionAPI;
using DiskCardGame;
using System.Collections.Generic;
using WhistleWindLobotomyMod.Properties;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Card_PlagueDoctor_O0145()
        {
            byte[] portrait;
            byte[] emissive;

            // Update portrait and emission on loadup
            switch (ConfigManager.Instance.NumOfBlessings)
            {
                case 0:
                    portrait = Artwork.plagueDoctor;
                    emissive = Artwork.plagueDoctor_emission;
                    break;
                case 1:
                    portrait = Artwork.plagueDoctor1;
                    emissive = Artwork.plagueDoctor1_emission;
                    break;
                case 2:
                    portrait = Artwork.plagueDoctor2;
                    emissive = Artwork.plagueDoctor2_emission;
                    break;
                case 3:
                    portrait = Artwork.plagueDoctor3;
                    emissive = Artwork.plagueDoctor3_emission;
                    break;
                case 4:
                    portrait = Artwork.plagueDoctor4;
                    emissive = Artwork.plagueDoctor4_emission;
                    break;
                case 5:
                    portrait = Artwork.plagueDoctor5;
                    emissive = Artwork.plagueDoctor5_emission;
                    break;
                case 6:
                    portrait = Artwork.plagueDoctor6;
                    emissive = Artwork.plagueDoctor6_emission;
                    break;
                case 7:
                    portrait = Artwork.plagueDoctor7;
                    emissive = Artwork.plagueDoctor7_emission;
                    break;
                case 8:
                    portrait = Artwork.plagueDoctor8;
                    emissive = Artwork.plagueDoctor8_emission;
                    break;
                case 9:
                    portrait = Artwork.plagueDoctor9;
                    emissive = Artwork.plagueDoctor9_emission;
                    break;
                case 10:
                    portrait = Artwork.plagueDoctor10;
                    emissive = Artwork.plagueDoctor10_emission;
                    break;
                default:
                    portrait = Artwork.plagueDoctor11;
                    emissive = Artwork.plagueDoctor11_emission;
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
                appearances: appearances, onePerDeck: true,
                choiceType: CardHelper.ChoiceType.Common, riskLevel: CardHelper.RiskLevel.Zayin);
        }
    }
}
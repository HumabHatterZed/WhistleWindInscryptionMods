using DiskCardGame;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using static WhistleWindLobotomyMod.Core.Helpers.LobotomyCardManager;

namespace WhistleWindLobotomyMod
{
    public partial class LobotomyPlugin
    {
        private void Card_PlagueDoctor_O0145()
        {
            byte[][] portraits = UpdatePlagueSprites();

            List<Ability> abilities = new()
            {
                Ability.Flying,
                Healer.ability
            };
            List<SpecialTriggeredAbility> specialAbilities = new()
            {
                Bless.specialAbility
            };
            CreateCard(
                "wstl_plagueDoctor", "Plague Doctor",
                "A worker of miracles. He humbly requests to join you.",
                atk: 0, hp: 3,
                blood: 0, bones: 3, energy: 0,
                portraits[0], portraits[1], pixelTexture: portraits[2],
                abilities: abilities, specialAbilities: specialAbilities,
                metaCategories: new(), tribes: new(), traits: new(),
                appearances: new(), onePerDeck: true,
                choiceType: CardHelper.CardChoiceType.Basic, riskLevel: RiskLevel.Zayin,
                customTribe: TribeDivine);
        }
        public static byte[][] UpdatePlagueSprites()
        {
            byte[][] resources = { null, null, null };
            // Update portrait and emission on loadup
            switch (LobotomyConfigManager.Instance.NumOfBlessings)
            {
                case 0:
                    resources[0] = Artwork.plagueDoctor;
                    resources[1] = Artwork.plagueDoctor_emission;
                    resources[2] = Artwork.allAroundHelper_pixel;
                    break;
                case 1:
                    resources[0] = Artwork.plagueDoctor1;
                    resources[1] = Artwork.plagueDoctor1_emission;
                    resources[2] = Artwork.allAroundHelper_pixel;
                    break;
                case 2:
                    resources[0] = Artwork.plagueDoctor2;
                    resources[1] = Artwork.plagueDoctor2_emission;
                    resources[2] = Artwork.allAroundHelper_pixel;
                    break;
                case 3:
                    resources[0] = Artwork.plagueDoctor3;
                    resources[1] = Artwork.plagueDoctor3_emission;
                    resources[2] = Artwork.allAroundHelper_pixel;
                    break;
                case 4:
                    resources[0] = Artwork.plagueDoctor4;
                    resources[1] = Artwork.plagueDoctor4_emission;
                    resources[2] = Artwork.allAroundHelper_pixel;
                    break;
                case 5:
                    resources[0] = Artwork.plagueDoctor5;
                    resources[1] = Artwork.plagueDoctor5_emission;
                    resources[2] = Artwork.allAroundHelper_pixel;
                    break;
                case 6:
                    resources[0] = Artwork.plagueDoctor6;
                    resources[1] = Artwork.plagueDoctor6_emission;
                    resources[2] = Artwork.allAroundHelper_pixel;
                    break;
                case 7:
                    resources[0] = Artwork.plagueDoctor7;
                    resources[1] = Artwork.plagueDoctor7_emission;
                    resources[2] = Artwork.allAroundHelper_pixel;
                    break;
                case 8:
                    resources[0] = Artwork.plagueDoctor8;
                    resources[1] = Artwork.plagueDoctor8_emission;
                    resources[2] = Artwork.allAroundHelper_pixel;
                    break;
                case 9:
                    resources[0] = Artwork.plagueDoctor9;
                    resources[1] = Artwork.plagueDoctor9_emission;
                    resources[2] = Artwork.allAroundHelper_pixel;
                    break;
                case 10:
                    resources[0] = Artwork.plagueDoctor10;
                    resources[1] = Artwork.plagueDoctor10_emission;
                    resources[2] = Artwork.allAroundHelper_pixel;
                    break;
                default:
                    resources[0] = Artwork.plagueDoctor11;
                    resources[1] = Artwork.plagueDoctor11_emission;
                    resources[2] = Artwork.allAroundHelper_pixel;
                    break;
            }
            return resources;
        }
    }
}
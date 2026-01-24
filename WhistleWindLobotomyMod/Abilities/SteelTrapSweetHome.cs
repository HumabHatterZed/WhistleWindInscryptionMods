using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddSteelTrapSweetHome() {
            AbilityManager.FullAbility full = AbilityManager.AllAbilities.AbilityByID(Ability.SteelTrap);
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.SetRulebookName("Sweet Trap")
                .SetRulebookDescription(full.BaseRulebookDescription)
                .SetPowerlevel(full.Info.powerLevel)
                .SetPixelAbilityIcon(full.Info.pixelIcon.texture)
                .SetCanStack(full.Info.canStack)
                .SetFlipYIfOpponent(full.Info.flipYIfOpponent)
                .SetOpponentUsable(full.Info.opponentUsable)
                .SetDefaultPart1Ability();

            SteelTrapSweetHome = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(SteelTrapSweetHome), full.Texture).Id;
        }

        public static Ability SteelTrapSweetHome;
    }

    public class SteelTrapSweetHome : SteelTrap {
        Tribe t = Tribe.None;
        public override Ability Ability => Abilities.SteelTrapSweetHome;
        public override CardInfo CardToDraw => DetermineCardToDraw();
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer) {
            if (killer != null && killer.Info.tribes.Count > 0) {
                t = killer.Info.tribes[0];
            }
            yield return base.OnDie(wasSacrifice, killer);
        }

        private CardInfo DetermineCardToDraw() {
            CardInfo result;
            switch (t) {
                case Tribe.Bird:
                    result = CardLoader.GetCardByName(Cards.mySweetHomeBr);
                    break;
                case Tribe.Canine:
                    result = CardLoader.GetCardByName(Cards.mySweetHomeC);
                    break;
                case Tribe.Hooved:
                    result = CardLoader.GetCardByName(Cards.mySweetHomeH);
                    break;
                case Tribe.Reptile:
                    result = CardLoader.GetCardByName(Cards.mySweetHomeR);
                    break;
                case Tribe.Insect:
                    result = CardLoader.GetCardByName(Cards.mySweetHomeI);
                    break;
                default:
                    if (t == AbnormalPlugin.TribeBotanic) {
                        result = CardLoader.GetCardByName(Cards.mySweetHomeB);
                    }
                    else if (t == AbnormalPlugin.TribeDivine) {
                        result = CardLoader.GetCardByName(Cards.mySweetHomeD);
                    }
                    else if (t == AbnormalPlugin.TribeFae) {
                        result = CardLoader.GetCardByName(Cards.mySweetHomeF);
                    }
                    else if (t == AbnormalPlugin.TribeMechanical) {
                        result = CardLoader.GetCardByName(Cards.mySweetHomeM);
                    }
                    else {
                        result = CardLoader.GetCardByName(Cards.mySweetHomeA);
                    }
                    break;
            }
            return result;
        }
    }
}

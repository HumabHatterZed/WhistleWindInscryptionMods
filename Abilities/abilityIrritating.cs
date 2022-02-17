using APIPlugin;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Irritating()
        {
            const string rulebookName = "Irritating";
            const string rulebookDescription = "The creature opposing this card gains 1 Power.";
            const string dialogue = "My beast finds yours irksome.";

            return WstlUtils.CreateAbility<Irritating>(
                Resources.sigilIrritating,
                rulebookName, rulebookDescription, dialogue, -1);
        }
    }
    public class Irritating : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard()
        {
            return base.Card.Slot.opposingSlot != null;
        }

        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.LearnAbility(0.4f);
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return base.Card.Slot.opposingSlot != null;
        }

        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.LearnAbility(0.4f);
        }
    }
}

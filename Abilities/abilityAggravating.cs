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
        private NewAbility Ability_Aggravating()
        {
            const string rulebookName = "Aggravating";
            const string rulebookDescription = "While this card is on the board, all opposing cards gain 1 Power.";
            const string dialogue = "The presence of your creature drives my beasts to bloodlust.";

            return WstlUtils.CreateAbility<Aggravating>(
                Resources.sigilAggravating,
                rulebookName, rulebookDescription, dialogue, -3);
        }
    }
    public class Aggravating : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard()
        {
            return ActivateOnPlay();
        }

        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.LearnAbility(0.4f);
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return ActivateOnPlay();
        }

        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.LearnAbility(0.4f);
        }
        public bool ActivateOnPlay()
        {
            int num = 0;
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetSlots(false))
            {
                if (slot.Card != null)
                {
                    num++;
                }
            }
            return num > 0;
        }
    }
}

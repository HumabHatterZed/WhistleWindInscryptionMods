using System.Linq;
using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Idol()
        {
            const string rulebookName = "Idol";
            const string rulebookDescription = "While this card is on the board, all opposing cards lose 1 Power.";
            const string dialogue = "My beasts defer to you.";

            return WstlUtils.CreateAbility<Idol>(
                Resources.sigilIdol,
                rulebookName, rulebookDescription, dialogue, 4);
        }
    }
    public class Idol : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToResolveOnBoard()
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.LearnAbility(0.5f);
            yield break;
        }
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            return ActivateOnPlay();
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.LearnAbility(0.5f);
            yield break;
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

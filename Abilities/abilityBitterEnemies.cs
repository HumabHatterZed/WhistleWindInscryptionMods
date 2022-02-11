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
        private NewAbility Ability_BitterEnemies()
        {
            const string rulebookName = "Bitter Enemies";
            const string rulebookDescription = "A card bearing this sigil gains 1 Power when another card on this board also has this sigil.";
            const string dialogue = "A bitter grudge laid bare.";

            return WstlUtils.CreateAbility<BitterEnemies>(
                Resources.sigilBitterEnemies,
                rulebookName, rulebookDescription, dialogue, 2, addModular: true);
        }
    }
    public class BitterEnemies : AbilityBehaviour
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
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot != base.Card.Slot))
            {
                if (slot.Card != null && slot.Card.HasAbility(BitterEnemies.ability))
                {
                    num++;
                }
            }
            return num > 0;
        }
    }
}

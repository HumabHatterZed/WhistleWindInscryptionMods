using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_BitterEnemies()
        {
            const string rulebookName = "Vendetta";
            const string rulebookDescription = "[creature] gains 1 Power for every opposing creature that also bears this sigil.";
            const string dialogue = "A bitter grudge laid bare.";

            BitterEnemies.ability = AbnormalAbilityHelper.CreateAbility<BitterEnemies>(
                "sigilBitterEnemies",
                rulebookName, rulebookDescription, dialogue, powerLevel: 2,
                modular: true, opponent: true, canStack: false).Id;
        }
    }
    public class BitterEnemies : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => ActivateOnPlay();
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => ActivateOnPlay();
        public override IEnumerator OnResolveOnBoard() => base.LearnAbility(0.4f);
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => base.LearnAbility(0.4f);

        // Gives +1 Attack to all cards with Bitter Enemies when two or more exist on the board (including the base)
        public int GetPassiveAttackBuff(PlayableCard target)
        {
            if (!this.Card.OnBoard || target.OpponentCard == base.Card.OpponentCard || target.Info.LacksAbility(ability) || target == base.Card)
                return 0;

            return BoardManager.Instance.GetCards(target.OpponentCard, x => x.HasAbility(this.Ability)).Count;
        }

        public bool ActivateOnPlay()
        {
            return BoardManager.Instance.GetCards(base.Card.OpponentCard, x => x.HasAbility(this.Ability)).Count > 0;
        }
    }
}

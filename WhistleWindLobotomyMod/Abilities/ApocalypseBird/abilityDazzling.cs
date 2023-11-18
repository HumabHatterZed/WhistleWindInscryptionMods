using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod
{
    public class Dazzling : AbilityBehaviour, IPreTakeDamage
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public bool RespondsToPreTakeDamage(PlayableCard source, int damage)
        {
            return source != null && source.HasStatusEffect<Enchanted>(true);
        }

        public IEnumerator OnPreTakeDamage(PlayableCard source, int damage)
        {
            base.Card.Anim.StrongNegationEffect();
            yield return source.Die(false, null);
            if (!base.HasLearned)
            {
                yield return new WaitForSeconds(0.5f);
                yield return base.LearnAbility();
            }
            else
                yield return new WaitForSeconds(0.25f);
        }


    }

    public partial class LobotomyPlugin
    {
        private void Ability_Dazzling()
        {
            const string rulebookName = "Dazzling";
            Dazzling.ability = LobotomyAbilityHelper.CreateAbility<Dazzling>(
                "sigilDazzling", rulebookName,
                "The turn after this card is played, inflict up to 3 other cards on the board with Enchanted.",
                "Like moths to a flame.", powerLevel: 0,
                canStack: false).Id;
        }
    }
}

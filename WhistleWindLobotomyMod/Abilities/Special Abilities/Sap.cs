using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Sap : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;

        public const string rName = "Sap";
        public const string rDesc = "Whenever Giant Tree Sap is sacrificed, there is a chance the sacrificing card will explode on resolve. This chance increases with every sacrifice.";

        private int sacrificeCount = 0;

        public override bool RespondsToSacrifice() => true;
        public override IEnumerator OnSacrifice()
        {
            GlobalTriggerHandler.Instance.NumTriggersThisBattle++;
            float chanceToExplode = Mathf.Min(.66f, sacrificeCount / 10f);
            if (SeededRandom.Value(base.GetRandomSeed()) <= chanceToExplode)
            {
                sacrificeCount = 0;
                PlayableCard card = Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard;
                card.Anim.StrongNegationEffect();
                if (card.LacksAbility(Ability.ExplodeOnDeath))
                {
                    card.Status.hiddenAbilities.Add(Ability.ExplodeOnDeath);
                    card.AddTemporaryMod(new(Ability.ExplodeOnDeath));
                }
                card.AddPermanentBehaviour<SapDetonator>();
                yield return new WaitForSeconds(0.4f);
                yield return DialogueHelper.ShowUntilInput("A strange gurgling sound comes from your beast's stomach.");
            }
            else
                sacrificeCount++;
        }
    }
    public class SapDetonator : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public override int Priority => 1000;
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            yield return new WaitForSeconds(0.2f);
            base.PlayableCard.Anim.LightNegationEffect();
            yield return new WaitForSeconds(0.3f);
            yield return base.PlayableCard.Die(false, null);
        }
    }
    public class RulebookEntrySap : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Sap() => RulebookEntrySap.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntrySap>(Sap.rName, Sap.rDesc).Id;
        private void SpecialAbility_Sap()
        {
            Sap.specialAbility = AbilityHelper.CreateSpecialAbility<Sap>(pluginGuid, Sap.rName).Id;
            SapDetonator.specialAbility = AbilityHelper.CreateSpecialAbility<SapDetonator>(pluginGuid, "SapDetonator").Id;
        }
    }
}

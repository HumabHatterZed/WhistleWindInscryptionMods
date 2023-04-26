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
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public const string rName = "Sap";
        public const string rDesc = "Whenever Giant Tree Sap is sacrificed, there is an increasing chance the sacrificing card will explode.";

        private int sacrificeCount = 0;

        private void Start()
        {
            base.PlayableCard.Status.hiddenAbilities.Add(Ability.Sacrificial);
            base.PlayableCard.AddTemporaryMod(new(Ability.Sacrificial));
        }
        public override bool RespondsToSacrifice() => true;
        public override IEnumerator OnSacrifice()
        {
            float chanceToExplode = Mathf.Min(.6f, sacrificeCount / 10f);

            if (SeededRandom.Value(base.GetRandomSeed()) <= chanceToExplode)
            {
                sacrificeCount = 0;
                PlayableCard card = Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard;

                yield return DialogueHelper.PlayDialogueEvent("GiantTreeSapExplode", 0f);
                yield return new WaitForSeconds(0.2f);

                if (!card.HasAbility(Volatile.ability))
                {
                    card.FlipInHand(AddVolatile);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    card.Anim.StrongNegationEffect();
                    yield return new WaitForSeconds(0.4f);
                }
                yield return card.Info.SetExtendedProperty("wstl:Sap", true);
            }
            else
                sacrificeCount++;
        }

        private void AddVolatile()
        {
            PlayableCard card = Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard;
            card.AddTemporaryMod(new CardModificationInfo(Volatile.ability));
        }
    }
    public class RulebookEntrySap : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Sap()
            => RulebookEntrySap.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntrySap>(Sap.rName, Sap.rDesc).Id;
        private void SpecialAbility_Sap()
            => Sap.specialAbility = AbilityHelper.CreateSpecialAbility<Sap>(pluginGuid, Sap.rName).Id;
    }
}

using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Cowardly : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public static readonly string rName = "Cowardly";
        public static readonly string rDesc = "Whenever Giant Tree Cowardly is sacrificed, there is an increasing chance the sacrificing card will explode.";

        private int sacrificeCount;

        public override bool RespondsToSacrifice() => true;
        public override IEnumerator OnSacrifice()
        {
            int percent = Mathf.Max(15 - sacrificeCount, 1);
            if (SeededRandom.Range(0, percent, base.GetRandomSeed()) == 0)
            {
                sacrificeCount = 0;
                PlayableCard card = Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard;

                yield return DialogueEventsManager.PlayDialogueEvent("GiantTreeCowardlyExplode", 0f);
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
                yield return card.Info.SetExtendedProperty("wstl:Cowardly", true);
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
    public class RulebookEntryCowardly : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_Cowardly()
        {
            RulebookEntryCowardly.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryCowardly>(Cowardly.rName, Cowardly.rDesc).Id;
        }
        private void SpecialAbility_Cowardly()
        {
            Cowardly.specialAbility = AbilityHelper.CreateSpecialAbility<Cowardly>(pluginGuid, Cowardly.rName).Id;
        }
    }
}

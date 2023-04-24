using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class CensoredSpecial : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "CENSORED";
        public const string rDesc = "Whenver CENSORED kills a card, create a CENSORED copy of it in your hand.";

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (fromCombat)
                return killer == base.PlayableCard && killer.Info.name == "wstl_censored" && !card.LacksAllTraits(Trait.Terrain, Trait.Pelt);

            return false;
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // Creates a minion that has the abilities, tribes, power of the killed card
            CardInfo minion = CardLoader.GetCardByName("wstl_censoredMinion");

            minion.displayedName = card.Info.displayedName;
            minion.appearanceBehaviour = card.Info.appearanceBehaviour;
            minion.cost = card.Info.BloodCost;
            minion.bonesCost = card.Info.BonesCost;
            minion.energyCost = card.Info.EnergyCost;
            minion.gemsCost = card.Info.GemsCost;

            int newAttack = card.Info.baseAttack < 1 ? 1 : card.Info.baseAttack;

            minion.Mods.Add(new(newAttack, 1));

            // Adds tribes
            foreach (Tribe item in card.Info.tribes.FindAll((Tribe x) => x != Tribe.NUM_TRIBES))
                minion.tribes.Add(item);

            // Adds base sigils
            foreach (Ability item in card.Info.Abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
                minion.Mods.Add(new CardModificationInfo(item));

            foreach (CardModificationInfo item in card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
            {
                // Adds merged sigils
                CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                if (cardModificationInfo.healthAdjustment > 0)
                    cardModificationInfo.healthAdjustment = 0;

                minion.Mods.Add(cardModificationInfo);
            }

            base.PlayableCard.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);

            // create minion in hand if not an opponent, otherwise add to queue
            if (!base.PlayableCard.OpponentCard)
            {
                if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
                {
                    yield return new WaitForSeconds(0.2f);
                    Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                    yield return new WaitForSeconds(0.2f);
                }
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(minion);
                yield return new WaitForSeconds(0.45f);
            }
            else
            {
                HelperMethods.QueueCreatedCard(minion);
            }
            yield return DialogueEventsManager.PlayDialogueEvent("CENSOREDKilledCard");
            yield return new WaitForSeconds(0.25f);
        }
    }
    public class RulebookEntryCensoredSpecial : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_CensoredSpecial()
            => RulebookEntryCensoredSpecial.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryCensoredSpecial>(CensoredSpecial.rName, CensoredSpecial.rDesc).Id;
        private void SpecialAbility_CensoredSpecial()
            => CensoredSpecial.specialAbility = AbilityHelper.CreateSpecialAbility<CensoredSpecial>(pluginGuid, CensoredSpecial.rName).Id;
    }
}

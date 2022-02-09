using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using HarmonyLib;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_CENSORED()
        {
            const string rulebookName = "CENSORED";
            const string rulebookDescription = "(CENSORED) killed cards then adds them to your hand.";
            return WstlUtils.CreateSpecialAbility<CENSORED>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class CENSORED : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "CENSORED");
            }

        }
        /*
        private readonly string censoredDialogue = "What have you done to my beast?";
        
        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            // Returns true when this card is the killer,
            // and the killed is not Terrain or a Pelt
            return killer == base.Card && (!card.Info.HasTrait(Trait.Terrain) && !card.Info.HasTrait(Trait.Pelt));
        }

        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
            {
                yield return new WaitForSeconds(0.2f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                yield return new WaitForSeconds(0.2f);
            }
            // Creates a minion that has the abilities, tribes, health of the killed card
            CardInfo minion = CardLoader.GetCardByName("wstl_censoredMinion");
            List <CardModificationInfo> killedInfo = new();
            int killedAtk = card.Info.baseAttack - 1 <= 0 ? 0 : card.Info.baseAttack - 1;
            CardModificationInfo stats = new CardModificationInfo(killedAtk, 0);
            killedInfo.Add(stats);
            foreach (Ability item in card.Info.Abilities.FindAll((Ability x) => x != Ability.NUM_ABILITIES))
            {
                killedInfo.Add(new CardModificationInfo(item));
            }

            foreach (Tribe item in card.Info.tribes.FindAll((Tribe x) => x != Tribe.NUM_TRIBES))
            {
                minion.tribes.Add(item);
            }

            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(minion, killedInfo, 0.25f, null);

            yield return new WaitForSeconds(0.45f);
            if (!PersistentValues.HasSeenCensoredKill)
            {
                PersistentValues.HasSeenCensoredKill = true;
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(censoredDialogue, -0.65f, 0.4f, Emotion.Surprise);
            }
            yield return new WaitForSeconds(0.25f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);
        }*/
    }
}

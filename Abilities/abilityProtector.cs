using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Protector()
        {
            const string rulebookName = "Protector";
            const string rulebookDescription = "Adjacent cards take 1 less damage from attacks.";
            const string dialogue = "Your beast shields its ally against the blow.";

            Protector.ability = AbilityHelper.CreateAbility<Protector>(
                Resources.sigilProtector, Resources.sigilProtector_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Protector : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool IsDespair => base.Card.Info.name.ToLowerInvariant().Equals("wstl_magicalgirlspade");
        private bool IsArmy => base.Card.Info.name.ToLowerInvariant().Equals("wstl_armyinpink");

        private readonly string protectDialogue = "The knight shields her friend against the blow.";
        private readonly string despairDialogue = "Having failed to protect again, the knight fell once more to despair.";

        private readonly string blackDialogue = "The human heart is black, and must be cleaned.";

        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
            {
                if (slot.Card == target)
                {
                    return amount > 0;
                }
            }
            return false;
        }
        public override IEnumerator OnOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return target.Status.damageTaken--;
            base.Card.Anim.StrongNegationEffect();
            if (!IsDespair)
            {
                yield return base.LearnAbility(0.4f);
            }
            else if (!WstlSaveManager.HasSeenDespairProtect)
            {
                WstlSaveManager.HasSeenDespairProtect = true;
                yield return new WaitForSeconds(0.4f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(protectDialogue, -0.65f, 0.4f);
            }
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (fromCombat && (IsDespair || IsArmy) && base.Card.OnBoard)
            {
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
                {
                    if (slot.Card == card)
                    {
                        return fromCombat;
                    }
                }
            }
            return false;
        }
        public override IEnumerator OnOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.15f);
            base.Card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.4f);
            if (IsDespair)
            {
                CardInfo cardByName = CardLoader.GetCardByName("wstl_knightOfDespair");
                foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                {
                    CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                    cardByName.Mods.Add(cardModificationInfo);
                }
                yield return base.Card.TransformIntoCard(cardByName);
                yield return new WaitForSeconds(0.5f);
                if (!WstlSaveManager.HasSeenDespairTransformation)
                {
                    WstlSaveManager.HasSeenDespairTransformation = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(despairDialogue, -0.65f, 0.4f);
                }
                yield return new WaitForSeconds(0.25f);
                yield break;
            }
            if (IsArmy)
            {
                CardInfo cardByName = CardLoader.GetCardByName("wstl_armyInBlack");
                foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                {
                    CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                    cardByName.Mods.Add(cardModificationInfo);
                }
                yield return base.Card.TransformIntoCard(cardByName);
                yield return new WaitForSeconds(0.5f);
                yield return CreateArmyInHand();
                if (!WstlSaveManager.HasSeenArmyBlacked)
                {
                    WstlSaveManager.HasSeenArmyBlacked = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(blackDialogue, -0.65f, 0.4f);
                }
                yield return new WaitForSeconds(0.25f);
                yield break;
            }
        }
        private IEnumerator CreateArmyInHand()
        {
            CardInfo cardByName = CardLoader.GetCardByName("wstl_armyInBlack");

            if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
            {
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                yield return new WaitForSeconds(0.2f);
            }

            for (int i = 0; i < 3; i++)
            {
                yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardByName, null, 0.25f, null);
            }
            yield return new WaitForSeconds(0.45f);
        }
    }
}

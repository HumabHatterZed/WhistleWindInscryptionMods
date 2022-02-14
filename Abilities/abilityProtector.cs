using APIPlugin;
using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_Protector()
        {
            const string rulebookName = "Protector";
            const string rulebookDescription = "Adjacent cards take 1 less damage from attacks.";
            const string dialogue = "Your beast shields its ally against the blow.";

            return WstlUtils.CreateAbility<Protector>(
                Resources.sigilProtector,
                rulebookName, rulebookDescription, dialogue, 3);
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

        private readonly string blackDialogue = "The human heart is black. They will clean it with their love.";

        public override bool RespondsToOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            if (!attacker.Dead)
            {
                foreach (CardSlot slot in Singleton<BoardManager>.Instance.GetAdjacentSlots(base.Card.Slot).Where(slot => slot.Card != null))
                {
                    if (slot.Card == target)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public override IEnumerator OnOtherCardDealtDamage(PlayableCard attacker, int amount, PlayableCard target)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return target.Status.damageTaken > 0 ? target.Status.damageTaken-- : target.Status.damageTaken;
            base.Card.Anim.StrongNegationEffect();
            if (IsDespair && !PersistentValues.HasSeenDespairProtect)
            {
                PersistentValues.HasSeenDespairProtect = true;
                yield return new WaitForSeconds(0.4f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(protectDialogue, -0.65f, 0.4f);
            }
            else if (!IsDespair)
            {
                yield return base.LearnAbility(0.4f);
            }
            yield return new WaitForSeconds(0.25f);
        }

        public override bool RespondsToOtherCardDie(PlayableCard card, CardSlot deathSlot, bool fromCombat, PlayableCard killer)
        {
            if (IsDespair || IsArmy)
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
            yield return new WaitForSeconds(0.15f);
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
                if (!PersistentValues.HasSeenDespairTransformation)
                {
                    PersistentValues.HasSeenDespairTransformation = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(despairDialogue, -0.65f, 0.4f);
                }
                yield return new WaitForSeconds(0.25f);
                yield break;
            }
            if (IsArmy)
            {
                CardInfo cardByName = CardLoader.GetCardByName("wstl_armyInBlack");
                yield return base.Card.TransformIntoCard(cardByName);
                foreach (CardModificationInfo item in base.Card.Info.Mods.FindAll((CardModificationInfo x) => !x.nonCopyable))
                {
                    CardModificationInfo cardModificationInfo = (CardModificationInfo)item.Clone();
                    cardByName.Mods.Add(cardModificationInfo);
                }
                yield return new WaitForSeconds(0.5f);
                if (!PersistentValues.HasSeenArmyBlacked)
                {
                    PersistentValues.HasSeenArmyBlacked = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(blackDialogue, -0.65f, 0.4f);
                }
                yield return new WaitForSeconds(0.25f);
                yield break;
            }
        }
    }
}

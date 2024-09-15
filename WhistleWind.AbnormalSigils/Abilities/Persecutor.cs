using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Persecutor()
        {
            const string rulebookName = "Persecutor";
            const string rulebookDescription = "When [creature] is played, create a Nail in the adjacent left space and a Hammer in the adjacent right space if they are empty.";
            const string dialogue = "Are you guilty of having a closed heart?";
            const string triggerText = "[creature] reveals its hidden tools!";
            Persecutor.ability = AbnormalAbilityHelper.CreateAbility<Persecutor>(
                "sigilPersecutor",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 4,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Persecutor : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private readonly string CannotSpawnDialogue = "These tools remain hidden for now.";

        public override bool RespondsToResolveOnBoard() => true;

        public override IEnumerator OnResolveOnBoard()
        {
            Singleton<ViewManager>.Instance.SwitchToView(View.Board);
            CardSlot toLeft = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: true);
            CardSlot toRight = Singleton<BoardManager>.Instance.GetAdjacent(base.Card.Slot, adjacentOnLeft: false);
            bool toLeftValid = toLeft != null && toLeft.Card == null;
            bool toRightValid = toRight != null && toRight.Card == null;
            yield return base.PreSuccessfulTriggerSequence();
            if (toLeftValid)
            {
                yield return new WaitForSeconds(0.1f);
                yield return this.SpawnCardOnSlot(toLeft, true);
            }
            if (toRightValid)
            {
                yield return new WaitForSeconds(0.1f);
                yield return this.SpawnCardOnSlot(toRight, false);
            }

            if (toLeftValid || toRightValid)
                yield return base.LearnAbility();

            else if (!base.HasLearned)
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(this.CannotSpawnDialogue, -0.65f, 0.4f);
        }

        private IEnumerator SpawnCardOnSlot(CardSlot slot, bool left)
        {
            CardInfo cardByName = CardLoader.GetCardByName(left ? "wstl_nail" : "wstl_hammer");
            this.ModifySpawnedCard(cardByName);
            yield return Singleton<BoardManager>.Instance.CreateCardInSlot(cardByName, slot, 0.15f);
        }

        private void ModifySpawnedCard(CardInfo card)
        {
            List<Ability> abilities = base.Card.Info.Abilities;
            foreach (CardModificationInfo temporaryMod in base.Card.TemporaryMods)
                abilities.AddRange(temporaryMod.abilities);

            abilities.RemoveAll((Ability x) => x == this.Ability);
            if (abilities.Count > 4)
                abilities.RemoveRange(3, abilities.Count - 4);

            CardModificationInfo cardModificationInfo = new()
            {
                fromCardMerge = true,
                abilities = abilities
            };
            card.Mods.Add(cardModificationInfo);
        }
    }
}

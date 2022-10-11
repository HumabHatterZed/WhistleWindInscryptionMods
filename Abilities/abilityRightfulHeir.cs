using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;
using System;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_RightfulHeir()
        {
            const string rulebookName = "Rightful Heir";
            const string rulebookDescription = "Activate: Pay 5 Bones to choose a creature to be transformed into a Pumpkin. Give the transformed card the Fledgling sigil if it is an ally.";
            const string dialogue = "All she has left now are her children.";
            RightfulHeir.ability = AbilityHelper.CreateActivatedAbility<RightfulHeir>(
                Artwork.sigilRightfulHeir, Artwork.sigilRightfulHeir_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3).Id;
        }
    }
    public class RightfulHeir : ActivatedSelectSlotBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override int BonesCost => 5;
        public override Ability LatchAbility => Ability.None;
        public override string InvalidTargetDialogue => "That card is fine as it is.";
        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return false;
        }
        public override IEnumerator OnValidTargetSelected(CardSlot slot)
        {
            CardInfo info = CardLoader.GetCardByName("wstl_ozmaPumpkin");
            if (base.Card.OpponentCard != slot.IsPlayerSlot)
            {
                CardModificationInfo mod = new(Ability.Evolve) { fromEvolve = true };
                info.Mods.Add(mod);
            }
            yield return slot.Card.TransformIntoCard(info);
            yield return new WaitForSeconds(0.5f);
        }
        public override IEnumerator OnPostValidTargetSelected()
        {
            yield break;
        }
        public override bool CardIsNotValid(PlayableCard card)
        {
            return card.Info.name.Contains("ozmaPumpkin");
        }
    }
}

using InscryptionAPI;
using InscryptionAPI.Triggers;
using DiskCardGame;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_NeuteredLatch()
        {
            const string rulebookName = "Neutered Latch";
            const string rulebookDescription = "Pay 6 Bones to choose a creature to gain the Neutered sigil. This can only be used once per turn.";
            const string dialogue = "The will to fight has been lost.";
            NeuteredLatch.ability = AbilityHelper.CreateActivatedAbility<NeuteredLatch>(
                Resources.sigilNeuteredLatch, Resources.sigilNeuteredLatch_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 4).Id;
        }
    }
    public class NeuteredLatch : ActivatedLatchBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override Ability LatchAbility => Neutered.ability;
        public override int BonesCost => 6;
    }
}

using InscryptionAPI;
using InscryptionAPI.Triggers;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Neutered()
        {
            const string rulebookName = "Neutered";
            const string rulebookDescription = "A card bearing this sigil has their Power reduced to 0. This sigil is lost on upkeep.";
            const string dialogue = "femboy";
            Neutered.ability = AbilityHelper.CreateAbility<Neutered>(
                Resources.sigilNeutered, Resources.sigilNeutered_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: -3,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Neutered : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public int GetPassiveAttackBuff(PlayableCard target)
        {
            if (this.Card.OnBoard && target == base.Card)
                return -9999;
            return 0;
        }
        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return base.Card.OpponentCard != playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.StrongNegationEffect();
            for (CardModificationInfo temporaryEvolveMod = this.GetTemporaryEvolveMod(); temporaryEvolveMod != null; temporaryEvolveMod = this.GetTemporaryEvolveMod())
            {
                base.Card.RemoveTemporaryMod(temporaryEvolveMod);
            }
            yield return new WaitForSeconds(0.4f);
        }
        private CardModificationInfo GetTemporaryEvolveMod()
        {
            return base.Card.TemporaryMods.Find((CardModificationInfo x) => x.abilities.Contains(Neutered.ability));
        }
    }
}

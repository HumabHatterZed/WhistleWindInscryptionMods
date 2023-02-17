using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Neutered()
        {
            const string rulebookName = "Neutered";
            const string rulebookDescription = "[creature] has it Power reduced to 0. This sigil is removed on upkeep.";
            const string dialogue = "femboy";
            Neutered.ability = AbnormalAbilityHelper.CreateAbility<Neutered>(
                Artwork.sigilNeutered, Artwork.sigilNeutered_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: -3,
                modular: false, opponent: false, canStack: false).Id;
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
            yield return HelperMethods.ChangeCurrentView(View.Board);
            base.Card.Anim.StrongNegationEffect();
            for (CardModificationInfo temporaryEvolveMod = this.GetTemporaryEvolveMod(); temporaryEvolveMod != null; temporaryEvolveMod = this.GetTemporaryEvolveMod())
            {
                base.Card.RemoveTemporaryMod(temporaryEvolveMod);
            }
            yield return new WaitForSeconds(0.4f);
            yield return HelperMethods.ChangeCurrentView(View.Default);
        }
        private CardModificationInfo GetTemporaryEvolveMod()
        {
            return base.Card.TemporaryMods.Find((CardModificationInfo x) => x.abilities.Contains(Neutered.ability));
        }
    }
}

using DiskCardGame;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Neutered()
        {
            const string rulebookName = "Neutered";
            const string rulebookDescription = "[creature] has its Power reduced to 0. At the start of the owner's next turn, remove this sigil.";
            Neutered.ability = AbnormalAbilityHelper.CreateAbility<Neutered>(
                "sigilNeutered",
                rulebookName, rulebookDescription, powerLevel: -2,
                modular: false, opponent: false, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook().Id;
        }
    }
    public class Neutered : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int TurnPlayed = 0;
        private void Start() => TurnPlayed = TurnManager.Instance.TurnNumber;
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep && TurnPlayed != TurnManager.Instance.TurnNumber;
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.Anim.PlayTransformAnimation();
            for (CardModificationInfo temporaryEvolveMod = GetTemporaryEvolveMod(); temporaryEvolveMod != null; temporaryEvolveMod = GetTemporaryEvolveMod())
            {
                base.Card.RemoveTemporaryMod(temporaryEvolveMod);
            }
            yield return new WaitForSeconds(0.5f);
        }
        private CardModificationInfo GetTemporaryEvolveMod() => base.Card.TemporaryMods.Find((CardModificationInfo x) => x.abilities.Contains(ability));
    }
}

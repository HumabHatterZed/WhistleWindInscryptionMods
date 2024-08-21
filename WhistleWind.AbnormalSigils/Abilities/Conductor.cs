using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Conductor()
        {
            const string rulebookName = "Conductor";
            const string rulebookDescription = "When [creature] is played, begin Movement 1: Adagio.";
            const string dialogue = "The conductor begins to direct the apocalypse.";
            const string triggerText = "[creature] begins to direct the apocalypse.";
            Conductor.ability = AbnormalAbilityHelper.CreateAbility<Conductor>(
                "sigilConductor",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 3,
                modular: false, opponent: true, canStack: false).Id;

            Ability_Conductor1();
            Ability_Conductor2();
            Ability_Conductor3();
            Ability_Conductor4();
            Ability_Conductor5();
        }
    }
    public class Conductor : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public const string CONDUCTOR_ID = "wstl:Conductor";
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.5f);
            yield return base.LearnAbility();

            base.Card.Anim.StrongNegationEffect();
            base.Card.AddTemporaryMod(CreateConductorMod(MovementOne.ability));
            yield return new WaitForSeconds(0.4f);
            
        }

        public static CardModificationInfo CreateConductorMod(Ability newMovement)
        {
            return new(newMovement)
            {
                singletonId = CONDUCTOR_ID,
                negateAbilities = new() { Conductor.ability }
            };
        }
    }

    public abstract class ConductorMovementBase : AbilityBehaviour, IPassiveAttackBuff
    {
        public abstract Ability NextMovement { get; }

        public override bool RespondsToUpkeep(bool onPlayerUpkeep) => base.Card.OpponentCard != onPlayerUpkeep;
        public override IEnumerator OnUpkeep(bool onPlayerUpkeep)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return base.LearnAbility();

            CardModificationInfo mod = base.Card.TemporaryMods.Find(x => x.singletonId == Conductor.CONDUCTOR_ID);
            base.Card.RemoveTemporaryMod(mod, false);
            base.Card.Anim.StrongNegationEffect();
            base.Card.AddTemporaryMod(Conductor.CreateConductorMod(NextMovement));

            yield return new WaitForSeconds(0.4f);
            yield return HelperMethods.ChangeCurrentView(View.Default);
        }

        // 1st: adjacent cards +1
        // 2nd: allied cards +1
        // 3rd: all other cards +1
        // 4th: all other cards +2, randomise targeting
        // finale: all other cards +3, randomise targeting
        public abstract int GetPassiveAttackBuff(PlayableCard target);
    }
}

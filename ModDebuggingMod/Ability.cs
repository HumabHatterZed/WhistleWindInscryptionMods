using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils.Core.Helpers;

using WhistleWind.Core.Helpers;

namespace ModDebuggingMod
{
    public partial class Plugin
    {
        private void Ability_Test()
        {
            const string rulebookName = "Test";
            const string rulebookDescription = "When [creature] dies, the killer transforms into a copy of this card.";
            const string dialogue = "The curse continues unabated.";
            const string triggerText = "[creature] passes the curse on.";
            Test.ability = AbilityHelper.CreateAbility<Test>(
                pluginGuid, "sigilCursed",
                rulebookName, rulebookDescription, dialogue, triggerText, powerLevel: 0,
                modular: true, opponent: false, canStack: false).Id;
        }
    }
    public class Test : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            base.Card.AddTemporaryMod(new() { energyCostAdjustment = -1, bloodCostAdjustment = -1 });
            //yield return ResourcesManager.Instance.AddGem(GemType.Blue);
            yield break;
        }
    }
}

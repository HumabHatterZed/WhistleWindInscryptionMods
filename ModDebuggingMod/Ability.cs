using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
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
                modular: true, opponent: false, canStack: true).Id;//.Info.SetHideSingleStacks().ability;
        }
    }
    public class Test : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return playerUpkeep != base.Card.OpponentCard;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            if (base.Card.GetComponent<Evolve>().numTurnsInPlay > 0)
                yield return base.Card.Die(false);
        }
    }
}

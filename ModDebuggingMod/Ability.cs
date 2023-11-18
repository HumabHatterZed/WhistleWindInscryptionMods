using DiskCardGame;
using InscryptionAPI.Card;

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
    public class Test : DamageShieldBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override int StartingNumShields => base.Card.GetAbilityStacks(Ability);

        /*        public override bool RespondsToResolveOnBoard()
                {
                    return true;
                }

                public override IEnumerator OnResolveOnBoard()
                {
                    Debug.Log("Start");
                    base.Card.AddShieldCount(1);
                    yield return new WaitForSeconds(0.5f);
                    Debug.Log("Start2");
                    base.Card.AddShieldCount(1, Ability.DeathShield);
                    yield return new WaitForSeconds(0.5f);
                    Debug.Log("Start3 {}");
                    base.Card.AddShieldCount<APIDeathShield>(1);
                    yield return new WaitForSeconds(0.5f);
                }*/
    }
}

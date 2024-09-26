using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers;
//using InscryptionAPI.Slots;
using InscryptionAPI.Triggers;
using System.Collections;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod;

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
            Test.ability = AbilityHelper.New<Test>(
                pluginGuid, "sigilCursed",
                rulebookName, rulebookDescription, 0, true, dialogue, triggerText,
                modular: true, opponent: false, canStack: true).Id;
        }
    }
    public class Test : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            //yield return Card.Slot.SetSlotModification(PavedSlot.ID);
            //Card.Slot.GetSlotModification();
            //base.Card.AddStatusEffect<Enchanted>(1);
            yield break;
        }
        /*        public override bool RespondsToUpkeep(bool playerUpkeep)
                {
                    return playerUpkeep != base.Card.OpponentCard;
                }
                public override IEnumerator OnUpkeep(bool playerUpkeep)
                {
                    if (base.Card.GetComponent<Evolve>().numTurnsInPlay > 0)
                        yield return base.Card.Die(false);
                }*/
    }
/*    public class PavedSlot : SlotModificationGainAbilityBehaviour, IPassiveAttackBuff
    {
        public static readonly SlotModificationManager.ModificationType ID = SlotModificationManager.New(
            "MyPluginGuid",
            "PavedSlot",
            typeof(PavedSlot),
            TextureHelper.GetImageAsTexture("slotPavedRoad.png", typeof(PavedSlot).Assembly),
            TextureHelper.GetImageAsTexture("slotPavedRoad_pixel.png", typeof(PavedSlot).Assembly)
        );

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => playerTurnEnd == Slot.IsPlayerSlot;

        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (Slot.Card != null)
                yield return Slot.Card.TakeDamage(1, null);
        }
        public int GetPassiveAttackBuff(PlayableCard target)
        {
            return Slot.Card == target ? 1 : 0;
        }
        protected override Ability AbilityToGain => Ability.Sharp;
    }*/
}

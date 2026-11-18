using DiskCardGame;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.StatusEffects;

//using InscryptionAPI.Slots;
using WhistleWind.Core.Helpers;

namespace ModDebuggingMod {
    public partial class Plugin {
        private void Ability_Test() {
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
    public class Test : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToDealDamage(int amount, PlayableCard target) {
            return true;
        }
        public override IEnumerator OnDealDamage(int amount, PlayableCard target) {
            //yield return target.AddStatusEffect<Worms>(5, true);
            yield return target.AddStatusEffect<Worms>(1);
            yield return target.AddStatusEffect<Spores>(1);
            yield return target.AddStatusEffect<Prudence>(1);
            yield return target.AddStatusEffect<Decay>(5);
            yield return target.AddStatusEffect<Sinking>(2);
        }
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

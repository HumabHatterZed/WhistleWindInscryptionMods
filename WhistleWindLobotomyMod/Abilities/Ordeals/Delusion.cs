using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddDelusion() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Delusion";
            info.rulebookDescription = "At the end of the owner's turn, reduce this sigil's counter by 1. If the counter is 0, activate this card's God sigil then reset the counter to 2~3.";
            info.powerLevel = 0;

            Delusion.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(Delusion), TextureLoader.LoadTextureFromFile("sigilDelusion.png")).Id;
        }
    }

    public class Delusion : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        private int counter;
        private GodColourAbilityBehaviour behav = null;
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            counter = SeededRandom.Range(2, 5, base.GetRandomSeed() + base.Card.Slot.Index);
            behav = base.Card.TriggerHandler.GetComponent<GodColourAbilityBehaviour>();
            base.Card.RenderInfo.OverrideAbilityIcon(this.Ability, GetDelusionOverrideTex());
            base.Card.RenderCard();
            yield break;
        }
        public override bool RespondsToUpkeep(bool playerUpkeep) => base.Card.OpponentCard != playerUpkeep;
        public override IEnumerator OnUpkeep(bool playerUpkeep) {
            counter--;
            base.Card.RenderInfo.OverrideAbilityIcon(this.Ability, GetDelusionOverrideTex());
            base.Card.RenderCard();
            if (counter == 1) {
                // play sound to indicate it's about to pop
                //yield return behav.PreActivate();
                yield return new WaitForSeconds(0.5f);
            }
            if (counter < 1) {
                //yield return behav.Activate();
                yield return new WaitForSeconds(0.5f);
                GlobalTriggerHandler.Instance.NumTriggersThisBattle++;
                counter = SeededRandom.Range(2, 4, base.GetRandomSeed() + TurnManager.Instance.TurnNumber + base.Card.Slot.Index);
                base.Card.RenderInfo.OverrideAbilityIcon(this.Ability, GetDelusionOverrideTex());
                base.Card.RenderCard();
            }
        }
        private Texture GetDelusionOverrideTex() {
            if (counter > 0) {
                return ResourceBank.Get<Texture>("Art/Cards/AbilityIcons/sigilDelusion_" +  counter);
            }
            return AbilityManager.AllAbilities.AbilityByID(this.Ability).Texture;
        }
    }

    public abstract class GodColourAbilityBehaviour : AbilityBehaviour {
        public abstract IEnumerator PreActivate();
        public abstract IEnumerator Activate();
    }
}

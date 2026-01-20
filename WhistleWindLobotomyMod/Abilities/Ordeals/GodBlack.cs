using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.RuleBook;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Opponents;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddGodBlack() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "The God Black";
            info.rulebookDescription = "Activate: Deal 1 damage to all opposing spaces, Piercing through struck cards.";
            info.powerLevel = 5;

            GodBlack.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(GodBlack), TextureLoader.LoadTextureFromFile("sigilGodBlack.png"))
                .SetAbilityRedirect("Piercing", Piercing.ability, GameColors.Instance.fuschia)
                .Id;
        }
    }

    public class GodBlack : GodColourAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        private PlayableCard dummyCard = null;
        protected override IEnumerator PreActivate() {
            // visuals
            SetUpDummyCard();
            yield break;
        }
        protected override IEnumerator Activate() {
            // visuals
            foreach (CardSlot slot in BoardManager.Instance.PlayerSlotsCopy) {
                if (slot.Card != null) {
                    SetUpDummyCard();
                    yield return slot.Card.TakeDamage(1, dummyCard);
                }
                else {
                    Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase++;
                }
            }
        }

        private void SetUpDummyCard() {
            if (dummyCard != null) return;

            CardInfo info = ScriptableObject.CreateInstance<CardInfo>();
            info.baseHealth = 9999;
            info.AddAbilities(Driver.ability, Piercing.ability);
            info.AddTraits(Trait.Uncuttable, Trait.Structure, AbnormalPlugin.ImmuneToInstaDeath, AbnormalPlugin.ImmuneToAilments);
            dummyCard = CardSpawner.SpawnPlayableCard(info);
            dummyCard.transform.position = new Vector3(100f, 100f, 100f); // hide offscreen
            dummyCard.Dead = true; // prevent this card from triggering various things it shouldn't
        }

        public override void SetUpVisualGameObject() {
            SetUpDummyCard();
            // stub
        }
    }
}

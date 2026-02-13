using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
using System.Collections;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddGodRed() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "The God Red";
            info.rulebookDescription = "Activate: .";
            info.powerLevel = 5;

            GodRed.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(GodRed), TextureLoader.LoadTextureFromFile("sigilGodRed.png"))
                .SetAbilityRedirect("Pin Down", Driver.ability, Color.red)
                .Id;
        }
    }

    public class GodRed : GodColourAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        private PlayableCard dummyCard = null;

        protected override IEnumerator PreActivate(bool halfHealth) {
            // visuals
            yield break;
        }
        protected override IEnumerator Activate(bool halfHealth) {
            // visuals
            foreach (CardSlot slot in BoardManager.Instance.PlayerSlotsCopy) {
                if (slot.Card != null) {
                    yield return slot.Card.TakeDamage(3, null);
                }
            }
        }

        public override void SetUpVisualGameObject() {
            // stub
        }

        protected override IEnumerator CleanUpVisuals() {
            //if (spikesOpen) {
            //    HideSpikes(true);
            //}

            yield return new WaitForSeconds(0.5f);
            Destroy(activateVisualGameObject);
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
    }
}

using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.RuleBook;
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
            info.rulebookDescription = "Activate: Launch a Spike through each lane. At half Health (once): Launch a Purple Spike through this card's lane.";
            info.powerLevel = 5;

            GodBlack.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(GodBlack), TextureLoader.LoadTextureFromFile("sigilGodBlack.png"))
                .SetUniqueRedirect("Spike", "wstl:Ordeals_Purple Spike", GameColors.Instance.purple)
                .Id;
        }
    }

    public class GodBlack : GodColourAbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        private PlayableCard dummyCard = null;
        private readonly List<Animator> spikeAnims = new();
        private Animator defenceSpike;

        protected override IEnumerator PreActivate(bool halfHealth) {
            SetUpDummyCard();
            ViewManager.Instance.SwitchToView(View.Default);
            AudioController.Instance.PlaySound2D("Violet_portal_on", MixerGroup.TableObjectsSFX);
            
            if (halfHealth) {
                defenceSpike.SetTrigger("Show");
                defenceSpike.SetLayerWeight(1, 1f);
            }
            else {
                foreach (Animator anim in spikeAnims) {
                    anim.SetTrigger("Show");
                    anim.SetLayerWeight(1, 1f);
                }
            }

            yield return new WaitForSeconds(1f);
            AudioController.Instance.PlaySound2D("Violet_spike", MixerGroup.TableObjectsSFX);
            yield return new WaitForSeconds(1f);
        }
        protected override IEnumerator Activate(bool halfHealth) {
            ViewManager.Instance.SwitchToView(View.Default);
            if (halfHealth) {
                defenceSpike.SetTrigger("Extend");
                yield return new WaitForSeconds(0.05f);
                AudioController.Instance.PlaySound2D("Violet_attack", MixerGroup.TableObjectsSFX);
                if (base.Card.OpposingCard() != null) {
                    SetUpDummyCard();
                    yield return base.Card.OpposingCard().TakeDamage(3, dummyCard);
                }

                yield return new WaitForSeconds(0.45f);
                ViewManager.Instance.SwitchToView(View.Default);
                defenceSpike.SetTrigger("Hide");
                defenceSpike.SetLayerWeight(1, 0f);
                AudioController.Instance.PlaySound2D("Violet_portal_off", MixerGroup.TableObjectsSFX);
                ViewManager.Instance.SwitchToView(View.Board);
            }
            else {
                foreach (CardSlot slot in BoardManager.Instance.PlayerSlotsCopy) {
                    spikeAnims[slot.Index].SetTrigger("Extend");
                    yield return new WaitForSeconds(0.05f);
                    AudioController.Instance.PlaySound2D("Violet_attack", MixerGroup.TableObjectsSFX);
                    if (slot.Card != null) {
                        SetUpDummyCard();
                        yield return slot.Card.TakeDamage(3, dummyCard);
                    }
                }

                yield return new WaitForSeconds(0.45f);
                HideSpikes(false);
            }
        }

        private void HideSpikes(bool force) {
            ViewManager.Instance.SwitchToView(View.Default);
            AudioController.Instance.PlaySound2D("Violet_portal_off", MixerGroup.TableObjectsSFX);
            foreach (Animator anim in spikeAnims) {
                if (force) {
                    anim.Play("spike_hide", 0);
                }
                else {
                    anim.SetTrigger("Hide");
                }
                anim.SetLayerWeight(1, 0f);
            }
            ViewManager.Instance.SwitchToView(View.Board);
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
            if (activateVisualGameObject == null) {
                activateVisualGameObject = new("ShrineSpike_pool");
                foreach (CardSlot slot in BoardManager.Instance.OpponentSlotsCopy) {
                    GameObject obj = GameObject.Instantiate(LobOpponentUtils.ShrineBossBlackPrefab, activateVisualGameObject.transform);
                    Animator anim = obj.GetComponent<Animator>();
                    spikeAnims.Add(anim);
                    obj.transform.position = slot.transform.position + Vector3.up + Vector3.forward * 2f;
                }

                GameObject obj2 = GameObject.Instantiate(LobOpponentUtils.ShrineBossBlackPrefab, activateVisualGameObject.transform);
                Animator anim2 = obj2.GetComponent<Animator>();
                defenceSpike = anim2;
                obj2.transform.position = base.Card.Slot.transform.position + Vector3.up * 1.5f + Vector3.forward * 2f;
            }
        }

        protected override IEnumerator CleanUpVisuals() {
            if (preActivated) {
                HideSpikes(true);
            }

            yield return new WaitForSeconds(0.5f);
            Destroy(activateVisualGameObject);
        }
    }
}

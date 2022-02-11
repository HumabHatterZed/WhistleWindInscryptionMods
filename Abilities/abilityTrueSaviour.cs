using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using HarmonyLib;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewAbility Ability_TrueSaviour()
        {
            const string rulebookName = "True Saviour";
            string rulebookDescription = "My story is nowhere, unknown to all.";
            const string dialogue = "I am death and life. Darkness and light.";

            if (ConfigHelper.Instance.RevealWhiteNight)
            {
                rulebookDescription = "Cannot die. Transform non-Terrain and non-Pelt cards into Apostles. 1-in-12 chance that the Apostle will be a Heretic.";
            }

            return WstlUtils.CreateAbility<TrueSaviour>(
                Resources.sigilTrueSaviour,
                rulebookName, rulebookDescription, dialogue, -3);
        }
    }
    public class TrueSaviour : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int softLock = 0;
        private int count = 0;
        private bool heretic = false;

        private readonly string killedDialogue = "Do not deny me.";
        private readonly string hammerDialogue = "I shall not leave thee until I have completed my mission.";
        private readonly string hereticDialogue = "Have I not chosen you, the Twelve? Yet one of you is a devil.";

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (otherCard != null)
            {
                if (!otherCard.Info.name.ToLowerInvariant().Contains("hundredsGoodDeeds") &&
                    !otherCard.Info.name.ToLowerInvariant().Contains("apostle") &&
                    otherCard != base.Card)
                {
                    if (!otherCard.Slot.IsPlayerSlot)
                    {
                        return !base.Card.Slot.IsPlayerSlot;
                    }
                    return base.Card.Slot.IsPlayerSlot;
                }
            }
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (otherCard.Info.HasTrait(Trait.Pelt) ||
                otherCard.Info.HasTrait(Trait.Terrain) ||
                otherCard.Info.SpecialAbilities.Contains(SpecialTriggeredAbility.PackMule))
            {
                softLock++;
                yield return otherCard.Die(false, base.Card);
                if(softLock >= 6)
                {
                    softLock = 0;
                    yield break;
                }
            }
            else
            {
                CardInfo randApostle = CardLoader.GetCardByName("wstl_apostleScythe");

                // 1/12 chance of being Heretic, there can only be one Heretic
                if (new System.Random().Next(0, 12) == 0 && !heretic)
                {
                    heretic = true;
                    randApostle = CardLoader.GetCardByName("wstl_apostleHeretic");
                }
                else
                {
                    switch (new System.Random().Next(0, 3))
                    {
                        case 0: // Scythe
                            break;
                        case 1: // Spear
                            randApostle = CardLoader.GetCardByName("wstl_apostleSpear");
                            break;
                        case 2: // Staff
                            randApostle = CardLoader.GetCardByName("wstl_apostleStaff");
                            break;
                    }
                }
                yield return otherCard.TransformIntoCard(randApostle);
                if (heretic && !PersistentValues.ApostleHeretic)
                {
                    PersistentValues.ApostleHeretic = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hereticDialogue, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                    yield return new WaitForSeconds(0.2f);
                }
            }
        }

        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            if (source.Info.name.ToLowerInvariant().Contains("hundredsgooddeeds"))
            {
                return false;
            }
            return true;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (base.Card.Health <= base.Card.MaxHealth)
            {
                base.Card.HealDamage(base.Card.MaxHealth - base.Card.Health);
            }
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            return true;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            yield return base.PreSuccessfulTriggerSequence();

            if (killer != null)
            {
                if (!killer.Info.name.ToLowerInvariant().Contains("hundredsgooddeeds"))
                {
                    yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);
                    yield return new WaitForSeconds(0.2f);

                    if (!PersistentValues.WhiteNightKilled)
                    {
                        PersistentValues.WhiteNightKilled = true;
                        yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(killedDialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
                        yield return new WaitForSeconds(0.2f);
                    }
                    yield return killer.Die(false, base.Card);
                }
                AudioController.Instance.PlaySound2D("mycologist_scream");
                Singleton<UIManager>.Instance.Effects.GetEffect<ScreenGlitchEffect>().SetIntensity(1f, 0.4f);
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                yield return Singleton<BoardManager>.Instance.CreateCardInSlot(base.Card.Info, base.Card.Slot, 0.15f);

                if (!PersistentValues.WhiteNightHammer)
                {
                    yield return new WaitForSeconds(0.2f);
                    PersistentValues.WhiteNightHammer = true;
                    yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(hammerDialogue, -0.65f, 0.4f, speaker: DialogueEvent.Speaker.Bonelord);
                    yield return new WaitForSeconds(0.2f);

                }
                yield return new WaitForSeconds(0.2f);
                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, toPlayer: true, 0.25f, ResourceBank.Get<GameObject>("Prefabs/Environment/ScaleWeights/Weight_RealTooth"));
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(killedDialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
            }
        }

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            if (!heretic)
            {
                return playerUpkeep;
            }
            return false;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return base.PreSuccessfulTriggerSequence();

            List<PlayableCard> list = Singleton<PlayerHand>.Instance.CardsInHand.FindAll((PlayableCard x) => x != Singleton<PlayerHand>.Instance.ChoosingSlotCard);
            while (list.Count > 0)
            {
                if (list[0].Info.name.ToLowerInvariant().Equals("wstl_hundredsgooddeeds"))
                {
                    count++;
                    if (count >= 2)
                    {
                        yield return base.PreSuccessfulTriggerSequence();

                        Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                        yield return new WaitForSeconds(0.4f);
                        list[0].Anim.StrongNegationEffect();
                        yield return new WaitForSeconds(0.4f);
                        (Singleton<PlayerHand>.Instance as PlayerHand3D).MoveCardAboveHand(list[0]);
                        Singleton<ViewManager>.Instance.SwitchToView(View.Board, false, false);

                        foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
                        {
                            // kill WhiteNight first
                            if (slot.Card.Info.name.ToLowerInvariant().Contains("whitenight"))
                            {
                                while (slot.Card != null)
                                {
                                    if (slot.Card.Health > 0)
                                    {
                                        yield return slot.Card.TakeDamage(66, list[0]);
                                        yield return new WaitForSeconds(0.4f);
                                    }
                                }
                            }
                        }
                        foreach (CardSlot slot in Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null))
                        {
                            if (slot.Card.Info.name.ToLowerInvariant().Contains("apostle") || slot.Card.Info.name.ToLowerInvariant().Contains("whitenight"))
                            {
                                while (slot.Card != null)
                                {
                                    if (slot.Card.Health > 0)
                                    {
                                        yield return slot.Card.TakeDamage(66, list[0]);
                                        yield return new WaitForSeconds(0.4f);
                                    }
                                }
                            }
                        }
                        SpecialBattleSequencer specialSequence = null;
                        var combatManager = Singleton<CombatPhaseManager>.Instance;

                        yield return combatManager.DamageDealtThisPhase += 33;

                        yield return new WaitForSeconds(0.4f);
                        yield return combatManager.VisualizeDamageMovingToScales(true);

                        int excessDamage = Singleton<LifeManager>.Instance.Balance + combatManager.DamageDealtThisPhase - 5;
                        int damage = combatManager.DamageDealtThisPhase - excessDamage;

                        yield return Singleton<LifeManager>.Instance.ShowDamageSequence(damage, damage, toPlayer: false);

                        RunState.Run.currency += excessDamage;
                        yield return combatManager.VisualizeExcessLethalDamage(excessDamage, specialSequence);
                    }
                    yield break;
                }
                list.RemoveAt(0);
            }
        }
    }
}

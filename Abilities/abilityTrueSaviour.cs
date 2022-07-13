using InscryptionAPI;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_TrueSaviour()
        {
            const string rulebookName = "True Saviour";
            string rulebookDescription = ConfigUtils.Instance.RevealWhiteNight ? "Cannot die. Transforms non-Terrain and non-Pelt cards into Apostles." : "My story is nowhere, unknown to all.";
            const string dialogue = "[c:bR]I am death and life. Darkness and light.[c:]";

            TrueSaviour.ability = WstlUtils.CreateAbility<TrueSaviour>(
                Resources.sigilTrueSaviour,
                rulebookName, rulebookDescription, dialogue, -3,
                overrideModular: true).Id;
        }
    }
    public class TrueSaviour : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private int softLock = 0;
        // Heretic doesn't exist in the player's hand or on the board
        private bool heretic = new List<PlayableCard>(Singleton<PlayerHand>.Instance.CardsInHand).FindAll((PlayableCard card) => card.Info.name == "wstl_apostleHeretic").Count == 0
            && new List<CardSlot>(Singleton<BoardManager>.Instance.AllSlotsCopy.Where(slot => slot.Card != null && slot.Card.Info.name == "wstl_apostleHeretic")).Count == 0;

        private readonly string killedDialogue = "[c:bR]Do not deny me.[c:]";
        private readonly string hammerDialogue = "[c:bR]I shall not leave thee until I have completed my mission.[c:]";
        private readonly string hereticDialogue = "[c:bR]Have I not chosen you, the Twelve? Yet one of you is [c:][c:bG]a devil[c:][c:bR].[c:]";
        private readonly string oneSinDialogue = "[c:bR]What are you doing?[c:]";

        public override bool RespondsToOtherCardResolve(PlayableCard otherCard)
        {
            if (otherCard != null && otherCard != base.Card)
            {
                if (!otherCard.Info.name.Contains("wstl_apostle") && otherCard.Info.name != "wstl_oneSins" && otherCard.Info.name != "wstl_hundredsGoodDeeds")
                {
                    return (base.Card.OnBoard && otherCard.Slot.IsPlayerSlot) ? base.Card.Slot.IsPlayerSlot : !base.Card.Slot.IsPlayerSlot;
                }
            }
            return false;
        }
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard)
        {
            yield return base.PreSuccessfulTriggerSequence();
            if (otherCard != null)
            {
                if (otherCard.Info.HasTrait(Trait.Pelt) || otherCard.Info.HasTrait(Trait.Terrain) ||
                    otherCard.Info.SpecialAbilities.Contains(SpecialTriggeredAbility.PackMule))
                {
                    if ((otherCard.HasAbility(Ability.DrawCopy) || otherCard.HasAbility(Ability.DrawCopyOnDeath)) && otherCard.HasAbility(Ability.CorpseEater))
                    {
                        otherCard.Info.RemoveBaseAbility(Ability.CorpseEater);
                    }
                    if (otherCard.HasAbility(Ability.IceCube))
                    {
                        otherCard.Info.RemoveBaseAbility(Ability.IceCube);
                    }
                    yield return otherCard.Die(false, base.Card);
                    softLock++;
                    if (softLock >= 3)
                    {
                        softLock = 0;
                        WstlPlugin.Log.LogWarning("Stuck in a loop, forcing removal of card.");
                        otherCard.UnassignFromSlot();
                        SpecialCardBehaviour[] components = otherCard.GetComponents<SpecialCardBehaviour>();
                        for (int i = 0; i < components.Length; i++)
                        {
                            components[i].OnCleanUp();
                        }
                        otherCard.ExitBoard(0.3f, Vector3.zero);
                        yield return new WaitForSeconds(0.5f);
                    }
                }
                else
                {
                    CardInfo randApostle = SeededRandom.Range(0, 3, base.GetRandomSeed()) switch
                    {
                        0 => CardLoader.GetCardByName("wstl_apostleScythe"),
                        1 => CardLoader.GetCardByName("wstl_apostleSpear"),
                        _ => CardLoader.GetCardByName("wstl_apostleStaff")
                    };
                    if (!heretic)
                    {
                        if (new System.Random().Next(0, 12) == 0)
                        {
                            heretic = true;
                            randApostle = CardLoader.GetCardByName("wstl_apostleHeretic");
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
        }

        public override bool RespondsToTakeDamage(PlayableCard source)
        {
            if (source.Info.name == "wstl_apostleHeretic")
            {
                return false;
            }
            return true;
        }
        public override IEnumerator OnTakeDamage(PlayableCard source)
        {
            yield return base.PreSuccessfulTriggerSequence();
            base.Card.HealDamage(base.Card.MaxHealth - base.Card.Health);
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
                // If killed by Heretic, die and break
                if (killer.Info.name == "wstl_apostleHeretic")
                {
                    AudioController.Instance.PlaySound2D("mycologist_scream");
                    Singleton<UIManager>.Instance.Effects.GetEffect<ScreenGlitchEffect>().SetIntensity(1f, 0.4f);
                    yield break;
                }
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
                yield return Singleton<CombatPhaseManager>.Instance.DamageDealtThisPhase += 1;
                yield return Singleton<LifeManager>.Instance.ShowDamageSequence(1, 1, toPlayer: true, 0.25f, ResourceBank.Get<GameObject>("Prefabs/Environment/ScaleWeights/Weight_RealTooth"));
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(killedDialogue, -0.65f, 0.4f, Emotion.Anger, speaker: DialogueEvent.Speaker.Bonelord);
            }
        }

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            // Return owner's turn and whether the player has Heretic in their hand
            return (base.Card.Slot.IsPlayerSlot ? playerUpkeep : !playerUpkeep) && Singleton<PlayerHand>.Instance.CardsInHand.FindAll((PlayableCard c) => c.Info.name == "wstl_apostleHeretic").Count() != 0;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.4f);
            // If all slots on the owner's side are full
            if (Singleton<BoardManager>.Instance.GetSlots(base.Card.Slot.IsPlayerSlot).Where(s => s.Card != null).Count() == 4)
            {
                int randomSeed = base.GetRandomSeed();
                List<CardSlot> cardsToKill = Singleton<BoardManager>.Instance.GetSlots(base.Card.Slot.IsPlayerSlot).FindAll((CardSlot s) => s.Card != null && s.Card != base.Card);
                PlayableCard cardToKill = cardsToKill[SeededRandom.Range(0, 3, randomSeed++)].Card;
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand);
                foreach (PlayableCard card in Singleton<PlayerHand>.Instance.CardsInHand.Where(c => c.Info.name == "wstl_apostleHeretic"))
                {
                    card.Anim.StrongNegationEffect();
                }
                yield return new WaitForSeconds(0.4f);
                Singleton<ViewManager>.Instance.SwitchToView(View.BoardCentered);
                cardToKill.Anim.SetShaking(true);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(oneSinDialogue, -0.65f, 0.4f, emotion: Emotion.Curious, speaker: DialogueEvent.Speaker.Bonelord);
                yield return cardToKill.Die(false, base.Card);
            }
        }
    }
}

using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_Healer()
        {
            const string rulebookName = "Healer";
            const string rulebookDescription = "On turn's end, heal a selected ally for 2 Health.";
            const string dialogue = "Never underestimate the importance of a healer.";
            Healer.ability = AbilityHelper.CreateAbility<Healer>(
                Artwork.sigilHealer, Artwork.sigilHealer_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 3,
                addModular: false, opponent: false, canStack: false, isPassive: false).Id;
        }
    }
    public class Healer : PlagueDoctorBase
    {
        public static Ability ability;
        public override Ability Ability => ability;
        private bool IsDoctor => base.Card.Info.name.ToLowerInvariant().Contains("plaguedoctor");

        public override string NoAlliesDialogue => "No one to heal.";
        public override string SelfTargetDialogue => "You must choose one of your other cards to heal.";
        public override string NullTargetDialogue => "You can't heal the air.";

        private readonly string failAsDoctorDialogue = "No allies to receive a blessing. [c:bR]An enemy[c:] will suffice instead.";
        private readonly string failExtraHardDialogue = "No enemies either. It seems no blessings will be given this turn.";
        private readonly string eventDialogue = "[c:bR]The time has come. A new world will come.[c:]";
        private readonly string eventDialogue2 = "[c:bR]I am death and life. Darkness and light.[c:]";
        private readonly string eventDialogue3 = "[c:bR]Rise, my servants. Rise and serve me.[c:]";
        private readonly string eventDialogueA = "[c:bR]The time has come again. I will be thy guide.[c:]";

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            return base.Card.OpponentCard != playerTurnEnd;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            yield return base.SelectionSequence();            
        }

        public override IEnumerator OnNoValidAllies()
        {
            if (!IsDoctor)
            {
                yield return CustomMethods.PlayAlternateDialogue(dialogue: NoAlliesDialogue);
                yield break;
            }
            yield return CustomMethods.PlayAlternateDialogue(dialogue: failAsDoctorDialogue);

            CardSlot randSlot;
            List<CardSlot> opposingSlots = base.Card.OpponentCard ? Singleton<BoardManager>.Instance.PlayerSlotsCopy : Singleton<BoardManager>.Instance.OpponentSlotsCopy;
            List<CardSlot> validTargets = opposingSlots.FindAll((CardSlot x) => x.Card != null && x.Card != base.Card);
            int randomSeed = SaveManager.SaveFile.GetCurrentRandomSeed() + Singleton<TurnManager>.Instance.TurnNumber;

            // If there are valid targets on the opposing side, heal a random one of their cards.
            // Else spit out another failure message then break
            if (validTargets.Count > 0)
            {
                randSlot = validTargets[SeededRandom.Range(0, validTargets.Count, randomSeed)];
                CombatPhaseManagerPatch.Instance.VisualizeConfirmSniperAbility(randSlot, false);
                yield return new WaitForSeconds(0.25f);
                randSlot.Card.HealDamage(2);
                randSlot.Card.Anim.StrongNegationEffect();
                CombatPhaseManagerPatch.Instance.VisualizeClearSniperAbility();
                base.Card.Anim.LightNegationEffect();
                ConfigManager.Instance.UpdateBlessings(1);
                UpdatePortrait();
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                base.Card.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.2f);
                yield return CustomMethods.PlayAlternateDialogue(Emotion.Anger, dialogue: failExtraHardDialogue);
                yield break;
            }
            Singleton<ViewManager>.Instance.Controller.SwitchToControlMode(Singleton<BoardManager>.Instance.DefaultViewMode, false);
            Singleton<ViewManager>.Instance.SwitchToView(Singleton<BoardManager>.Instance.CombatView, false, false);
            // Call the Clock if an opponent is healed
            yield return ClockTwelve();
        }

        public override IEnumerator OnValidTargetSelected(PlayableCard card)
        {
            card.HealDamage(2);
            card.Anim.StrongNegationEffect();
            yield return new WaitForSeconds(0.2f);
        }

        public override IEnumerator OnPostValidTargetSelected()
        {
            if (IsDoctor)
            {
                base.Card.Anim.LightNegationEffect();
                ConfigManager.Instance.UpdateBlessings(1);
                UpdatePortrait();
                yield return new WaitForSeconds(0.15f);
                yield return ClockTwelve();
            }
            yield break;
        }

        private IEnumerator ClockTwelve()
        {
            // If Blessings are between (0,11), break
            if (0 < ConfigManager.Instance.NumOfBlessings && ConfigManager.Instance.NumOfBlessings < 12)
            {
                yield break;
            }
            yield return new WaitForSeconds(0.5f);
            // If blessings are in the negatives (aka someone cheated), wag a finger and go 'nuh-uh-uh!'
            if (ConfigManager.Instance.NumOfBlessings < 0)
            {
                yield return CustomMethods.PlayAlternateDialogue(speaker: DialogueEvent.Speaker.Bonelord, dialogue: "[c:bR]Thou cannot stop my ascension. Even the tutelary bows to my authority.[c:]");
            }
            // Change Leshy's eyes to red
            LeshyAnimationController.Instance.SetEyesTexture(ResourceBank.Get<Texture>("Art/Effects/red"));
            // Transform the Doctor into Him
            yield return base.Card.TransformIntoCard(CardLoader.GetCardByName("wstl_whiteNight"));
            base.Card.Status.hiddenAbilities.Add(Ability.Flying);
            base.Card.AddTemporaryMod(new CardModificationInfo(Ability.Flying));
            yield return new WaitForSeconds(0.5f);
            CardLoader.GetCardByName("wstl_plagueDoctor").SetPortrait(WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor), WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor_emission));
            // Create dialogue depending on whether this is the first time this has happened this run
            if (!WstlSaveManager.ClockThisRun)
            {
                WstlSaveManager.ClockThisRun = true;
                yield return CustomMethods.PlayAlternateDialogue(Emotion.Neutral, DialogueEvent.Speaker.Bonelord, 0.2f, eventDialogue, eventDialogue2, eventDialogue3);
            }
            else
            {
                yield return CustomMethods.PlayAlternateDialogue(Emotion.Neutral, DialogueEvent.Speaker.Bonelord, 0.2f, eventDialogueA, eventDialogue3);
            }
            yield return new WaitForSeconds(0.2f);
            // Determine whether a Heretic is needed by seeing if One Sin exists in the player's deck
            bool sinful = new List<CardInfo>(RunState.DeckList).FindAll((CardInfo info) => info.name == "wstl_oneSin").Count() > 0;

            foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(!base.Card.OpponentCard).Where(slot => slot.Card != base.Card))
            {
                // Kill non-living/Mule card(s) and transform the rest (excluding One Sin) into Apostles
                if (slot.Card != null && slot.Card.Info.name != "wstl_oneSin")
                {
                    yield return base.ConvertToApostle(slot.Card, sinful);
                }
            }
            // If the player has One Sin
            if (sinful)
            {
                yield return new WaitForSeconds(0.5f);
                // if there is a One Sin on the board
                if (Singleton<BoardManager>.Instance.PlayerSlotsCopy.FindAll((CardSlot slot) => slot.Card != null && slot.Card.Info.name == "wstl_oneSin").Count > 0)
                {
                    foreach (CardSlot slot in Singleton<BoardManager>.Instance.PlayerSlotsCopy.Where(s => s.Card != null && s.Card.Info.name == "wstl_oneSin"))
                    {
                        // Transform the first One Sin into Heretic
                        // Remove the rest
                        if (!HasHeretic)
                        {
                            HasHeretic = true;
                            yield return slot.Card.TransformIntoCard(CardLoader.GetCardByName("wstl_apostleHeretic"));
                        }
                        else
                        {
                            slot.Card.Dead = true;
                            slot.Card.UnassignFromSlot();
                            SpecialCardBehaviour[] components = slot.Card.GetComponents<SpecialCardBehaviour>();
                            for (int i = 0; i < components.Length; i++)
                            {
                                components[i].OnCleanUp();
                            }
                            slot.Card.ExitBoard(0.3f, Vector3.zero);
                        }
                    }
                }
                else
                {
                    // Transform into Heretic
                    yield return new WaitForSeconds(0.25f);
                    foreach (PlayableCard card in Singleton<PlayerHand>.Instance.CardsInHand.Where(c => c.Info.name == "wstl_oneSin"))
                    {
                        if (!HasHeretic)
                        {
                            HasHeretic = true;
                            yield return card.TransformIntoCard(CardLoader.GetCardByName("wstl_apostleHeretic"));
                            yield return new WaitForSeconds(0.5f);
                        }
                        else
                        {
                            card.Dead = true;
                            card.UnassignFromSlot();
                            SpecialCardBehaviour[] components = card.GetComponents<SpecialCardBehaviour>();
                            for (int i = 0; i < components.Length; i++)
                            {
                                components[i].OnCleanUp();
                            }
                            card.ExitBoard(0.3f, Vector3.zero);
                        }
                    }

                }
                // Spawn card to hand if One Sin is in the deck or dead
                if (!HasHeretic)
                {
                    HasHeretic = true;
                    yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardLoader.GetCardByName("wstl_apostleHeretic"));
                }
            }
            yield return new WaitForSeconds(0.2f);
        }

        private void UpdatePortrait()
        {
            Texture2D portrait;
            Texture2D emissive;
            bool force = false;
            switch (ConfigManager.Instance.NumOfBlessings)
            {
                case 0:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor_emission);
                    break;
                case 1:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor1);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor1_emission);
                    break;
                case 2:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor2);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor2_emission);
                    break;
                case 3:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor3);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor3_emission);
                    break;
                case 4:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor4);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor4_emission);
                    break;
                case 5:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor5);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor5_emission);
                    break;
                case 6:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor6);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor6_emission);
                    break;
                case 7:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor7);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor7_emission);
                    break;
                case 8:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor8);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor8_emission);
                    break;
                case 9:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor9);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor9_emission);
                    break;
                case 10:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor10);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor10_emission);
                    break;
                default:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor11);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor11_emission);
                    force = true;
                    break;
            }

            base.Card.Info.SetPortrait(portrait, emissive);
            if (force)
            {
                base.Card.StatsLayer.SetEmissionColor(GameColors.Instance.brightNearWhite);
            }
            base.Card.UpdateStatsText();
        }
    }
}

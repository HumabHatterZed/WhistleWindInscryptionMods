using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Saves;
using InscryptionAPI.Slots;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class Mimicry : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public const string rName = "Mimicry";
        public const string rDesc = "Nothing There reveals itself after three turns on the board.";

        public override bool RespondsToTurnEnd(bool playerTurnEnd)
        {
            if (playerTurnEnd != base.PlayableCard.OpponentCard)
            {
                return base.PlayableCard.Info.name == Cards.nothingThere || !base.PlayableCard.Info.name.StartsWith(Cards.nothingThere);
            }
            return false;
        }
        public override IEnumerator OnTurnEnd(bool playerTurnEnd)
        {
            if (base.PlayableCard.TurnPlayed + 2 <= TurnManager.Instance.TurnNumber)
            {
                bool faceDown = base.PlayableCard.FaceDown;
                yield return base.PlayableCard.FlipFaceUp(faceDown);

                CardInfo evolution = CardLoader.GetCardByName(Cards.nothingThereTrue);
                evolution.evolveParams = new()
                {
                    turnsToEvolve = base.PlayableCard.OpponentCard == TurnManager.Instance.IsPlayerTurn ? 3 : 2,
                    evolution = evolution.evolveParams.evolution,
                };
                foreach (Ability item in base.PlayableCard.Info.DefaultAbilities)
                {
                    evolution.Mods.Add(new CardModificationInfo(item) { fromCardMerge = true }); // Add base sigils
                }

                AudioController.Instance.PlaySound3D("trial_cave_outro#1", MixerGroup.TableObjectsSFX, base.transform.position);
                yield return base.PlayableCard.TransformIntoCard(evolution, preTransformCallback: base.PlayableCard.ClearAppearanceBehaviours);
                yield return new WaitForSeconds(0.25f);
                yield return DialogueHelper.PlayDialogueEvent("NothingThereReveal");

                yield return base.PlayableCard.FlipFaceDown(faceDown);
            }
        }

        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer)
        {
            if (!wasSacrifice)
            {
                return base.PlayableCard.Info.name == Cards.nothingThere || !base.PlayableCard.Info.name.StartsWith(Cards.nothingThere);
            }
            return false;
        }
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            bool faceDown = base.PlayableCard.FaceDown;
            yield return base.PlayableCard.FlipFaceUp(faceDown);

            CardInfo evolution = CardLoader.GetCardByName(Cards.nothingThereTrue);
            evolution.evolveParams = new()
            {
                turnsToEvolve = base.PlayableCard.OpponentCard == TurnManager.Instance.IsPlayerTurn ? 3 : 2,
                evolution = evolution.evolveParams.evolution,
            };
            foreach (Ability item in base.PlayableCard.Info.DefaultAbilities)
            {
                evolution.Mods.Add(new CardModificationInfo(item) { fromCardMerge = true }); // Add base sigils
            }

            AudioController.Instance.PlaySound3D("trial_cave_outro#1", MixerGroup.TableObjectsSFX, base.transform.position);
            yield return BoardManager.Instance.CreateCardInSlot(evolution, base.PlayableCard.Slot, 0.15f);
            yield return new WaitForSeconds(0.25f);
            yield return DialogueHelper.PlayDialogueEvent("NothingThereReveal");

            yield return base.PlayableCard.FlipFaceDown(faceDown);
        }

        public override bool RespondsToDrawn() => true;
        public override IEnumerator OnDrawn()
        {
            this.DisguiseInBattle();
            yield break;
        }
        public override IEnumerator OnShownForCardSelect(bool forPositiveEffect)
        {
            this.DisguiseOutOfBattle();
            yield break;
        }
        public override IEnumerator OnSelectedForDeckTrial()
        {
            this.DisguiseOutOfBattle();
            yield break;
        }

        public override void OnShownInDeckReview() => DisguiseOutOfBattle();
        public override void OnShownForCardChoiceNode() => DisguiseAsCardChoice();

        private void DisguiseInBattle()
        {
            CardModificationInfo mod = GetNothingThereMod();
            CardInfo disguise = CardLoader.GetCardByName(mod?.singletonId.Replace("NothingThere:", "") ?? Cards.nothingThere);
            this.DisguiseAsCard(disguise);
            base.PlayableCard.AddPermanentBehaviour<Mimicry>();
        }

        private void DisguiseOutOfBattle()
        {
            CardModificationInfo mod = GetNothingThereMod();
            CardInfo disguise = CardLoader.GetCardByName(mod?.singletonId.Replace("NothingThere:", "") ?? Cards.nothingThere);
            this.DisguiseAsCard(disguise);
        }

        private void DisguiseAsCardChoice() // for when first choosing Nothing There
        {
            // valid initial card options are cards without special abilities or traits
            List<CardInfo> options = LobotomyCardLoader.GetUnlockedModCards(CardMetaCategory.ChoiceNode)
                .Where(x => x.SpecialAbilities.Count == 0 && x.traits.Count == 0).ToList();

            CardInfo disguise = CardLoader.GetCardByName(options.Count > 0 ? options.GetSeededRandom(Environment.TickCount).name : Cards.nothingThere);

            CardModificationInfo cardModificationInfo = new();
            cardModificationInfo.singletonId = "NothingThere";
            disguise.Mods.Add(cardModificationInfo);
            this.DisguiseAsCard(disguise);
        }

        private void DisguiseAsCard(CardInfo disguise)
        {
            base.Card.ClearAppearanceBehaviours();
            base.Card.SetInfo(disguise);
        }

        public static CardInfo GetNothingThereInDeck()
        {
            return RunState.Run.playerDeck.Cards.Find(x => x.name == "wstl_nothingThere");
        }
        public static CardModificationInfo GetNothingThereMod()
        {
            return GetNothingThereInDeck()?.Mods.Find(x => HelperMethods.StartsWithSingleton(x.singletonId, "NothingThere:"));
        }
    }

    public class RulebookEntryMimicry : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class Abilities
    {
        private static void Rulebook_Mimicry()
            => RulebookEntryMimicry.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryMimicry>(Mimicry.rName, Mimicry.rDesc).Id;
        private static void AddSpecial_Mimicry()
            => Mimicry.specialAbility = AbilityHelper.CreateSpecialAbility<Mimicry>(LobotomyPlugin.pluginGuid, Mimicry.rName).Id;
    }
}

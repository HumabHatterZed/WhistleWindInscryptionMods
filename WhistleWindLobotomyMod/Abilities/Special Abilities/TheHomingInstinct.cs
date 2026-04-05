using Core.Helpers;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;


namespace WhistleWindLobotomyMod {
    [HarmonyPatch]
    public class TheHomingInstinct : SpecialCardBehaviour {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public const string rName = "The Homing Instinct";
        public const string rDesc = "When The Road Home is played, create a Scaredy Cat in your hand. [define:wstl_scaredyCat]";

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            if (BoardManager.Instance.CardsOnBoard.Exists(x => x.Info.name == Cards.scaredyCat)) {
                yield break;
            }

            if (base.PlayableCard.OpponentCard) {
                if (!TurnManager.Instance.Opponent.Queue.Exists(x => x.Info.name == Cards.scaredyCat)) {
                    CardInfo CardToDraw = CardLoader.GetCardByName(Cards.scaredyCat);
                    ModifySpawnedCard(CardToDraw);
                    yield return CombatHelpers.QueueCreatedCard(CardToDraw);
                }
            }
            else if (!PlayerHand.Instance.CardsInHand.Exists(x => x.Info.name == Cards.scaredyCat)) {
                CardInfo CardToDraw = CardLoader.GetCardByName(Cards.scaredyCat);
                ModifySpawnedCard(CardToDraw);
                yield return CreateDrawnCard(CardToDraw);
            }
        }
        private IEnumerator CreateDrawnCard(CardInfo CardToDraw) {
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardToDraw);
            yield return new WaitForSeconds(0.45f);

        }
        private void ModifySpawnedCard(CardInfo card) {
            StatusEffectPatches.ModifySpawnedCardStatusEffects(this.PlayableCard, card, YellowBrickRoad.ID);
        }
    }
    public class RulebookEntryTheHomingInstinct : AbilityBehaviour {
        public static Ability ID { get; internal set; }
        public override Ability Ability => ID;
    }
    public partial class Abilities {
        private static void Rulebook_TheHomingInstinct()
            => RulebookEntryTheHomingInstinct.ID = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryTheHomingInstinct>(TheHomingInstinct.rName, TheHomingInstinct.rDesc).Id;
        private static void AddSpecial_TheHomingInstinct()
            => TheHomingInstinct.specialAbility = AbilityHelper.CreateSpecialAbility<TheHomingInstinct>(LobotomyPlugin.pluginGuid, TheHomingInstinct.rName).Id;
    }
}

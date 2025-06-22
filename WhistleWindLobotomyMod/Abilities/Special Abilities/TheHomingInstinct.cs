using Core.Helpers;
using DiskCardGame;
using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
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
            CardInfo CardToDraw = CardLoader.GetCardByName(Cards.scaredyCat);
            ModifySpawnedCard(CardToDraw);

            if (base.PlayableCard.OpponentCard)
                yield return CombatHelpers.QueueCreatedCard(CardToDraw);
            else
                yield return CreateDrawnCard(CardToDraw);
        }
        private IEnumerator CreateDrawnCard(CardInfo CardToDraw) {
            //yield return HelperMethods.ChangeCurrentView(View.Default);
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardToDraw);
            yield return new WaitForSeconds(0.45f);

        }
        private void ModifySpawnedCard(CardInfo card) {
            List<Ability> abilities = base.PlayableCard.Info.Abilities;
            foreach (CardModificationInfo temporaryMod in base.PlayableCard.TemporaryMods)
                abilities.AddRange(temporaryMod.abilities);

            abilities.RemoveAll((Ability x) => x == YellowBrickRoad.ability);
            if (abilities.Count > 0) {
                if (abilities.Count > 4)
                    abilities.RemoveRange(3, abilities.Count - 4);

                CardModificationInfo cardModificationInfo = new() {
                    fromCardMerge = true,
                    abilities = abilities
                };
                card.Mods.Add(cardModificationInfo);
            }
        }
    }
    public class RulebookEntryTheHomingInstinct : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class Abilities {
        private static void Rulebook_TheHomingInstinct()
            => RulebookEntryTheHomingInstinct.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryTheHomingInstinct>(TheHomingInstinct.rName, TheHomingInstinct.rDesc).Id;
        private static void AddSpecial_TheHomingInstinct()
            => TheHomingInstinct.specialAbility = AbilityHelper.CreateSpecialAbility<TheHomingInstinct>(LobotomyPlugin.pluginGuid, TheHomingInstinct.rName).Id;
    }
}

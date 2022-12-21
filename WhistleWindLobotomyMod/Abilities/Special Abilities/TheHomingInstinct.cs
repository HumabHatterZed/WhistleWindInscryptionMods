using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class TheHomingInstinct : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public static readonly string rName = "The Homing Instinct";
        public static readonly string rDesc = "When The Road Home is played, create a Scaredy Cat in your hand. [define:wstl_scaredyCat]";
        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard()
        {
            CardInfo CardToDraw = CardLoader.GetCardByName("wstl_scaredyCat");
            ModifySpawnedCard(CardToDraw);

            if (base.PlayableCard.OpponentCard)
                yield return HelperMethods.QueueCreatedCard(CardToDraw);
            else
                yield return CreateDrawnCard(CardToDraw);
        }
        private IEnumerator CreateDrawnCard(CardInfo CardToDraw)
        {
            if (Singleton<ViewManager>.Instance.CurrentView != View.Default)
            {
                yield return new WaitForSeconds(0.2f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Default);
                yield return new WaitForSeconds(0.2f);
            }
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardToDraw);
            yield return new WaitForSeconds(0.45f);
        }
        private void ModifySpawnedCard(CardInfo card)
        {
            List<Ability> abilities = base.Card.Info.Abilities;
            foreach (CardModificationInfo temporaryMod in base.PlayableCard.TemporaryMods)
            {
                abilities.AddRange(temporaryMod.abilities);
            }
            abilities.RemoveAll((Ability x) => x == YellowBrickRoad.ability);
            if (abilities.Count > 4)
            {
                abilities.RemoveRange(3, abilities.Count - 4);
            }
            CardModificationInfo cardModificationInfo = new CardModificationInfo();
            cardModificationInfo.fromCardMerge = true;
            cardModificationInfo.abilities = abilities;
            card.Mods.Add(cardModificationInfo);
        }
    }
    public class RulebookEntryTheHomingInstinct : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_TheHomingInstinct()
        {
            RulebookEntryTheHomingInstinct.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryTheHomingInstinct>(TheHomingInstinct.rName, TheHomingInstinct.rDesc).Id;
        }
        private void SpecialAbility_TheHomingInstinct()
        {
            TheHomingInstinct.specialAbility = AbilityHelper.CreateSpecialAbility<TheHomingInstinct>(pluginGuid, TheHomingInstinct.rName).Id;
        }
    }
}

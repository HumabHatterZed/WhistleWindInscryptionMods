using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void Ability_GiftGiver()
        {
            const string rulebookName = "Gift Giver";
            const string rulebookDescription = "When this card is played, create a random card in your hand.";
            const string dialogue = "A gift for you.";

            GiftGiver.ability = WstlUtils.CreateAbility<GiftGiver>(
                Resources.sigilGiftGiver,
                rulebookName, rulebookDescription, dialogue, 3).Id;
        }
    }
    public class GiftGiver : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        private bool IsLaetitia => base.Card.Info.name.ToLowerInvariant().Contains("laetitia");

        public override bool RespondsToResolveOnBoard()
        {
            return true;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            yield return base.PreSuccessfulTriggerSequence();
            yield return new WaitForSeconds(0.2f);
            base.Card.Anim.LightNegationEffect();
            yield return CreateDrawnCard();
        }

        private IEnumerator CreateDrawnCard()
        {
            if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
            {
                yield return new WaitForSeconds(0.2f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand);
                yield return new WaitForSeconds(0.2f);
            }
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(CardToDraw);
            yield return new WaitForSeconds(0.45f);
            yield return base.LearnAbility(0.1f);
            Singleton<ViewManager>.Instance.SwitchToView(View.Default);
        }
        private CardInfo CardToDraw
        {
            get
            {
                if (this.IsLaetitia)
                {
                    return CardLoader.GetCardByName("wstl_laetitiaFriend");
                }
                List<CardInfo> list = ScriptableObjectLoader<CardInfo>.AllData.FindAll((CardInfo x) => x.metaCategories.Contains(CardMetaCategory.ChoiceNode));
                return list[SeededRandom.Range(0, list.Count, base.GetRandomSeed())];
            }
        }
    }
}

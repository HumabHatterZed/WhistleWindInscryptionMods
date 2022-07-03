using InscryptionAPI;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Nettles()
        {
            const string rulebookName = "Nettles";
            const string rulebookDescription = "Adds 1 random Brother card to your hand when played.";
            DreamOfBlackSwan.specialAbility = WstlUtils.CreateSpecialAbility<DreamOfBlackSwan>(rulebookName, rulebookDescription).Id;
        }
    }
    public class DreamOfBlackSwan : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public override bool RespondsToResolveOnBoard()
        {
            return !this.PlayableCard.OpponentCard;
        }
        public override IEnumerator OnResolveOnBoard()
        {
            if (Singleton<ViewManager>.Instance.CurrentView != View.Hand)
            {
                yield return new WaitForSeconds(0.2f);
                Singleton<ViewManager>.Instance.SwitchToView(View.Hand, false, false);
                yield return new WaitForSeconds(0.2f);
            }
            CardInfo cardInfo = CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother1");
            int rand = SeededRandom.Range(0, 6, base.GetRandomSeed());
            switch(rand)
            {
                case 0:
                    break;
                case 1:
                    cardInfo = CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother2");
                    break;
                case 2:
                    cardInfo = CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother3");
                    break;
                case 3:
                    cardInfo = CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother4");
                    break;
                case 4:
                    cardInfo = CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother5");
                    break;
                case 5:
                    cardInfo = CardLoader.GetCardByName("wstl_dreamOfABlackSwanBrother6");
                    break;
            }
            yield return Singleton<CardSpawner>.Instance.SpawnCardToHand(cardInfo, null, 0.25f, null);
        }
    }
}

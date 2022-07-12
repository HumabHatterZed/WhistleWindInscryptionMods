using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Sap()
        {
            const string rulebookName = "Sap";
            const string rulebookDescription = "May react to being sacrificed.";
            GiantTreeSap.specialAbility = WstlUtils.CreateSpecialAbility<GiantTreeSap>(rulebookName, rulebookDescription).Id;
        }
    }
    public class GiantTreeSap : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        private readonly string dialogue = "A strange gurgling sound comes from your beast's stomach.";
        private int sacrificeCount;

        public override bool RespondsToSacrifice()
        {
            return true;
        }

        public override IEnumerator OnSacrifice()
        {
            if (Random.Range(0, 10 - sacrificeCount) == 0)
            {
                sacrificeCount = 0;
                PlayableCard card = Singleton<BoardManager>.Instance.CurrentSacrificeDemandingCard;
                if (!card.HasAbility(Volatile.ability))
                {
                    card.Status.hiddenAbilities.Add(Volatile.ability);
                    card.AddTemporaryMod(new CardModificationInfo(Volatile.ability));
                }
                yield return card.Info.SetExtendedProperty("wstl:Sap", true);
                yield return new WaitForSeconds(0.25f);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f, Emotion.Curious);
                yield return new WaitForSeconds(0.25f);
            }
            else
            {
                this.sacrificeCount++;
            }
        }
    }
}

using DiskCardGame;
using System.Collections;
using UnityEngine;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Syrinx()
        {
            const string rulebookName = "Syrinx";
            const string rulebookDescription = "Transforms when sacrificed 6 times.";
            Syrinx.specialAbility = AbilityHelper.CreateSpecialAbility<Syrinx>(rulebookName, rulebookDescription).Id;
        }
    }
    public class Syrinx : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        private readonly string dialogue = "As you cut into the beast's flesh, it lets out a piercing cry.";
        private int sacrificeCount;

        public override bool RespondsToSacrifice()
        {
            return true;
        }

        public override IEnumerator OnSacrifice()
        {
            this.sacrificeCount++;

            if (this.sacrificeCount >= 6)
            {
                yield return new WaitForSeconds(0.25f);
                CardInfo cardByName = CardLoader.GetCardByName("wstl_namelessFetusAwake");
                yield return base.PlayableCard.TransformIntoCard(cardByName);
                yield return Singleton<TextDisplayer>.Instance.ShowUntilInput(dialogue, -0.65f, 0.4f, Emotion.Laughter);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}

using System.Collections;
using DiskCardGame;
using UnityEngine;
using APIPlugin;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class Plugin
    {
        private NewSpecialAbility SpecialAbility_Fetus()
        {
            const string rulebookName = "Fetus";
            const string rulebookDescription = "Will awaken after sacrificed six times.";
            return WstlUtils.CreateSpecialAbility<NamelessFetus>(
                AbilitiesUtil.LoadAbilityIcon("None"),
                rulebookName, rulebookDescription, false, false, false);
        }
    }
    public class NamelessFetus : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public static SpecialAbilityIdentifier GetSpecialAbilityId
        {
            get
            {
                return SpecialAbilityIdentifier.GetID(WhistleWindLobotomyMod.Plugin.pluginGUID, "Fetus");
            }
        }

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

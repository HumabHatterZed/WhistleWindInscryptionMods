using DiskCardGame;
using InscryptionAPI.Card;
using System.Collections;
using UnityEngine;
using Resources = WhistleWindLobotomyMod.Properties.Resources;

namespace WhistleWindLobotomyMod
{
    public partial class WstlPlugin
    {
        private void SpecialAbility_Bless()
        {
            const string rulebookName = "Bless";
            const string rulebookDescription = "Changes appearances based on the number of times its ability has successfully activated. Will heal opposing cards if no allies exist.";
            Bless.specialAbility = AbilityHelper.CreateSpecialAbility<Bless>(rulebookName, rulebookDescription).Id;
        }
    }
    public class Bless : SpecialCardBehaviour
    {
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static SpecialTriggeredAbility specialAbility;

        public override bool RespondsToDrawn()
        {
            return true;
        }
        public override IEnumerator OnDrawn()
        {
            this.DisguiseInBattle();
            yield break;
        }
        public override IEnumerator OnShownForCardSelect(bool forPositiveEffect)
        {
            this.UpdatePortrait();
            yield break;
        }
        public override IEnumerator OnSelectedForDeckTrial()
        {
            this.UpdatePortrait();
            yield break;
        }
        public override void OnShownInDeckReview()
        {
            this.UpdatePortrait();
        }
        public override void OnShownForCardChoiceNode()
        {
            this.UpdatePortrait();
        }
        private void DisguiseInBattle()
        {
            this.UpdatePortrait();
            base.PlayableCard.AddPermanentBehaviour<Bless>();
            base.PlayableCard.ApplyAppearanceBehaviours(new() { ForcedWhite.appearance });
        }
        private void UpdatePortrait()
        {
            Texture2D portrait;
            Texture2D emissive;

            switch (ConfigManager.Instance.NumOfBlessings)
            {
                case 0:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor_emission);
                    break;
                case 1:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor1);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor1_emission);
                    break;
                case 2:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor2);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor2_emission);
                    break;
                case 3:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor3);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor3_emission);
                    break;
                case 4:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor4);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor4_emission);
                    break;
                case 5:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor5);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor5_emission);
                    break;
                case 6:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor6);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor6_emission);
                    break;
                case 7:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor7);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor7_emission);
                    break;
                case 8:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor8);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor8_emission);
                    break;
                case 9:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor9);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor9_emission);
                    break;
                case 10:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor10);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor10_emission);
                    break;
                default:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor11);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Resources.plagueDoctor11_emission);
                    break;
            }
            base.Card.ClearAppearanceBehaviours();
            base.Card.Info.SetPortrait(portrait, emissive);
        }
    }
}

using InscryptionAPI;
using InscryptionAPI.Card;
using DiskCardGame;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Properties;

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
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor_emission);
                    break;
                case 1:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor1);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor1_emission);
                    break;
                case 2:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor2);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor2_emission);
                    break;
                case 3:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor3);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor3_emission);
                    break;
                case 4:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor4);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor4_emission);
                    break;
                case 5:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor5);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor5_emission);
                    break;
                case 6:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor6);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor6_emission);
                    break;
                case 7:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor7);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor7_emission);
                    break;
                case 8:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor8);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor8_emission);
                    break;
                case 9:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor9);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor9_emission);
                    break;
                case 10:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor10);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor10_emission);
                    break;
                default:
                    portrait = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor11);
                    emissive = WstlTextureHelper.LoadTextureFromResource(Artwork.plagueDoctor11_emission);
                    break;
            }
            base.Card.ClearAppearanceBehaviours();
            base.Card.Info.SetPortrait(portrait, emissive);
        }
    }
}

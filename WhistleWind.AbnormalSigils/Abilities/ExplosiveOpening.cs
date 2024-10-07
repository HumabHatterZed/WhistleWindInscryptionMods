using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_ExplosiveOpening()
        {
            const string rulebookName = "Explosive Opening";
            const string rulebookDescription = "When this card is played, adjacent and opposing cards are dealt 10 damage.";
            const string dialogue = "A bitter grudge laid bare.";

            AbilityManager.FullAbility ab = AbnormalAbilityHelper.CreateAbility<ExplosiveOpening>(
                "sigilExplosiveOpening",
                rulebookName, rulebookDescription, dialogue, powerLevel: 1,
                modular: false, opponent: true, canStack: false)
                .SetPart3Rulebook()
                .SetGrimoraRulebook()
                .SetMagnificusRulebook();
            ab.SetCustomFlippedTexture(TextureLoader.LoadTextureFromFile("sigilExplosiveOpening_flipped.png"));
            
            ExplosiveOpening.ability = ab.Id;
        }
    }
    public class ExplosiveOpening : ExplodeOnDeath
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToResolveOnBoard() => true;
        public override bool RespondsToPreDeathAnimation(bool wasSacrifice) => false;
        public override IEnumerator OnResolveOnBoard() => base.OnPreDeathAnimation(false);
    }
}

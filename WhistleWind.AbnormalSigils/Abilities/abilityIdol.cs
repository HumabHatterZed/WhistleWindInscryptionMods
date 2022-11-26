using DiskCardGame;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Linq;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.AbnormalSigils.Properties;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Ability_Idol()
        {
            const string rulebookName = "Idol";
            const string rulebookDescription = "While this card is on the board, all opposing cards lose 1 Power.";
            const string dialogue = "My beasts defer to you.";

            Idol.ability = AbnormalAbilityHelper.CreateAbility<Idol>(
                Artwork.sigilIdol, Artwork.sigilIdol_pixel,
                rulebookName, rulebookDescription, dialogue, powerLevel: 5,
                modular: false, opponent: false, canStack: false).Id;
        }
    }
    public class Idol : AbilityBehaviour, IPassiveAttackBuff
    {
        public static Ability ability;
        public override Ability Ability => ability;
        public override bool RespondsToResolveOnBoard() => ActivateOnPlay();
        public override bool RespondsToOtherCardResolve(PlayableCard otherCard) => ActivateOnPlay();

        public override IEnumerator OnResolveOnBoard() => base.LearnAbility(0.5f);
        public override IEnumerator OnOtherCardResolve(PlayableCard otherCard) => base.LearnAbility(0.5f);
        public int GetPassiveAttackBuff(PlayableCard target)
        {
            if (this.Card.OnBoard && target.OpponentCard != base.Card.OpponentCard)
                return -1;

            return 0;
        }
        public bool ActivateOnPlay()
        {
            if (base.Card.Slot != null)
                return Singleton<BoardManager>.Instance.GetSlots(base.Card.OpponentCard).Where(s => s.Card != null).Count() > 0;

            return false;
        }
    }
}

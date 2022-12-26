using DiskCardGame;
using System.Collections;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;

namespace WhistleWindLobotomyMod
{
    public class MagicBullet : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility specialAbility;
        public SpecialTriggeredAbility SpecialAbility => specialAbility;

        public static readonly string rName = "Magic Bullet";
        public static readonly string rDesc = "After Der Freischütz attacks 6 times, it will attack all other ally cards on the board.";

        private int freischutzShots;
        public override bool RespondsToDealDamage(int amount, PlayableCard target) => amount > 0;
        public override bool RespondsToDealDamageDirectly(int amount) => amount > 0;
        public override IEnumerator OnDealDamage(int amount, PlayableCard target) => SeventhBullet();
        public override IEnumerator OnDealDamageDirectly(int amount) => SeventhBullet();

        private IEnumerator SeventhBullet()
        {
            freischutzShots++;
            if (freischutzShots >= 6)
            {
                freischutzShots = 0;
                yield return new WaitForSeconds(0.5f);
                base.PlayableCard.Anim.StrongNegationEffect();
                yield return new WaitForSeconds(0.5f);

                yield return DialogueEventsManager.PlayDialogueEvent("DerFreischutzSeventhBullet");

                foreach (var slot in Singleton<BoardManager>.Instance.GetSlots(!base.PlayableCard.OpponentCard).Where(slot => slot.Card != base.Card))
                {
                    if (slot.Card != null)
                        yield return slot.Card.TakeDamage(base.PlayableCard.Attack, base.PlayableCard);
                }
            }
        }
    }
    public class RulebookEntryMagicBullet : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;
    }
    public partial class LobotomyPlugin
    {
        private void Rulebook_MagicBullet()
        {
            RulebookEntryMagicBullet.ability = LobotomyAbilityHelper.CreateRulebookAbility<RulebookEntryMagicBullet>(MagicBullet.rName, MagicBullet.rDesc).Id;
        }
        private void SpecialAbility_MagicBullet()
        {
            MagicBullet.specialAbility = AbilityHelper.CreateSpecialAbility<MagicBullet>(pluginGuid, MagicBullet.rName).Id;
        }
    }
}

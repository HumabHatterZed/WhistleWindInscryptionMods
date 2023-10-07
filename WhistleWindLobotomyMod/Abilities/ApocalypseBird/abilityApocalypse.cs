using DiskCardGame;
using Infiniscryption.Spells.Patchers;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using InscryptionAPI.Triggers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core.Helpers;
using WhistleWindLobotomyMod.Opponents.Apocalypse;

namespace WhistleWindLobotomyMod
{
    public class ApocalypseAbility : AbilityBehaviour
    {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToUpkeep(bool playerUpkeep)
        {
            return base.Card.HasTrait(Trait.Giant) && base.Card.OpponentCard != playerUpkeep;
        }
        public override IEnumerator OnUpkeep(bool playerUpkeep)
        {
            foreach (CardSlot item in Singleton<BoardManager>.Instance.PlayerSlotsCopy)
            {
                PlayableCard playerCard = item.Card;
                if (playerCard == null || !playerCard.IsAffectedByTidalLock())
                    continue;

                Singleton<ViewManager>.Instance.SwitchToView(View.Board);
                yield return new WaitForSeconds(0.25f);
                yield return playerCard.Die(false);
                yield return new WaitForSeconds(0.1f);
                Singleton<ViewManager>.Instance.SwitchToView(View.OpponentQueue);
                yield return new WaitForSeconds(0.1f);

                base.Card.HealDamage(1);

                if (!base.HasLearned)
                {
                    yield return new WaitForSeconds(1f);
                    yield return base.LearnAbility();
                }
                else
                {
                    yield return new WaitForSeconds(0.5f);
                }
            }
        }
    }

    public partial class LobotomyPlugin
    {
        private void Ability_Apocalypse()
        {
            const string rulebookName = "Monster in the Black Forest";
            ApocalypseAbility.ability = LobotomyAbilityHelper.CreateAbility<ApocalypseAbility>(
                "sigilApocalypse", rulebookName,
                "'Once upon a time, three birds lived happily in the lush forest...'",
                "The three birds, now one, wandered vainly looking for the monster.", powerLevel: 0, canStack: false).Id;
        }
    }
}

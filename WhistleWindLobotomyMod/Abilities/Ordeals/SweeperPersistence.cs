using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.Core.Helpers;


namespace WhistleWindLobotomyMod {
    public partial class Abilities {
        private static void AddSweeperPersistence() {
            AbilityInfo info = ScriptableObject.CreateInstance<AbilityInfo>();
            info.rulebookName = "Persistent Sweeping";
            info.rulebookDescription = "Opposing creatures cannot avoid or redirect attacks from this card. At the end of the owner's turn, this card will attack adjacent non-Sweeper cards.";
            SweeperPersistence.ability = AbilityManager.Add(LobotomyPlugin.pluginGuid, info, typeof(SweeperPersistence), AbilityManager.AllAbilities.AbilityByID(Persistent.ability).Texture).Id;
        }
    }

    /// <summary>
    /// Whenever [creature] moves to a new space, create a Perfect SweeperPersistence in the old space. [define:wstl_foodPerfect]
    /// </summary>
    public class SweeperPersistence : AbilityBehaviour {
        public static Ability ability;
        public override Ability Ability => ability;

        public override bool RespondsToTurnEnd(bool playerTurnEnd) => base.Card.OpponentCard != playerTurnEnd;
        public override IEnumerator OnTurnEnd(bool playerTurnEnd) {
            List<PlayableCard> corpses = base.Card.Slot.GetAdjacentCards().Where(x => x.Info.DisplayedNameEnglish != "Sweeper").ToList();
            if (corpses.Count > 0) {
                yield return DialogueHelper.PlayDialogueEvent("OrdealPersistence");
                foreach (PlayableCard card in corpses) {
                    yield return Singleton<CombatPhaseManager3D>.Instance.SlotAttackSlot(base.Card.Slot, card.Slot);
                }
            }
        }

        public override bool RespondsToResolveOnBoard() => true;
        public override IEnumerator OnResolveOnBoard() {
            if (!base.Card.Info.Mods.Exists(x => x.singletonId == "wstl:Sweeper")) {
                base.Card.Info.Mods.Add(new(Persistent.ability) { singletonId = "wstl:Sweeper" });
            }
            base.Card.TriggerHandler.AddAbility(Persistent.ability);
            yield break;
        }

        public override int Priority => 1000;
    }
}

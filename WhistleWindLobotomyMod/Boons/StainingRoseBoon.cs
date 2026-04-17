using DiskCardGame;
using InscryptionAPI.Boons;
using InscryptionAPI.Card;
using InscryptionAPI.Helpers.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;

namespace WhistleWindLobotomyMod {
    public class StainingRoseBoon : BoonBehaviour {
        public override bool RespondsToPostBoonActivation() => !SaveManager.SaveFile.CurrentDeck.Cards.Exists(x => x.name == Cards.stainingRose4);
        public override IEnumerator OnPostBoonActivation() {
            int thirst = GetThirstInDeck();
            string cardToDraw = thirst switch {
                RoseCost.MAX_STACK => Cards.stainingRose, // at max thirst
                0 => Cards.stainingRose3, // at no thirst
                _ => Cards.stainingRose2
            };
            Singleton<PlayerHand>.Instance.Initialize();
            base.StartCoroutine(BoonsHandler.Instance.PlayBoonAnimation(Boons.RoseCurse));
            yield return new WaitForSeconds(0.25f);
            yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName(cardToDraw));
            yield return new WaitForSeconds(0.5f);
            yield return DialogueHelper.PlayDialogueEvent("StainingRoseIntro");
        }

        private int GetThirstInDeck() {
            int thirst = RoseCost.MAX_STACK;
            CardInfo info = SaveManager.SaveFile.CurrentDeck.Cards.Find(x => x.HasAbility(RoseChosen.ID));
            if (info != null) {
                thirst -= info.GetAbilityStacks(RoseChosen.ID);
            }
            //LobotomyPlugin.Log.LogInfo($"[RoseBoon] Thirst: {thirst}");
            return thirst;
        }
    }
}

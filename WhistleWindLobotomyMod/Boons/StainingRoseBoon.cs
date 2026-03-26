using DiskCardGame;
using InscryptionAPI.Boons;
using InscryptionAPI.Helpers.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using WhistleWind.AbnormalSigils.StatusEffects;

namespace WhistleWindLobotomyMod {
    public class StainingRoseBoon : BoonBehaviour {
        public override bool RespondsToPostBoonActivation() => true;
        public override IEnumerator OnPostBoonActivation() {
            Singleton<PlayerHand>.Instance.Initialize();
            yield return BoonsHandler.Instance.PlayBoonAnimation(Boons.RoseCurse);
            yield return new WaitForSeconds(0.25f);
            yield return CardSpawner.Instance.SpawnCardToHand(CardLoader.GetCardByName(Cards.stainingRose));
            yield return new WaitForSeconds(0.5f);
        }
    }
}

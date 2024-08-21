using DiskCardGame;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BonniesBakingPack
{
    public class DuckRabbitAbility : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility SpecialAbility;

        public override bool RespondsToDrawn() => true;
        public override IEnumerator OnDrawn()
        {
            DisguiseOutOfBattle();
            yield break;
        }
        public override IEnumerator OnSelectedForDeckTrial()
        {
            DisguiseOutOfBattle();
            yield break;
        }
        public override IEnumerator OnSelectedForCardRemoval() => OnSelectedForDeckTrial();
        public override IEnumerator OnSelectedForCardMergeHost() => OnSelectedForDeckTrial();
        public override IEnumerator OnShownForCardSelect(bool forPositiveEffect) => OnSelectedForDeckTrial();

        public override void OnShownInDeckReview() => DisguiseOutOfBattle();

        private void DisguiseOutOfBattle()
        {
            base.Card.Info.Mods.Add(NameMod());
            base.Card.ClearAppearanceBehaviours();
            base.Card.RenderCard();
        }

        private CardModificationInfo NameMod()
        {
            return new()
            {
                singletonId = "DuckitName",
                nameReplacement = UnityEngine.Random.RandomRangeInt(0, 6) switch
                {
                    0 => "Duck",
                    1 => "Rabbit",
                    2 => "Rabuck",
                    3 => "Dubbit",
                    4 => "Rabbick",
                    _ => null
                }
            };
        }
    }
}

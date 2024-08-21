using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.Triggers;
using Pixelplacement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BonniesBakingPack
{
    public class NineAbility : SpecialCardBehaviour
    {
        public static SpecialTriggeredAbility SpecialAbility;

        public override int Priority => 9;
        public const string NINE_LIVES_ID = "bbp_NineLives";
        public override bool RespondsToDie(bool wasSacrifice, PlayableCard killer) => true;
        public override IEnumerator OnDie(bool wasSacrifice, PlayableCard killer)
        {
            bool notInDeck;
            CardModificationInfo nineMod = base.PlayableCard.Info.Mods.Find(x => x.singletonId == NINE_LIVES_ID);
            if (notInDeck = nineMod == null)
            {
                nineMod = new() { singletonId = NINE_LIVES_ID };
                base.PlayableCard.Info.Mods.Add(nineMod);
            }
            if (nineMod.nameReplacement == null)
                nineMod.nameReplacement = "Eight";
            else
            {
                nineMod.SetNameReplacement(nineMod.nameReplacement switch
                {
                    "Eight" => "Seven",
                    "Seven" => "Six",
                    "Six" => "Five",
                    "Five" => "Four",
                    "Four" => "Three",
                    "Three" => "Two",
                    "Two" => "One",
                    "One" => "Zero",
                    _ => "Nine"
                });
            }

            if (nineMod.nameReplacement == "Zero")
            {
                nineMod.negateAbilities = new() { Ability.DrawCopyOnDeath };
                nineMod.SetAttackAndHealth(9 - base.PlayableCard.Attack, 9 - base.PlayableCard.Health)
                    .SetBonesCost(9 - base.PlayableCard.BonesCost())
                    .AddAbilities(Ability.Brittle);
            }
            else if (nineMod.nameReplacement == "Nine")
            {
                nineMod.nameReplacement = null;
                nineMod.negateAbilities = new();
                nineMod.abilities = new();
                nineMod.SetAttackAndHealth(0, 0).SetBonesCost(0);
            }
            yield break;
        }
    }
}

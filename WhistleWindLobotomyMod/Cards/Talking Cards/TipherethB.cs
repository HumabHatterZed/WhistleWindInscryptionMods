using DiskCardGame;
using InscryptionAPI.Card;
using InscryptionAPI.TalkingCards;
using InscryptionAPI.TalkingCards.Animation;
using InscryptionAPI.TalkingCards.Create;
using System.Collections.Generic;
using UnityEngine;
using WhistleWind.AbnormalSigils;
using WhistleWind.AbnormalSigils.Core.Helpers;
using WhistleWind.Core.Helpers;
using WhistleWindLobotomyMod.Core;
using WhistleWindLobotomyMod.Core.Helpers;
using static WhistleWind.Core.Helpers.TextureLoader;

namespace WhistleWindLobotomyMod {
    public class TalkingCardTipherethB : CustomPaperTalkingCard {
        public override string CardName => Cards.sephirahTipherethB;
        public override FaceInfo FaceInfo => new(voiceId: "female1_voice", blinkRate: 7f, voiceSoundPitch: 1.3f);
        public override DialogueEvent.Speaker SpeakerType => DialogueEvent.Speaker.Single;

        public static SpecialTriggeredAbility specialAbility;
        public override SpecialTriggeredAbility DialogueAbility => specialAbility;

        public override List<EmotionData> Emotions {
            get {
                Sprite face = LoadSpriteFromFile("talkingTipherethBBody.png", new Vector2(0.5f, 0f));
                FaceAnim emissionMain = MakeFaceAnim("talkingTipherethBEmission.png");

                return new()
                {
                    new(emotion: Emotion.Neutral,
                        face: face,
                        eyes: MakeFaceAnim("talkingTipherethBEyesOpen1.png", "talkingTipherethBEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingTipherethBMouthOpen1.png", "talkingTipherethBMouthClosed1.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Laughter,
                        face: face,
                        eyes: MakeFaceAnim("talkingTipherethBEyesOpen1.png", "talkingTipherethBEyesClosed1.png"),
                        mouth: MakeFaceAnim("talkingTipherethBMouthOpen2.png", "talkingTipherethBMouthClosed2.png"),
                        emission: emissionMain),
                    new(emotion: Emotion.Quiet,
                        face: face,
                        eyes: MakeFaceAnim("talkingTipherethBEyesOpen2.png", "talkingTipherethBEyesOpen2.png"),
                        mouth: MakeFaceAnim("talkingTipherethBMouthClosed1.png", "talkingTipherethBMouthClosed1.png"),
                        emission: GeneratePortrait.EmptyPortraitTuple)
                };
            }
        }

        public override string OnDrawnDialogueId => "TipherethBDrawn";
        public override string OnAttackedDialogueId => "TipherethBSacrificed";
        public override string OnSacrificedDialogueId => "TipherethBSacrificed";
        public override string OnBecomeSelectableNegativeDialogueId => "TipherethBSacrificed";
        public override string OnBecomeSelectablePositiveDialogueId => "TipherethBSacrificed";
        public override string OnPlayFromHandDialogueId => "TipherethBPlayed";

        public override Dictionary<Opponent.Type, string> OnDrawnSpecialOpponentDialogueIds => new();
        public override void OnShownForCardChoiceNode() => base.OnShownForCardChoiceNode();
    }
    public partial class Cards {
        public const string sephirahTipherethB = "wstl_sephirahTipherethB";
        public const string sephirahTipherethB_final = "wstl_sephirahTipherethB_final";
        private static void TipherethB() {
            CardManager.New(LobotomyPlugin.pluginPrefix, sephirahTipherethB_final, "Tiphereth",
                attack: 0, health: 1)
                .SetEnergyCost(3)
                .SetPortrait(TextureLoader.LoadTextureFromFile("talkingTipherethB_final.png", LobotomyPlugin.ModAssembly))
                .AddAbilities(Ability.LatchDeathShield)
                .SetOnePerDeck()
                .Build();

            CardManager.New(LobotomyPlugin.pluginPrefix, sephirahTipherethB, "Tiphereth",
                attack: 0, health: 1)
                .SetEnergyCost(3)
                .AddAbilities(UnkillableWeak.ID, Ability.LatchDeathShield)
                .SetOnePerDeck()
                .SetFinalSamsara(sephirahTipherethB_final)
                .Build();
        }
    }
    public partial class Abilities {
        private static void AddSpecial_TipherethB() {
            TalkingCardTipherethB.specialAbility = LobotomyAbilityHelper.CreatePaperTalkingCard<TalkingCardTipherethB>("TipherethB").Id;
        }
    }
}
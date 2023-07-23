using DiskCardGame;
using InscryptionAPI.Card;
using WhistleWind.Core.Helpers;

namespace WhistleWind.AbnormalSigils
{
    public partial class AbnormalPlugin
    {
        private void Card_SnowQueenIceBlock_F0137()
        {
            const string snowQueenIceBlock = "snowQueenIceBlock";
            const string snowQueenIceHeart = "snowQueenIceHeart";

            CreateCard(MakeCard(
                snowQueenIceBlock,
                "Block of Ice",
                attack: 0, health: 2)
                .SetPortraits(snowQueenIceBlock)
                .AddAbilities(Ability.Reach)
                .SetTerrain());

            CreateCard(MakeCard(
                snowQueenIceHeart,
                "Frozen Heart",
                attack: 0, health: 1)
                .SetPortraits(snowQueenIceHeart)
                .AddAbilities(FrozenHeart.ability));
        }
    }
}
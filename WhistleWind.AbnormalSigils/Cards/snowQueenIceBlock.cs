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

            CardManager.New(pluginPrefix, snowQueenIceBlock, "Block of Ice", 0, 2)
                .SetPortraits(Assembly, snowQueenIceBlock)
                .SetTerrain();

            CardManager.New(pluginPrefix, snowQueenIceHeart, "Frozen Heart", 0, 1)
                .SetPortraits(Assembly, snowQueenIceHeart)
                .AddAbilities(FrozenHeart.ability)
                .SetTerrain();
        }
    }
}
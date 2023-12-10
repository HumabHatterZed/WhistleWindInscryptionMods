using DiskCardGame;

namespace WhistleWindLobotomyMod.Opponents.Saviour
{
    public class SaviourBattleSequencer : Part1BossBattleSequencer
    {
        public static readonly string ID;/* = SpecialSequenceManager.Add(LobotomyPlugin.pluginGuid, "SaviourBattleSequencer", typeof(SaviourBattleSequencer)).Id;*/
        public override Opponent.Type BossType => SaviourBossOpponent.ID;
        public override StoryEvent DefeatedStoryEvent => LobotomyPlugin.ApocalypseBossDefeated;
        private SaviourBossOpponent Opponent => TurnManager.Instance.Opponent as SaviourBossOpponent;

        private int damageTakenThisTurn = 0;
        private int timesHitThisTurn = 0;

        // makes things more difficult based on how much damage is dealt to the boss
        private int reactiveDifficulty = 0;
        private int ReactiveDifficulty => RunState.Run.DifficultyModifier + reactiveDifficulty;
    }
}
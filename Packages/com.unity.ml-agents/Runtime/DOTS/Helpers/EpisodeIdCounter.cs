namespace MLAgents.DOTS
{
    internal static class EpisodeIdCounter
    {
        private static int _counter;
        public static int GetEpisodeId()
        {
            return _counter++;
        }
    }
}
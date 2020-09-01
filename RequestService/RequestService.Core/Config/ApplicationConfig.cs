namespace RequestService.Core.Config
{
    public class ApplicationConfig
    {

        public string ManualReferEmail { get; set; }
        public string ManualReferName { get; set; }
        public string EmailBaseUrl { get; set; }
        public int DistanceInMilesForDailyDigest { get; set; }
        public int FaceMaskChunkSize { get; set; }
        public int DaysSinceJobRequested { get; set; }
        public int DaysSinceJobStatusChanged { get; set; }
    }
}

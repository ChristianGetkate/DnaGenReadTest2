namespace DnaGenReadTest2
{
    /// <summary>
    /// Class to give the background worker some progress information
    /// </summary>
    public class ProgressInfo
    {
        /// <summary>
        /// The percentage of progress of the task.
        /// </summary>
        public int Percentage { get; set; }

        /// <summary>
        /// The status message of the task that needs to be presented to the user on the main form.
        /// </summary>
        public string StatusMessage { get; set; }


        /// <summary>
        /// Constructor of the ProgressInfo class.
        /// </summary>
        /// <param name="percentage">The percentage of the status of the task.</param>
        /// <param name="message">The message that needs to be presented to the user on the main form.</param>
        public ProgressInfo(int percentage, string message)
        {
            Percentage = percentage;
            StatusMessage = message;
        }
    }
}

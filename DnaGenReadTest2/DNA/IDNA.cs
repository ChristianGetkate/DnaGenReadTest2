namespace DnaGenReadTest2.DNA
{
    /// <summary>
    /// Interface for the machine and transport DNA
    /// </summary>
    public interface IDNA
    {
        /// <summary>
        /// Function to set the excel database location of a DNA
        /// </summary>
        /// <param name="location">The location of the excel document</param>
        public void SetExcelLocation(string location);

        /// <summary>
        /// Function to get the ECTR database code of the Excel document
        /// </summary>
        /// <returns>The code of the ECTR database</returns>
        public string GetECTRDatabaseCode();

        /// <summary>
        /// Function to get the genevalue
        /// </summary>
        /// <param name="geneName">Name of the gene</param>
        /// <returns>The value of the gene</returns>
        public string GetGeneValue(string geneName);

        /// <summary>
        /// Function to get the component name of the DNA
        /// </summary>
        /// <returns>The component name</returns>
        public string GetComponentName();
    }
}

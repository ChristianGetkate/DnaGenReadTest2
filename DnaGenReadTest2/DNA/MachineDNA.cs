namespace DnaGenReadTest2.DNA
{
    /// <summary>
    /// Class that contains DNA of the machine and all the functionality 
    /// </summary>
    public class MachineDNA : BaseDNA
    {
        /// <summary>
        /// Function to find the required database to build this machine
        /// </summary>
        /// <exception cref="Exception">Throws an exception when the underscore cannot be found in the machinename</exception>
        public void FindRequiredDatabase()
        {
            string name = GetComponentType();
            int underscoreindex = name.IndexOf('_');

            if (underscoreindex == -1)
            {
                throw new Exception("Could not find the index of the underscore in the machine: " + name);
            }

            name = name[..underscoreindex];

            ECTRDatabaseCode = Constants.MachineDatabaseMappings.ContainsKey(name)
                ? Constants.MachineDatabaseMappings[name]
                : throw new Exception("Component not found in mapping of the excel database: " + name);
        }

        /// <summary>
        /// Function to get the category of the machine
        /// </summary>
        /// <returns>The category of the machine</returns>
        public string GetCategory()
        {
            string category = dna.SelectSingleNode("./*[local-name() = 'category']").InnerText;

            if (category.Contains("VSB"))
            {
                category = "VSB";
            }
            else if (category.Contains("VP"))
            {
                category = "VPxx";
            }
            else if (category.Contains("V550"))
            {
                category = "V5xx";
            }

            return category;
        }

        /// <summary>
        /// Function to get the machine type
        /// </summary>
        /// <returns>The machine type name</returns>
        public string GetMachineType()
        {
            return GetGeneValue(GetCategory() + "_001");
        }
    }
}

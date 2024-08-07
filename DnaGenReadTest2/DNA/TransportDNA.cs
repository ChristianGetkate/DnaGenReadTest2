namespace DnaGenReadTest2.DNA
{
    /// <summary>
    /// DNA of the transport
    /// </summary>
    public class TransportDNA : BaseDNA
    {
        /// <summary>
        /// Function to find the required database for the transport
        /// </summary>
        /// <exception cref="Exception">Trows exception when the required database cannot be found</exception>
        public void FindRequiredDatabase()
        {
            string name = GetComponentType();
            int underscoreindex = name.IndexOf('_');

            if (underscoreindex == -1)
            {
                throw new Exception("Could not find the index of the underscore in the transport: " + name);
            }

            name = name[..underscoreindex];

            ECTRDatabaseCode = Constants.TransportDatabaseMappings.ContainsKey(name)
                ? Constants.TransportDatabaseMappings[name]
                : throw new Exception("Component not found in mapping of excel database: " + name);
        }

        /// <summary>
        /// Function that counts the amount of genes that exist with this name with a digit behind it
        /// </summary>
        /// <param name="GeneCode">The gene that needs to be searched</param>
        /// <returns>The amount of genes that were found</returns>
        /// <exception cref="Exception">Exception when the count is to many</exception>
        public int CountGene(string GeneCode)
        {
            int count = 0;

            if (GeneCode.Contains("##"))
            {
                while (true)
                {
                    string GeneValue = GeneCode.Replace("##", (count + 1).ToString("00"));

                    if (GetGeneValue(GeneValue) == "")
                    {
                        break;
                    }

                    count++;

                    if (count > 50)
                    {
                        throw new Exception($"Count overflow for {GeneCode}");
                    }
                }
            }
            else
            {
                while (true)
                {
                    string GeneValue = GeneCode.Replace("#", (count + 1).ToString("0"));

                    if (GetGeneValue(GeneValue) == "")
                    {
                        break;
                    }

                    count++;

                    if (count > 9)
                    {
                        throw new Exception($"Count overflow for {GeneCode}");
                    }
                }
            }

            return count;
        }
    }
}

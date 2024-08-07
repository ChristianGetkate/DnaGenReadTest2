using System.Xml;

namespace DnaGenReadTest2.DNA
{
    /// <summary>
    /// Abstract class for all the DNA classes
    /// </summary>
    public abstract class BaseDNA : IDNA
    {
        /// <summary>
        /// The XML node (The DNA)
        /// </summary>
        protected XmlNode? dna;

        /// <summary>
        /// The ECTR database code e.g. (234234323)
        /// </summary>
        protected string ECTRDatabaseCode = "";

        /// <summary>
        /// The excel location on the PC of the user
        /// </summary>
        protected string ExcelLocation = "";

        /// <summary>
        /// Function to search for a DNA value inside a node
        /// </summary>
        /// <param name="geneName">Name of the parameter that needs to be searched</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Argument cant be empty</exception>
        /// <exception cref="InvalidOperationException">DNA was empty</exception>
        public string GetGeneValue(string geneName)
        {
            if (string.IsNullOrEmpty(geneName))
            {
                throw new ArgumentException("Gene name can not be empty", nameof(geneName));
            }

            if (dna == null)
            {
                throw new InvalidOperationException("MachineDNA is empty.");
            }

            // Iterate through all 'genes' elements in the document
            XmlNodeList genesList = dna.SelectNodes("//*[local-name()='genes']");

            if (genesList == null || genesList.Count == 0)
            {
                throw new InvalidOperationException("Genes not found in DNA");
            }

            foreach (XmlNode genes in genesList)
            {
                if (geneName.Contains("xxxx"))
                {
                    string category = GetCategory(genes);
                    geneName = geneName.Replace("xxxx", category);
                }

                string xpath = $"./*[local-name()='{geneName}']";

                XmlNode? node = genes.SelectSingleNode(xpath);

                if (node != null)
                {
                    // Return the value if the node is found
                    return node.InnerText;
                }
            }

            // Return an empty string if the gene was not found in any 'genes' element
            return "";
        }

        /// <summary>
        /// Function to set the DNA
        /// </summary>
        /// <param name="dna">DNA that needs to be set</param>
        /// <exception cref="Exception">DNA cannot be empty</exception>
        public void SetDNA(XmlNode dna)
        {
            this.dna = dna ?? throw new Exception("Can't set DNA that is empty");
        }

        /// <summary>
        /// Function to get the type of the component
        /// </summary>
        /// <returns>Name of the component</returns>
        /// <exception cref="Exception">Type attribute must be present</exception>
        public string GetComponentType()
        {
            if (dna == null)
            {
                throw new Exception("GetComponentType -> DNA was not set !");
            }

            XmlAttribute? typeAttribute = dna.Attributes?["type"];

            return typeAttribute == null ? throw new Exception("Type attribute not found on root node of DNA!") : typeAttribute.Value;
        }

        /// <summary>
        ///  Function to get the name of the component
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public string GetComponentName()
        {
            if (dna == null)
            {
                throw new Exception("GetComponentType -> DNA was not set !");
            }

            string name;
            try
            {
                name = dna.SelectSingleNode("./*[local-name()='name']").InnerText;
            }
            catch
            {
                throw new Exception("GetComponentName -> failed to get name form DNA");
            }

            return string.IsNullOrEmpty(name) ? throw new Exception("GetComponentName -> failed to get name form DNA") : name;
        }

        /// <summary>
        /// Getter of the ECTRDatabaseCode
        /// </summary>
        /// <returns>The ECTRDatabaseCode in string format</returns>
        public string GetECTRDatabaseCode()
        {
            return ECTRDatabaseCode;
        }

        /// <summary>
        /// Setter of the ExcelLocation
        /// </summary>
        /// <param name="location">The location of the Excel of the machine</param>
        public void SetExcelLocation(string location)
        {
            ExcelLocation = location;
        }

        /// <summary>
        /// Getter of the excel location
        /// </summary>
        /// <returns>The excel location of the machine</returns>
        public string GetExcelLocation()
        {
            return ExcelLocation;
        }

        public string GetCategory(XmlNode genes)
        {
            string category = genes.ParentNode.SelectSingleNode("./*[local-name()='category']").InnerText;

            if (category.Contains("VSB"))
            {
                category = "VSB";
            }
            else if (category.Contains("VP"))
            {
                category = "VPxx";
            }
            return category;
        }
    }
}

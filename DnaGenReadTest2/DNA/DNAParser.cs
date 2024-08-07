using System.Xml;

namespace DnaGenReadTest2.DNA
{
    /// <summary>
    /// Class for parsing the customer DNA
    /// </summary>
    public class DNAParser
    {
        private XmlDocument DNAFile = new();
        private readonly SystemDNA SystemDNA = new();

        private string CustomerName = "No name assigned";

        private readonly Dictionary<string, MachineDNA> Machines = [];
        private readonly Dictionary<string, TransportDNA> Transports = [];

        private List<string> SelectedMachines = [];
        private List<string> SelectedTransports = [];

        /// <summary>
        /// Function to override the DNAFile in case of for example a self made DNA
        /// </summary>
        /// <param name="DNAFile">XMLdocument as input</param>
        public void SetDNADocument(XmlDocument DNAFile)
        {
            this.DNAFile = DNAFile;
        }

        public SystemDNA GetSystemDNA()
        {
            return SystemDNA;
        }

        public string GetCustomerName()
        {
            return CustomerName;
        }

        /// <summary>
        /// Setter of the selected machines
        /// </summary>
        /// <param name="selectedMachines">The selected machines</param>
        public void SetSelectedMachines(List<string> selectedMachines)
        {
            SelectedMachines = selectedMachines;
        }

        /// <summary>
        /// Setter of the selected transports
        /// </summary>
        /// <param name="selectedTransports">The selected transports</param>
        public void SetSelectedTransports(List<string> selectedTransports)
        {
            SelectedTransports = selectedTransports;
        }

        /// <summary>
        /// Function to return the selectedTransportNames
        /// </summary>
        /// <returns>The names of the selected transports</returns>
        public List<string> GetSelectedTransportsNames()
        {
            return SelectedTransports;
        }

        /// <summary>
        /// Function to return the selected machine names
        /// </summary>
        /// <returns>The selected machine names</returns>
        public List<string> GetSelectedMachineNames()
        {
            return SelectedMachines;
        }

        /// <summary>
        /// Getter of the selected machines
        /// </summary>
        /// <returns>The DNA of the machines</returns>
        public List<MachineDNA> GetSelectedMachines()
        {
            List<MachineDNA> SelectedMachines = [];
            foreach (string index in this.SelectedMachines)
            {
                SelectedMachines.Add(Machines[index]);
            }

            return SelectedMachines;
        }

        /// <summary>
        /// Function to get all the selected transports
        /// </summary>
        /// <returns>All the selected transports</returns>
        public List<TransportDNA> GetSelectedTransports()
        {
            List<TransportDNA> SelectedTransports = [];
            foreach (string index in this.SelectedTransports)
            {
                SelectedTransports.Add(Transports[index]);
            }

            return SelectedTransports;
        }

        /// <summary>
        /// Function to check if there are more than 1 modules selected by the user
        /// </summary>
        /// <returns>Returns true if there are more than 1 modules selected</returns>
        public bool MoreThanOneModuleSelected()
        {
            return SelectedMachines.Count + SelectedTransports.Count > 1;
        }

        /// <summary>
        /// Getter of the machines
        /// </summary>
        /// <returns>A list of the machines</returns>
        public Dictionary<string, MachineDNA> GetMachines()
        {
            return Machines;
        }

        /// <summary>
        /// Getter of the transports
        /// </summary>
        /// <returns>A list of the transports</returns>
        public Dictionary<string, TransportDNA> GetTransports()
        {
            return Transports;
        }

        /// <summary>
        /// Function to load a XMLfile with a filepath
        /// </summary>
        /// <param name="filePath">path of the XML file</param>
        /// <exception cref="Exception">File could not exist or something else went wrong</exception>
        public void SetDNADocument(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    DNAFile.Load(filePath);
                }
                catch (XmlException e)
                {
                    throw new Exception($"XML Error: {e.Message}");
                }
                catch (Exception e)
                {
                    throw new Exception($"Error: {e.Message}");
                }
            }
            else
            {
                throw new Exception("DNA filepath does not exist");
            }
        }

        /// <summary>
        /// Function to pre parse the DNA (getting some things ready)
        /// </summary>
        /// <exception cref="Exception">Exception when the DNA is not loaded</exception>
        public void ParseDNA()
        {
            Machines.Clear();
            Transports.Clear();

            if (DNAFile == null)
            {
                throw new Exception("DNA file is not loaded");
            }

            XmlNode? NodeX = DNAFile?.LastChild;

            if (NodeX == null)
            {
                throw new Exception("DNAFile last child is null!");
            }

            XmlNode? maindna = NodeX.SelectSingleNode("//*[local-name() = 'system']");

            if (maindna == null)
            {
                throw new Exception("Main DNA node is null!");
            }

            XmlNode? systemdna = maindna.ChildNodes.Count > 0 ? maindna.ChildNodes[0] : null;

            if (systemdna == null)
            {
                throw new Exception("System DNA node is null!");
            }

            SystemDNA.SetDNA(systemdna);

            int Numberofmachines = GetAmountOfMachines();
            int NumberofTransports = GetAmountOfTransports();

            for (int i = 1; i <= NumberofTransports; i++)
            {
                TransportDNA transportDNA = new();
                transportDNA.SetDNA(maindna.ChildNodes[i]);
                transportDNA.FindRequiredDatabase();
                Transports.Add(transportDNA.GetComponentType(), transportDNA);
            }

            for (int i = 1; i <= Numberofmachines; i++)
            {
                MachineDNA machineDNA = new();
                machineDNA.SetDNA(maindna.ChildNodes[i + NumberofTransports]);
                machineDNA.FindRequiredDatabase(); // Gets the code that needs to be extracted from ECTR
                Machines.Add(machineDNA.GetComponentType() + "_" + machineDNA.GetMachineType(), machineDNA);
            }

            XmlNode? componentNode = systemdna.SelectSingleNode("//*[local-name() = 'component']");

            if (componentNode != null && componentNode.Attributes["type"] != null)
            {
                CustomerName = componentNode.Attributes["type"].Value;
            }
            else
            {
                // Handel de situatie af als het componentNode null is of het geen 'type' attribuut heeft
                CustomerName = "DefaultCustomerName"; // Pas deze standaardwaarde aan op basis van je behoeften
            }
        }

        /// <summary>
        /// Function that gets the amount of machines
        /// </summary>
        /// <returns>The amount of machines in the DNA</returns>
        /// <exception cref="Exception">When SystemDNA is not set the function returns an exception</exception>
        private int GetAmountOfMachines()
        {
            if (SystemDNA == null)
            {
                throw new Exception("SystemDNA is not set yet! so the amount of machines cannot be read");
            }

            int NumberOfMachines = 0;

            if (int.TryParse(SystemDNA.GetGeneValue("SYS_200"), out int machineamount))
            {
                NumberOfMachines = machineamount;
            }

            return NumberOfMachines;
        }

        /// <summary>
        /// Function that gets the amount of transports in the SystemDNA
        /// </summary>
        /// <returns>The amount of transports in the SystemDNA</returns>
        /// <exception cref="Exception">When SystemDNA is not set yet the function will report an exception</exception>
        private int GetAmountOfTransports()
        {
            if (SystemDNA == null)
            {
                throw new Exception("SystemDNA is not set yet! so the amount of transports cannot be read");
            }

            int NumberOfTransports = 0;

            if (int.TryParse(SystemDNA.GetGeneValue("SYS_300"), out int transportamount))
            {
                NumberOfTransports = transportamount;
            }

            return NumberOfTransports;
        }

        /// <summary>
        /// Function to get all the names of the machines
        /// </summary>
        /// <returns>A list of the names of all the machines</returns>
        public List<string> GetMachineNames()
        {
            return new List<string>(Machines.Keys.ToList<string>());
        }

        /// <summary>
        /// Function to get all the names of the transports
        /// </summary>
        /// <returns>A list of strings of all the transport names</returns>
        public List<string> GetTransportNames()
        {
            return new List<string>(Transports.Keys.ToList<string>());
        }
    }
}

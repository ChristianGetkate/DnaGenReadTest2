namespace DnaGenReadTest2
{
    /// <summary>
    /// Static class for project constants
    /// </summary>
    public static class Constants
    {
        public static readonly Dictionary<string, string> MachineDatabaseMappings = new()
        {
            {"VBxxx",      "10000021803" },
            {"Fabricator", "10000021794" },
            {"V6xx",       "10000021795" },
            {"V32x",       "10000021796" },
            {"V80X",       "10000021798" },
            {"V808",       "10000021798" },
            {"V310MK2",    "10000022065" }, 
            {"V550",       "10000021800" },
            {"V2000",      "10000021801" },
            {"V3100",      "10000021802" },
            {"VPxxx",      "10000021804" },
            {"VSBxxx",     "10000021805" }
        };

        public static readonly Dictionary<string, string> TransportDatabaseMappings = new()
        {
            {"Transport", "10000021858" }
        };

        public static string ECTRDirectory = "C:\\SAPPLM\\sessiondir\\";
        public static int maximumAllowedDNAPaths = 10;
        public static string TransportSettingsName = "TransportSettings";
        public static string MachineSettingsName = "MachineSettings";
        public static string SolidEdgeSettingsName = "SolidEdgeSettings";
        public static string ECTRSettingsName = "ECTRSettings";
        public static string True = "true";
        public static string False = "false";

        //Blue Region Database: GENES
        public static string GenesColumn = "B";
        public static string IsMissingColumn = "M";
        public static string ElseColumn = "L";
        public static string SuffixColumn = "P";
        public static string PrefixColumn = "O";
        public static int IF1Column = 4;
        public static int AmountOfIfStatements = 4;
        public static int ExcelSearchLimit = 20000;
        public static int KeysStartrow = 3;

        //Green Region Database: VARIABLES AND EQUATIONS
        public static int VariableNameColumn = 1;
        public static int StandardValue = 4;
        public static int VariableIF1Column = 10;
        public static int InputColumn = 2;
        public static int AND1Column = 14;

        //Orange Region Database: MODULES
        public static string MinVersion = "A";
        public static string MaxVersion = "B";
        public static readonly string[] OperatorList = { "NXOR", "XOR", "NOR", "OR", "NAND", "AND", "NOT", "PATTERNBYGEN", "PATTERNFILL", "PARTNERGENE" };
        public static int Command1Column = 22;
        public static int AlternateLocationColumn = 3;
        public static int DescriptionColumn = 6;
        public static int xPosColumn = 7;
        public static int SimplifiedDocumentNumberColumn = 4;
        public static int DocumentNumberColumn = 5;
        public static string Command1ColumnText = "V";
        public static string IdentifierColumn = "U";
        public static int xOffsetColumn = 18;
        public static string ShiftVarColumn = "M";

        // Transport constants.
        public static string GeneCodeColumn = "C";
        public static string GeneValueColumn = "D";
        public static string KeysResultColumn = "D";
        public static string CTCoordinateValueColumn = "D";
        public static int maximumAllowedOutputPaths = 10;

        public static string TransportDescriptionColumn = "A";
        public static int TransportSimplifiedDocumentNumberColumn = 8;

        // Modulesheet constants
        public static string DocumentNumberColumnTransport = "G";
        public static int xcoor = 14;
        public static string referencepointColumn = "M";
        public static string PatternColumn = "L";
        public static int KeyColumn1 = 21;
        public static int KeyColumn2 = 26;
        public static string MinimumVersionTransport = "B";
        public static string MaximumVersionTransport = "C";


        // BOM constants
        public static List<string> BOMHeader = new List<string>()
        {
            "Level",
            "FileName",
            "MateriaalNumber",
            "Description",
            "Quantity",
            "Eff_Length",
            "Part_Length",
            "ChainLength"   
        };
    }
}

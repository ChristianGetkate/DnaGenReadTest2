using DnaGenReadTest2.DNA;
using System.Xml;
using TwinCAT.Ads;

namespace DnaGenReadTest2
{
    public class Program
    {
        static void Main(string[] args)
        {
            DNAParser dnaParser = new DNAParser();
            string filePath = @"C:\Users\c.getkate\Desktop\DNA Safety\11574 DNA Implicit - Tasche Staalbouw B.V. - V4_2.xml";

            dnaParser.SetDNADocument(filePath);
            dnaParser.ParseDNA();

            SystemDNA systemDNA = dnaParser.GetSystemDNA();

            Dictionary<string, LightcurtainData> lightcurtains = new Dictionary<string, LightcurtainData>();
            Dictionary<string, ResetbuttonData> resetbuttons = new Dictionary<string, ResetbuttonData>();

            // Linking the lightcurtainDevicetags to the testplugname and variablename
            int j = 0;
            for (int i = 0; i < 10; i++)
            {
                string lightcurtainDeviceTag = systemDNA.GetGeneValue($"LC_0{i}02");
               
                if (!string.IsNullOrEmpty(lightcurtainDeviceTag))
                {
                    LightcurtainData lightcurtainData = new();
                    lightcurtainData.TestPlugname = $"LC{i}_TS";
                    lightcurtainData.VariableName = $"MachineObjectsArray.SafetyTestTrolley[0].arrLightcurtains_TS[{j}]";
                    j++;

                    lightcurtains.Add(lightcurtainDeviceTag, lightcurtainData);
                }
            }

            // Linking the resetbuttonDevicetags to the testplugname and variablename
            int k = 0;
            for (int i = 0; i < 10; i++)
            {
                string resetbuttonDeviceTag = systemDNA.GetGeneValue($"RB_0{i}02");

                if (!string.IsNullOrEmpty(resetbuttonDeviceTag))
                {
                    ResetbuttonData resetbuttonData = new();
                    resetbuttonData.TestPlugname = $"RB{i}_TS";
                    resetbuttonData.VariableNameReset = $"MachineObjectsArray.SafetyTestTrolley[0].arrResetButtons_TS[{k}]";
                    resetbuttonData.VariableNameLampReset = $"MachineObjectsArray.SafetyTestTrolley[0].arrLampResetButtons_TS[{k}]";
                    k++;

                    resetbuttons.Add(resetbuttonDeviceTag, resetbuttonData);
                }
            }


            foreach (var kvp  in lightcurtains)
            {
                Console.WriteLine("Devicetag: " + kvp.Key + " Testplugname: " + kvp.Value.TestPlugname 
                                                          + " VariableName: " + kvp.Value.VariableName);
            }

            foreach (var kvp in resetbuttons)
            {
                Console.WriteLine("Devicetag: " + kvp.Key + " Testplugname: " + kvp.Value.TestPlugname
                                                          + " VariableNameReset: " + kvp.Value.VariableNameReset
                                                          + " VariableNameLampReset: " + kvp.Value.VariableNameLampReset);
            }
            
            // TwinCAT stuff here
            TwinCATConnector connector = new TwinCATConnector();
            Dictionary<string, uint> variableHandleData = new Dictionary<string, uint>();

            connector.Connect(AmsNetId.Local.ToString(), 851);

            // Add variable handles lightcurtains
            // test
            foreach (var kvp in lightcurtains)
            {
                string deviceTag = kvp.Key;
                string variableName = kvp.Value.VariableName;

                uint handle = connector.CreateVariableHandle(variableName);

                if (!variableHandleData.ContainsKey(deviceTag))
                {
                    variableHandleData.Add(deviceTag, handle);
                }
            }

            // Looping through the safety zones
            for (int i = 0; i < 5; i++)
            {
                string lightcurtainSafetyZoneTag = systemDNA.GetGeneValue($"SZ_0{i}02");
                string gatedBySafetyZoneTag = systemDNA.GetGeneValue($"SZ_0{i}07");
                string resetbuttonTags = systemDNA.GetGeneValue($"SZ_0{i}04");
                uint variableHandle;

                if (!string.IsNullOrEmpty(lightcurtainSafetyZoneTag))
                {
                    variableHandle = variableHandleData[lightcurtainSafetyZoneTag];
                    connector.WriteBool(variableHandle, true);
                }

                if (!string.IsNullOrEmpty(gatedBySafetyZoneTag))
                {
                    variableHandle = variableHandleData[gatedBySafetyZoneTag];
                    connector.WriteBool(variableHandle, true);
                }

                Thread.Sleep(1000);

                // Logging the motordrives in STO

                Thread.Sleep(500);

                // Check lamp reset


                if (!string.IsNullOrEmpty(lightcurtainSafetyZoneTag))
                {
                    variableHandle = variableHandleData[lightcurtainSafetyZoneTag];
                    connector.WriteBool(variableHandle, false);
                }

                if (!string.IsNullOrEmpty(gatedBySafetyZoneTag))
                {
                    variableHandle = variableHandleData[gatedBySafetyZoneTag];
                    connector.WriteBool(variableHandle, false);
                }
            }
            
        }
    }
}

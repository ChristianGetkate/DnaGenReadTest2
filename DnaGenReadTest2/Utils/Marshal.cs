﻿using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;

namespace DnaGenReadTest2
{
    /// <summary>
    /// Marshal class to get an active application via COM
    /// </summary>
    internal class Marshal
    {
        internal const string OLEAUT32 = "oleaut32.dll";
        internal const string OLE32 = "ole32.dll";

        [System.Security.SecurityCritical]  // auto-generated_required
        public static object GetActiveObject(string progID)
        {
            Guid clsid;

            // Call CLSIDFromProgIDEx first then fall back on CLSIDFromProgID if
            // CLSIDFromProgIDEx doesn't exist.
            try
            {
                CLSIDFromProgIDEx(progID, out clsid);
            }
            //            catch
            catch (Exception)
            {
                CLSIDFromProgID(progID, out clsid);
            }

            GetActiveObject(ref clsid, IntPtr.Zero, out object obj);
            return obj;
        }

        public static int SizeOf(Type t)
        {
            return System.Runtime.InteropServices.Marshal.SizeOf(t);
        }

        //[DllImport(Microsoft.Win32.Win32Native.OLE32, PreserveSig = false)]
        [DllImport(OLE32, PreserveSig = false)]
        [ResourceExposure(ResourceScope.None)]
        [SuppressUnmanagedCodeSecurity]
        [System.Security.SecurityCritical]  // auto-generated
        private static extern void CLSIDFromProgIDEx([MarshalAs(UnmanagedType.LPWStr)] string progId, out Guid clsid);

        //[DllImport(Microsoft.Win32.Win32Native.OLE32, PreserveSig = false)]
        [DllImport(OLE32, PreserveSig = false)]
        [ResourceExposure(ResourceScope.None)]
        [SuppressUnmanagedCodeSecurity]
        [System.Security.SecurityCritical]  // auto-generated
        private static extern void CLSIDFromProgID([MarshalAs(UnmanagedType.LPWStr)] string progId, out Guid clsid);

        //[DllImport(Microsoft.Win32.Win32Native.OLEAUT32, PreserveSig = false)]
        [DllImport(OLEAUT32, PreserveSig = false)]
        [ResourceExposure(ResourceScope.None)]
        [SuppressUnmanagedCodeSecurity]
        [System.Security.SecurityCritical]  // auto-generated
        private static extern void GetActiveObject(ref Guid rclsid, IntPtr reserved, [MarshalAs(UnmanagedType.Interface)] out object ppunk);
    }
}
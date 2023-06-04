using System;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;

[assembly: AssemblyCompany("Miloš Konečný")]
[assembly: AssemblyProduct("PDFiumDotNET")]
[assembly: AssemblyCopyright("Copyright © Miloš Konečný 2020-2023")]
[assembly: AssemblyTrademark("")]
[assembly: ComVisible(false)]

[assembly: CLSCompliant(true)]

[assembly: NeutralResourcesLanguage("en", UltimateResourceFallbackLocation.MainAssembly)]

[assembly: AssemblyVersion("1.00.01.000")]
[assembly: AssemblyFileVersion("1.0.1.0")]

#if DEBUG
[assembly: AssemblyConfiguration("Debug")]
#else
[assembly: AssemblyConfiguration("Release")]
#endif

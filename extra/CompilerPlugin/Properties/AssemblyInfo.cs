﻿using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
#if UNITY4
[assembly: AssemblyTitle("Universal C# Compiler Plugin for Unity 4")]
#elif UNITY5
[assembly: AssemblyTitle("Universal C# Compiler Plugin for Unity 5")]
#elif UNITY2017
[assembly: AssemblyTitle("Universal C# Compiler Plugin for Unity 2017")]
#endif
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("SaladLab")]
[assembly: AssemblyProduct("Unity3D.IncrementalCompiler")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("e8330f9b-ba07-4344-9067-c14b1fb76b26")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.2.9")]
[assembly: AssemblyFileVersion("1.2.9")]

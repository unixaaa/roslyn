﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildBoss
{
    internal enum RoslynProjectKind
    {
        Dll,
        Exe,
        ExeCoreClr,
        UnitTest,
        UnitTestDesktop,
        UnitTestPortable,
        CompilerGeneratorTool,
        DeploymentCompilerGeneratorTools,
        Deployment,
        DeploymentTest,
        Vsix,
        Depedency,
        Custom
    }

    internal static class RoslynProjectKindUtil
    {

        /// <summary>
        /// Convert a declared kind into the correspending enum value.
        /// </summary>
        internal static RoslynProjectKind? GetRoslynProjectKind(string value)
        {
            switch (value)
            {
                case "ExeCoreClr":
                    return RoslynProjectKind.Exe;
                case "UnitTestPortable":
                    return RoslynProjectKind.UnitTestPortable;
                case "UnitTestDesktop":
                    return RoslynProjectKind.UnitTestDesktop;
                case "UnitTest":
                    return RoslynProjectKind.UnitTest;
                case "CompilerGeneratorTool":
                    return RoslynProjectKind.CompilerGeneratorTool;
                case "DeploymentCompilerGeneratorTools":
                    return RoslynProjectKind.DeploymentCompilerGeneratorTools;
                case "Deployment":
                    return RoslynProjectKind.Deployment;
                case "DeploymentTest":
                    return RoslynProjectKind.DeploymentTest;
                case "Vsix":
                    return RoslynProjectKind.Vsix;
                case "Dependency":
                    return RoslynProjectKind.Depedency;
                case "Custom":
                    return RoslynProjectKind.Custom;
                default:
                    return null;
            }
        }

        internal static bool IsAnyUnitTest(RoslynProjectKind kind)
        {
            return
                kind == RoslynProjectKind.UnitTest ||
                kind == RoslynProjectKind.UnitTestDesktop ||
                kind == RoslynProjectKind.UnitTestPortable;
        }

        internal static bool IsDeploymentProject(RoslynProjectKind kind)
        {
            return
                kind == RoslynProjectKind.Exe ||
                kind == RoslynProjectKind.DeploymentTest ||
                IsAnyUnitTest(kind);
        }
    }

    internal struct RoslynProjectData
    {
        internal RoslynProjectKind EffectiveKind { get; }
        internal RoslynProjectKind? DeclaredKind { get; }
        internal string DeclaredValue { get; }

        internal bool IsAnyUnitTest => RoslynProjectKindUtil.IsAnyUnitTest(EffectiveKind);
        internal bool IsDeploymentProject => RoslynProjectKindUtil.IsDeploymentProject(EffectiveKind);

        internal RoslynProjectData(RoslynProjectKind effectiveKind)
        {
            EffectiveKind = effectiveKind;
            DeclaredValue = null;
            DeclaredKind = null;
        }

        internal RoslynProjectData(RoslynProjectKind effectiveKind, RoslynProjectKind declaredKind, string declaredValue)
        {
            Debug.Assert(declaredKind == RoslynProjectKindUtil.GetRoslynProjectKind(declaredValue).Value);
            EffectiveKind = effectiveKind;
            DeclaredValue = declaredValue;
            DeclaredKind = declaredKind;
        }
    }
}

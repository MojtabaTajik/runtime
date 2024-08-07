// Generated by Fuzzlyn v2.2 on 2024-07-30 17:57:54
// Run on Arm64 Windows
// Seed: 1755918021774833910-vectort,vector64,vector128,armsve
// Reduced from 111.7 KiB to 1.0 KiB in 00:00:56
// Hits JIT assert in Release:
// Assertion failed '!IsUnusedValue()' in 'Program:Main(Fuzzlyn.ExecutionServer.IRuntime)' during 'Lowering nodeinfo' (IL size 108; hash 0xade6b36b; FullOpts)
//
//     File: C:\dev\dotnet\runtime\src\coreclr\jit\gentree.cpp Line: 18154
//
using System;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using Xunit;

public class Runtime_105484
{
    private static byte[] s_1 = new byte[1];
    private static byte[,, ] s_2 = new byte[1, 1, 1];
    private static Vector<uint> s_5;

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void M()
    {
        for (int vr7 = 0; vr7 < 1; vr7++)
        {
            var vr8 = Vector128.CreateScalar(1U).AsVector();
            s_5 = Sve.AndAcross(vr8);
            try
            {
                double vr9 = s_1[0];
            }
            finally
            {
                var vr10 = (byte)1;
                var vr11 = Vector128.CreateScalar(vr10).AsVector();
                var vr12 = s_2[0, 0, 0];
                var vr13 = Vector.Create<byte>(0);
                var vr14 = Vector.Create<byte>(0);
                var vr15 = Sve.CreateMaskForFirstActiveElement(vr13, vr14);
                s_1[0] = Sve.ConditionalExtractLastActiveElement(vr11, vr12, vr15);
            }
        }
    }

    [Fact]
    public static void TestEntryPoint()
    {
        if (Sve.IsSupported)
        {
            try
            {
                M();
            }
            catch {}
        }
    }
}

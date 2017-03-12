using System;
using System.Linq;
using NuGet.Common;

namespace NuGet.Test.Utility
{
    public static class PlatformTestUtility
    {
        /// <summary>
        /// Returns a message to apply to the xunit attribute if it should be skipped.
        /// Null is returned if the test should run.
        /// </summary>
        public static string GetSkipMessageOrNull(params string[] platforms)
        {
            var current = CurrentPlatform;

            var runTest = platforms.Any(s => StringComparer.OrdinalIgnoreCase.Equals(current, s));

            if (!runTest)
            {
                var plural = platforms.Length == 1 ? "" : "s";

                return $"Test does not apply to: {current}. Target platform{plural}: {String.Join(", ", platforms)}";
            }

            return null;
        }

        /// <summary>
        /// Current platform, windows, darwin, linux
        /// </summary>
        public static string CurrentPlatform
        {
            get
            {
                return _currentPlatform.Value;
            }
        }

        public static string GetMonoMessage(bool onlyOnMono, bool skipMono)
        {
            if (onlyOnMono && !RuntimeEnvironmentHelper.IsMono)
            {
                return "This test only runs on mono.";
            }

            if (skipMono && RuntimeEnvironmentHelper.IsMono)
            {
                return "This test does not run on mono.";
            }

            return null;
        }

        private static readonly Lazy<string> _currentPlatform = new Lazy<string>(GetCurrentPlatform);

        private static string GetCurrentPlatform()
        {
            if (RuntimeEnvironmentHelper.IsWindows)
            {
                return Platform.Windows;
            }

            if (RuntimeEnvironmentHelper.IsLinux)
            {
                return Platform.Linux;
            }

            if (RuntimeEnvironmentHelper.IsMacOSX)
            {
                return Platform.Darwin;
            }

            return "UNKNOWN";
        }
    }
}

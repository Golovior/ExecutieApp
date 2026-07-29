using System;
using System.Collections.Generic;
using System.IO;

namespace WIDM_Executie
{
    // Single source of truth for the executieSettings.txt key/value schema, shared by
    // Form6 (reads settings to run a reveal) and Form7 (reads/writes settings in the UI).
    internal static class ExecutionSettingsStore
    {
        public const string ShowYellowScreensKey = "showYellowScreens";
        public const string SecondsBeforeColorKey = "secondsBeforeColor";
        public const string SecondsOfColorKey = "secondsOfColor";
        public const string SecondsBeforeYellowKey = "secondsBeforeYellow";
        public const string LampsResultDelayKey = "lamps_result_delay";
        public const string LampResultsUrlKey = "lamp_results_url";

        public static readonly string[] Keys =
        {
            ShowYellowScreensKey,
            SecondsBeforeColorKey,
            SecondsOfColorKey,
            SecondsBeforeYellowKey,
            LampsResultDelayKey,
            LampResultsUrlKey,
        };

        public static string FilePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/widmExecutie";
            return path + "/executieSettings.txt";
        }

        // Present-but-empty values are kept as "" (distinct from a key that's absent entirely) so
        // callers can decide their own fallback for "never configured" vs "explicitly left blank".
        public static Dictionary<string, string> Read(string settingsFile)
        {
            Dictionary<string, string> values = new();

            if (!File.Exists(settingsFile))
                return values;

            using StreamReader sr = File.OpenText(settingsFile);

            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                if (line == "")
                    continue;

                string[] parts = line.Split(',');
                if (parts.Length < 1)
                    continue;

                values[parts[0]] = parts.Length > 1 ? parts[1] : "";
            }

            return values;
        }

        public static bool IsValidUrl(string? url)
        {
            if (string.IsNullOrWhiteSpace(url))
                return false;

            return Uri.TryCreate(url, UriKind.Absolute, out Uri? uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}

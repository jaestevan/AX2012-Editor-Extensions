using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;

namespace JAEE.AX.EditorExtensions
{
    [Serializable]
    public class EditorSettings
    {
        private static EditorSettings settings;

        private string FILE_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "AX2012EditorSettings");
        private string FILE_NAME = "AX2012EditorSettings.set";

        public JAEEOutliningSettings Outlining;
        public JAEEHighlightWordSettings HighlightWord;
        public JAEECurrentLineHighlightSettings HighlightCurrentLine;
        public JAEEBraceMatchingSettings BraceMatching;

        /// <summary>
        /// Create instance with default values
        /// </summary>
        public EditorSettings()
        {
            Outlining = new JAEEOutliningSettings();
            HighlightWord = new JAEEHighlightWordSettings();
            HighlightCurrentLine = new JAEECurrentLineHighlightSettings();
            BraceMatching = new JAEEBraceMatchingSettings();
        }

        /// <summary>
        /// Get Singleton instance
        /// </summary>
        public static EditorSettings getInstance()
        {
            if (settings == null)
            {
                settings = new EditorSettings();
                settings.loadFromFile();
            }

            return settings;
        }

        /// <summary>
        /// Load settings from disk
        /// </summary>
        private void loadFromFile()
        {
            try
            {
                string settingsFilePath = Path.Combine(FILE_PATH, FILE_NAME);

                if (File.Exists(settingsFilePath))
                {
                    using (FileStream fs = new FileStream(settingsFilePath, FileMode.Open, FileAccess.Read))
                    {
                        if (fs != null)
                        {
                            SoapFormatter formatter = new SoapFormatter();
                            settings = formatter.Deserialize(fs) as EditorSettings;
                            fs.Close();
                        }
                    }
                }
                else
                {
                    settings = new EditorSettings();
                    settings.saveChanges();
                }
            }
            catch
            {
                settings = new EditorSettings();
            }
        }

        /// <summary>
        /// Save settings to disk
        /// </summary>
        public void saveChanges()
        {
            try
            {
                string settingsFilePath = Path.Combine(FILE_PATH, FILE_NAME);

                if (!Directory.Exists(FILE_PATH))
                    Directory.CreateDirectory(FILE_PATH);

                using (FileStream fs = File.OpenWrite(settingsFilePath))
                {
                    if (fs != null)
                    {
                        SoapFormatter formatter = new SoapFormatter();
                        formatter.Serialize(fs, settings);
                        fs.Close();
                    }
                }
            }
            catch
            {
                throw new Exception(string.Format("Error saving settings file. Be sure the folder {0} exists.", FILE_PATH));
            }
        }

    }

}

﻿using System;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace WinHue3.Settings
{
    public static class WinHueSettings
    {
        public static CustomSettings settings = new CustomSettings();
        public static CustomHotkeys hotkeys = new CustomHotkeys();
        public static CustomBridges bridges = new CustomBridges();
        private static string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static WinHueSettings()
        {
            LoadAllSettings();

        }


        /// <summary>
        /// Load all settings.
        /// </summary>
        /// <returns>True or false on all success</returns>
        public static bool LoadAllSettings()
        {
            if (LoadBridges() && LoadHotkeys() && LoadSettings()) return true;
            return false;
        }

        /// <summary>
        /// Save all settings
        /// </summary>
        /// <returns>True or false on all success</returns>
        public static bool SaveAllSettings()
        {
            if (SaveBridges() && SaveHotkeys() && SaveSettings()) return true;
            return false;
        }

        /// <summary>
        /// Create the WinHue folders in the Application data folder.
        /// </summary>
        /// <returns>True or false on success</returns>
        private static bool CreateWinHueDirectory()
        {
            try
            {
                if (!Directory.Exists(Path.Combine(path, "WinHue")))
                {
                    log.Debug($@"Trying to create WinHue directory in {Path.Combine(path, "WinHue")}");
                    Directory.CreateDirectory(Path.Combine(path, "WinHue"));
                    log.Debug("Directory created succesfully.");
                    return true;
                }
                else
                {
                    log.Info("WinHue Folder already exists.");
                }
                return true;
            }
            catch (Exception)
            {
                log.Error($"Unable to created WinHue directory in {path}. Please check that you have the permissions to this path.");
                return false;
            }

        }

        /// <summary>
        /// Save the Hotkeys to file.
        /// </summary>
        /// <returns>True or false on success</returns>
        public static bool SaveHotkeys()
        {
            bool ret = false;
            try
            {
                string result = JsonConvert.SerializeObject(hotkeys, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented});
                log.Debug($@"Saving hotkeys : {hotkeys}");
                string filepath = Path.Combine(path, "WinHue\\WinHueHotkeys.set");
                if (CreateWinHueDirectory())
                {
                    File.WriteAllText(filepath, result);
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                settings = new CustomSettings();
                ret = false;
                log.Error("Error while saving the hotkeys.", ex);

            }
            return ret;
        }

        /// <summary>
        /// Load hotkeys from file
        /// </summary>
        /// <returns>True or false on success</returns>
        public static bool LoadHotkeys()
        {
            bool result = false;

            try
            {
                string filepath = Path.Combine(path, "WinHue\\WinHueHotkeys.set");
                if (!File.Exists(filepath)) return result;
                log.Debug("Trying to open hotkeys file...");
                StreamReader sr = File.OpenText(filepath);
                log.Debug("File open.");
                string settingsString = sr.ReadToEnd();
                log.Debug($@"Loading hotkeys : {settingsString}");
                sr.Close();
                log.Debug("Deserializing the settings file.");
                hotkeys = JsonConvert.DeserializeObject<CustomHotkeys>(settingsString, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                result = true;
            }
            catch (Exception ex)
            {
                settings = new CustomSettings();
                result = false;
                log.Error("Error while loading the hotkeys.", ex);
            }
            return result;
        }

        /// <summary>
        /// Save Bridge to file.
        /// </summary>
        /// <returns>True or false on success</returns>
        public static bool SaveBridges()
        {
            bool ret = false;
            try
            {
                string result = JsonConvert.SerializeObject(bridges, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });
                log.Debug($@"Saving bridge settings : {bridges}");
                string filepath = Path.Combine(path, "WinHue\\WinHueBridges.set");
                if (CreateWinHueDirectory())
                {
                    File.WriteAllText(filepath, result);
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                settings = new CustomSettings();
                ret = false;
                log.Error("Error while saving the bridge settings.", ex);

            }
            return ret;
        }

        /// <summary>
        /// Load bridge from file.
        /// </summary>
        /// <returns></returns>
        public static bool LoadBridges()
        {
            bool result = false;

            try
            {
                string filepath = Path.Combine(path, "WinHue\\WinHueBridges.set");
                if (!File.Exists(filepath)) return result;
                log.Debug("Trying to open bridge settings file...");
                StreamReader sr = File.OpenText(filepath);
                log.Debug("File open.");
                string settingsString = sr.ReadToEnd();
                log.Debug($@"Loading bridge settings : {settingsString}");
                sr.Close();
                log.Debug("Deserializing the settings file.");
                bridges = JsonConvert.DeserializeObject<CustomBridges>(settingsString, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                result = true;
            }
            catch (Exception ex)
            {
                settings = new CustomSettings();
                result = false;
                log.Error("Error while loading the bridge settings.", ex);
            }
            return result;
        }

        /// <summary>
        /// Save the settings to disk.
        /// </summary>
        /// <returns>True or false succeeded.</returns>
        public static bool SaveSettings()
        {
            bool ret = false;
            try
            {
                string result = JsonConvert.SerializeObject(settings ,new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore, Formatting = Formatting.Indented });
                log.Debug($@"Saving settings : {settings}");
                string filepath = Path.Combine(path, "WinHue\\WinHueSettings.set");
                if (CreateWinHueDirectory())
                {
                    File.WriteAllText(filepath, result);
                    ret = true;
                }
            }
            catch (Exception ex)
            {
                settings = new CustomSettings();
                ret = false;
                log.Error("Error while saving the settings.",ex);

            }
            return ret;
        }

        /// <summary>
        /// Reload the settings from disk.
        /// </summary>
        /// <returns>True or false succeeded.</returns>
        public static bool LoadSettings()
        {
            bool result = false;
            
            try
            {
                string filepath = Path.Combine(path, "WinHue\\WinHueSettings.set");
                if (!File.Exists(filepath)) return result;
                log.Debug("Trying to open settings file...");
                StreamReader sr = File.OpenText(filepath);
                log.Debug("File open.");
                string settingsString = sr.ReadToEnd();
                log.Debug($@"Loading settings : {settingsString}");
                sr.Close();         
                log.Debug("Deserializing the settings file.");    
                settings =  JsonConvert.DeserializeObject<CustomSettings>(settingsString,new JsonSerializerSettings() {NullValueHandling = NullValueHandling.Ignore});
                result = true;
            }
            catch (Exception ex)
            {
                settings = new CustomSettings();
                result = false;
                log.Error("Error while loading the settings.",ex);
            }
            return result;
        }
    }



}
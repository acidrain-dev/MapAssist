﻿using MapAssist.Settings;
using System;
using System.IO;
using System.Media;

namespace MapAssist.Helpers
{
    public class AudioPlayer
    {
        private static DateTime _itemAlertLastPlayed = DateTime.MinValue;
        private static SoundPlayer _itemAlertPlayer = null;

        public static void PlayItemAlert()
        {
            LoadNewSound();
            var now = DateTime.Now;
            if (now - _itemAlertLastPlayed >= TimeSpan.FromSeconds(1))
            {
                _itemAlertLastPlayed = now;
                try
                {
                    _itemAlertPlayer.Play();
                }
                catch
                {
                    _itemAlertPlayer = new SoundPlayer(Properties.Resources.ching);
                    _itemAlertPlayer.Play();
                }
            }
        }

        public static void LoadNewSound(bool ignoreIfAlreadyLoaded = false)
        {
            if (ignoreIfAlreadyLoaded)
            {
                _itemAlertPlayer = new SoundPlayer(Properties.Resources.ching);
            }

            if (!string.IsNullOrEmpty(MapAssistConfiguration.Loaded.ItemLog.SoundFile) && (_itemAlertPlayer == null || ignoreIfAlreadyLoaded))
            {
                var exePath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                var directory = Path.GetDirectoryName(exePath);
                var soundPath = Path.Combine(directory, MapAssistConfiguration.Loaded.ItemLog.SoundFile);
                _itemAlertPlayer = new SoundPlayer(soundPath);
                Console.Write("Loaded new sound file");
            }
            if (_itemAlertPlayer == null) { _itemAlertPlayer = new SoundPlayer(Properties.Resources.ching); }
        }
    }
}

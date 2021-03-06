﻿using System.ComponentModel;
using System.Runtime.Serialization;
using WinHue3.Philips_Hue.HueObjects.Common;
using WinHue3.ViewModels;


namespace WinHue3.Philips_Hue.HueObjects.NewSensorsObject.ClipZllTemperature
{
    /// <summary>
    /// Temperature sensor state.
    /// </summary>
    [DataContract]
    public class TemperatureSensorState : ValidatableBindableBase, ISensorStateBase
    {
        private int? _temperature;

        /// <summary>
        /// Current temperature.
        /// </summary>
        [HueProperty, DataMember, ReadOnly(true)]
        public int? temperature
        {
            get => _temperature;
            set => SetProperty(ref _temperature,value);
        }

        private string _lastupdated;

        [HueProperty, DataMember, ReadOnly(true)]
        public string lastupdated
        {
            get => _lastupdated;
            set => SetProperty(ref _lastupdated, value);
        }
    }
}

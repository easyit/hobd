﻿namespace hobd
{
    using System;
    using System.Runtime.CompilerServices;

    public class DistanceCostSensor : DerivedSensor, IFuelCostSensor
    {
        public DistanceCostSensor(string fuelConsumedSensor, string distanceSensor)
               : base(distanceSensor + "_UnitCost", fuelConsumedSensor, distanceSensor)
        {
            Func<Sensor, Sensor, double> func = null;
            if (func == null)
            {
                func = (consumed, distance) => (consumed.Value * this.FuelPrice) / distance.Value;
            }
            base.DerivedValue = func;
            this.Interval = 10000;
        }

        public string FuelCurrency
        {
            get
            {
                return this.Units;
            }
            set
            {
                this.Units = value;
            }
        }

        public double FuelPrice { get; set; }

        public string FuelPriceString
        {
            get
            {
                return this.FuelPrice.ToString();
            }
            set
            {
                try
                {
                    this.FuelPrice = double.Parse(value, UnitsConverter.DefaultNumberFormat);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}


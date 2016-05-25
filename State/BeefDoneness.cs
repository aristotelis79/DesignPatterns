﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State
{
    /// <summary>
    /// The State abstract class
    /// </summary>
    abstract class Doneness
    {
        protected Steak steak;
        protected double currentTemp;
        protected double lowerTemp;
        protected double upperTemp;
        protected bool canEat;

        public Steak Steak
        {
            get { return steak; }
            set { steak = value; }
        }

        public double CurrentTemp
        {
            get { return currentTemp; }
            set { currentTemp = value; }
        }

        public abstract void AddTemp(double temp);
        public abstract void RemoveTemp(double temp);
    }


    /// <summary>
    /// A Concrete State class.
    /// </summary>
    class Uncooked : Doneness
    {
        public Uncooked(Doneness state)
        {
            this.currentTemp = state.CurrentTemp;
            this.steak = state.Steak;
            Initialize();
        }

        private void Initialize()
        {
            lowerTemp = 0;
            upperTemp = 130;
            canEat = false;
        }

        public override void AddTemp(double amount)
        {
            currentTemp += amount;
            DonenessCheck();
        }

        public override void RemoveTemp(double amount)
        {
            currentTemp -= amount;
            DonenessCheck();
        }

        private void DonenessCheck()
        {
            if (currentTemp > upperTemp)
            {
                steak.State = new Rare(this);
            }
        }
    }

    /// <summary>
    /// A 'ConcreteState' class.
    /// </summary>
    class Rare : Doneness
    {
        public Rare(Doneness state) : this(state.CurrentTemp, state.Steak)
        {
        }

        public Rare(double currentTemp, Steak steak)
        {
            this.currentTemp = currentTemp;
            this.steak = steak;
            Initialize();
        }

        private void Initialize()
        {
            lowerTemp = 130;
            upperTemp = 139.999999999999;
            canEat = true;
        }

        public override void AddTemp(double amount)
        {
            currentTemp += amount;
            StateChangeCheck();
        }

        public override void RemoveTemp(double amount)
        {
            currentTemp -= amount;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (currentTemp < lowerTemp)
            {
                steak.State = new Uncooked(this);
            }
            else if (currentTemp > upperTemp)
            {
                steak.State = new MediumRare(this);
            }
        }
    }

    /// <summary>
    /// A Concrete State class
    /// </summary>
    class MediumRare : Doneness
    {
        public MediumRare(Doneness state) : this(state.CurrentTemp, state.Steak)
        {
        }

        public MediumRare(double currentTemp, Steak steak)
        {
            this.currentTemp = currentTemp;
            this.steak = steak;
            Initialize();
        }

        private void Initialize()
        {
            lowerTemp = 140;
            upperTemp = 154.9999999999;
        }

        public override void AddTemp(double amount)
        {
            currentTemp += amount;
            StateChangeCheck();
        }

        public override void RemoveTemp(double amount)
        {
            currentTemp -= amount;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (currentTemp < 0.0)
            {
                steak.State = new Uncooked(this);
            }
            else if (currentTemp < lowerTemp)
            {
                steak.State = new Rare(this);
            }
            else if (currentTemp > upperTemp)
            {
                steak.State = new Medium(this);
            }
        }
    }

    /// <summary>
    /// A Concrete State class
    /// </summary>
    class Medium : Doneness
    {
        public Medium(Doneness state) : this(state.CurrentTemp, state.Steak)
        {
        }

        public Medium(double currentTemp, Steak steak)
        {
            this.currentTemp = currentTemp;
            this.steak = steak;
            Initialize();
        }

        private void Initialize()
        {
            lowerTemp = 155;
            upperTemp = 169.9999999999;
        }

        public override void AddTemp(double amount)
        {
            currentTemp += amount;
            StateChangeCheck();
        }

        public override void RemoveTemp(double amount)
        {
            currentTemp -= amount;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (currentTemp < 130)
            {
                steak.State = new Uncooked(this);
            }
            else if (currentTemp < lowerTemp)
            {
                steak.State = new MediumRare(this);
            }
            else if (currentTemp > upperTemp)
            {
                steak.State = new WellDone(this);
            }
        }
    }

    /// <summary>
    /// A Concrete State class
    /// </summary>
    class WellDone : Doneness //aka Ruined
    {
        public WellDone(Doneness state) : this(state.CurrentTemp, state.Steak)
        {
        }

        public WellDone(double currentTemp, Steak steak)
        {
            this.currentTemp = currentTemp;
            this.steak = steak;
            Initialize();
        }

        private void Initialize()
        {
            lowerTemp = 170;
            upperTemp = 230;
        }

        public override void AddTemp(double amount)
        {
            currentTemp += amount;
            StateChangeCheck();
        }

        public override void RemoveTemp(double amount)
        {
            currentTemp -= amount;
            StateChangeCheck();
        }

        private void StateChangeCheck()
        {
            if (currentTemp < 0)
            {
                steak.State = new Uncooked(this);
            }
            else if (currentTemp < lowerTemp)
            {
                steak.State = new Medium(this);
            }
        }
    }

    /// <summary>
    /// The Context class
    /// </summary>
    class Steak
    {
        private Doneness _state;
        private string _cook;

        public Steak(string cook)
        {
            _cook = cook;
            _state = new Rare(0.0, this);
        }

        public double CurrentTemp
        {
            get { return _state.CurrentTemp; }
        }

        public Doneness State
        {
            get { return _state; }
            set { _state = value; }
        }

        public void AddTemp(double amount)
        {
            _state.AddTemp(amount);
            Console.WriteLine("Increased temperature by {0} degrees.", amount);
            Console.WriteLine(" Current temp is {0}", this.CurrentTemp);
            Console.WriteLine(" Status is {0}", this.State.GetType().Name);
            Console.WriteLine("");
        }

        public void RemoveTemp(double amount)
        {
            _state.RemoveTemp(amount);
            Console.WriteLine("Decreasd temperature by {0} degrees.", amount);
            Console.WriteLine(" Current temp is {0}", this.CurrentTemp);
            Console.WriteLine(" Status is {0}", this.State.GetType().Name);
            Console.WriteLine("");
        }
    }
}

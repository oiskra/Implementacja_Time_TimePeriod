using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementacja_Time_TimePeriod
{
    public readonly struct Time : IEquatable<Time>, IComparable<Time>
    {
        public readonly byte Hours { get; init; }
        public readonly byte Minutes { get; init; }
        public readonly byte Seconds { get; init; }
        public static bool operator ==(Time left, Time right) => left.Equals(right);
        public static bool operator !=(Time left, Time right) => !(left == right);
        public static bool operator <(Time left, Time right) => left.CompareTo(right) < 0;
        public static bool operator <=(Time left, Time right) => left.CompareTo(right) <= 0;
        public static bool operator >(Time left, Time right) => left.CompareTo(right) > 0;
        public static bool operator >=(Time left, Time right) => left.CompareTo(right) >= 0;
        public static Time operator +(Time left, Time right)
        {
            var rightTP = new TimePeriod(right.ToString());
            return left.Plus(rightTP);
        }
        public static Time operator -(Time left, Time right)
        {
            var tp = new TimePeriod(left, right);
            return new Time(tp.ToString());
        }
        public static Time operator *(Time time, int multiplier)
        {
            if (multiplier <= 0) throw new ArgumentOutOfRangeException("Time cannot be a negative number");
            var timePeriod = new TimePeriod(time.ToString());
            if (multiplier > 1)
            {
                var multiplicatedTimePeriod = (multiplier - 1) * timePeriod;
                var resultTime = time.Plus(multiplicatedTimePeriod);
                return resultTime;
            }
            else
                return time;
        }
        public static Time operator *(int multiplier, Time time)
            => time * multiplier;

        /**
        <summary>
            Konstruktor przyjmujący 3 parametry byte hours, byte minutes(default = 0), byte seconds(default = 0).
            Poprawność parametrów sprawdzana przez funkcje lokalną VerifyConstructor
        </summary>
         */
        public Time(byte hours, byte minutes = 0, byte seconds = 0)
        {
            Hours = VerifyConstructor(hours, 24);
            Minutes = VerifyConstructor(minutes, 60);
            Seconds = VerifyConstructor(seconds, 60);

            byte VerifyConstructor(byte value, byte modulo)
                => (value == value % modulo && value >= 0) ? value : throw new ArgumentOutOfRangeException();
        }

        /**
        <summary>
            Konstruktor przyjmujący parametr string text w formie "hh:mm:ss".
            Poprawność parametrów sprawdzana przez funkcje lokalną VerifyConstructor
        </summary>
         */
        public Time(string text)
        {
            byte[] arr = Array.ConvertAll<string, byte>(text.Split(':'), byte.Parse);
            if (arr.Length != 3) throw new ArgumentException();
            Hours = VerifyConstructor(arr[0], 24);
            Minutes = VerifyConstructor(arr[1], 60);
            Seconds = VerifyConstructor(arr[2], 60);

            byte VerifyConstructor(byte value, byte modulo)
                => (value == value % modulo && value >= 0) ? value : throw new ArgumentOutOfRangeException();
        }

        public override string ToString()
            => $"{Hours:D2}:{Minutes:D2}:{Seconds:D2}";

        public bool Equals(Time other)
        {
            return this.Hours == other.Hours &&
                   this.Minutes == other.Minutes &&
                   this.Seconds == other.Seconds;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is Time other) return Equals(other);

            return false;
        }

        public override int GetHashCode()
            => HashCode.Combine(Hours, Minutes, Seconds);

        public int CompareTo(Time other)
        {
            int hoursValue = this.Hours.CompareTo(other.Hours);
            int minutesValue = this.Minutes.CompareTo(other.Minutes);
            int secondsValue = this.Seconds.CompareTo(other.Seconds);

            if (hoursValue != 0) return hoursValue;
            else
            {
                if (minutesValue != 0) return minutesValue;
                else return secondsValue;
            }
        }

  
        ///<summary>Metoda zwraca wartość metody statycznej Time Plus(Time, TimePeriod) </summary>
        public Time Plus(TimePeriod timePeriod) 
            => Plus(this, timePeriod);

        ///<summary>Metoda zwraca wartość dodanych do siebie obiektów Time i TimePeriod, zważając na formę czasu(modulo 24, modulo 60)</summary>
        public static Time Plus(Time time, TimePeriod timePeriod)
        {
            TimePeriod tp = new (time.Hours, time.Minutes, time.Seconds);
            TimePeriod addedTime = tp.Plus(timePeriod);
            
            return new Time(CalculateTimeForm(addedTime).ToString());
            
            
            TimePeriod CalculateTimeForm(TimePeriod tp)
            {
                byte h = (byte)((tp.Seconds / 3600) % 24);
                byte m = (byte)((tp.Seconds / 60) % 60);
                byte s = (byte)(tp.Seconds % 60);
                return new TimePeriod(h, m, s);
            }
        }
    }
}

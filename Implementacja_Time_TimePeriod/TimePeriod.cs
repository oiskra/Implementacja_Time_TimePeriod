using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementacja_Time_TimePeriod
{
    public readonly struct TimePeriod : IEquatable<TimePeriod>, IComparable<TimePeriod>
    {
        public readonly long Seconds { get; init; }

        public static bool operator ==(TimePeriod left, TimePeriod right)
            => left.Equals(right);
        public static bool operator !=(TimePeriod left, TimePeriod right)
            => !(left == right);
        public static bool operator <(TimePeriod left, TimePeriod right)
            => left.CompareTo(right) < 0;
        public static bool operator <=(TimePeriod left, TimePeriod right)
            => left.CompareTo(right) <= 0;
        public static bool operator >(TimePeriod left, TimePeriod right)
            => left.CompareTo(right) > 0;
        public static bool operator >=(TimePeriod left, TimePeriod right)
            => left.CompareTo(right) >= 0;
        public static TimePeriod operator +(TimePeriod left, TimePeriod right)
            => left.Plus(right);
        public static TimePeriod operator -(TimePeriod left, TimePeriod right)
        {
            if (left.Seconds - right.Seconds < 0) throw new ArgumentOutOfRangeException("TimePeriod cannot be negative");
            return new(left.Seconds - right.Seconds);
        }
        public static TimePeriod operator *(TimePeriod timePeriod, int multiplier)
        {
            if (multiplier <= 0) throw new ArgumentOutOfRangeException("TimePeriod cannot be a negative number");
            return new TimePeriod(timePeriod.Seconds * multiplier);
        }
        public static TimePeriod operator *(int multiplier, TimePeriod timePeriod)
            => new TimePeriod(timePeriod.Seconds * multiplier);

        /**
        <summary>
            Konstruktor przyjmujący parametry byte hours, byte minutes, byte seconds(default = 0) i konwertowane na sekundy.
            Poprawność parametrów sprawdzana przez funkcje lokalną VerifyConstructor
        </summary>
         */
        public TimePeriod(byte hours, byte minutes, byte seconds = 0)
        {
            Seconds = hours * 3600 
                + VerifyConstructor(minutes, 60) * 60 
                + VerifyConstructor(seconds, 60);

            byte VerifyConstructor(byte value, byte modulo)
                => (value == value % modulo && value >= 0) ? value : throw new ArgumentOutOfRangeException();
        }

        /**
        <summary>
            Konstruktor przyjmujący parametr long seconds.
        </summary>
         */
        public TimePeriod(long seconds) { Seconds = seconds; }

        /**
        <summary>
            Konstruktor przyjmujący 2 parametry Time sprawdzający TimePeriod między 2 czasami.
        </summary>
         */
        public TimePeriod(Time t1, Time t2)
        {
            long t1Sec = t1.Hours * 3600 + t1.Minutes * 60 + t1.Seconds;
            long t2Sec = t2.Hours * 3600 + t2.Minutes * 60 + t2.Seconds;

            long tpSec = t1Sec - t2Sec;
            if (tpSec < 0) tpSec *= (-1);           

            Seconds = tpSec;
        }

        /**
        <summary>
            Konstruktor przyjmujący parametr string text w formie "h:mm:ss" i konwertowana na sekundy.
            Poprawność parametrów sprawdzana przez funkcje lokalną VerifyConstructor
        </summary>
         */
        public TimePeriod(string text)
        {
            long[] arr = Array.ConvertAll<string, long>(text.Split(':'), long.Parse);
            Seconds = arr[0] * 3600 
                + VerifyConstructor(arr[1], 60) * 60 
                + VerifyConstructor(arr[2], 60);

            long VerifyConstructor(long value, long modulo)
                => (value == value % modulo && value >= 0) ? value : throw new ArgumentOutOfRangeException();
        }

        public override string ToString()
        {
            long h = Seconds / 3600;
            long m = (Seconds / 60) % 60;
            long s = Seconds % 60;

            return $"{h}:{m:D2}:{s:D2}";
        }

        public int CompareTo(TimePeriod other)
        {
            int secValue = Seconds.CompareTo(other.Seconds);
            return secValue;     
        }

        public bool Equals(TimePeriod other)
            => Seconds == other.Seconds;

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (obj is TimePeriod other) return Equals(other);

            return false;
        }

        public override int GetHashCode()
            => HashCode.Combine(Seconds);

        ///<summary>Metoda zwraca wartość metody statycznej Time Plus(TimePeriod, TimePeriod) </summary>
        public TimePeriod Plus(TimePeriod timePeriod)
            => Plus(this, timePeriod);

        ///<summary>Metoda zwraca wartość dodanych do siebie 2 obiektów TimePeriod, zważając na formę czasu(modulo 60)</summary>
        public static TimePeriod Plus(TimePeriod tp1, TimePeriod tp2)
            => new(tp1.Seconds + tp2.Seconds);
        }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Implementacja_Time_TimePeriod;
using System;

namespace TimeTimePeriodUnitTests
{
    [TestClass]
    public class TimeUnitTest
    {
        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)5,(byte)24,(byte)40, "05:24:40")]
        [DataRow((byte)13, (byte)2, (byte)25, "13:02:25")]
        [DataRow((byte)20, (byte)35, (byte)0, "20:35:00")]
        public void Time_Constructor_3params(byte h, byte m, byte s, string expectedTime)
        {
            var time = new Time(h, m, s);
            Assert.AreEqual(expectedTime, time.ToString());
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)16,(byte)1,"16:01:00")]
        [DataRow((byte)4,(byte)4, "04:04:00")]
        [DataRow((byte)18, (byte)20, "18:20:00")]
        public void Time_Constructor_2params(byte h, byte m, string expectedTime)
        {
            var time = new Time(h, m);
            Assert.AreEqual(expectedTime, time.ToString());
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)11, "11:00:00")]
        [DataRow((byte)2, "02:00:00")]
        [DataRow((byte)21, "21:00:00")]
        public void Time_Constructor_1params(byte h, string expectedTime)
        {
            var time = new Time(h);
            Assert.AreEqual(expectedTime, time.ToString());
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow((byte)24, (byte)1, (byte)1)]
        [DataRow((byte)1, (byte)60, (byte)1)]
        [DataRow((byte)1, (byte)1, (byte)60)]
        [DataRow((byte)24, (byte)1, (byte)60)]
        [DataRow((byte)1, (byte)60, (byte)60)]
        [DataRow((byte)24, (byte)60, (byte)1)]
        [DataRow((byte)24, (byte)60, (byte)50)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Time_Constructor_3params_ArgumentOutOfRangeExeption(byte h, byte m, byte s)
        {
            var time = new Time(h, m, s);            
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("01:01:01", "01:01:01")]
        [DataRow("02:12:21", "02:12:21")]
        [DataRow("14:04:30", "14:04:30")]
        [DataRow("22:38:09", "22:38:09")]
        [DataRow("23:59:59", "23:59:59")]
        public void Time_Constructor_Text(string text, string expectedTime)
        {
            var time = new Time(text);
            Assert.AreEqual(expectedTime, time.ToString());
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("24:01:01")]
        [DataRow("01:60:01")]
        [DataRow("01:01:60")]
        [DataRow("24:01:60")]
        [DataRow("01:60:60")]
        [DataRow("24:60:01")]
        [DataRow("24:60:60")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Time_Constructor_Text_ArgumentOutOfRangeException(string text)
        {
            var time = new Time(text);
        }

        [DataTestMethod, TestCategory("Constructors")]
        [DataRow("12")]
        [DataRow("6:01")] 
        [DataRow("1:41")]
        [DataRow("23:56")]
        [ExpectedException(typeof(ArgumentException))]
        public void Time_Constructor_Text_WrongText(string text)
        {
            var time = new Time(text);
        }

        [TestMethod]
        public void Time_ToStringMethod()
        {
            var time = new Time(1, 2, 3);
            var time2 = new Time(01, 02, 03);
            var time3 = new Time("01:02:03");
            string stringRepresentation = "01:02:03";

            Assert.IsInstanceOfType(time.ToString(),typeof(string));
            Assert.IsInstanceOfType(time2.ToString(), typeof(string));
            Assert.IsInstanceOfType(time3.ToString(), typeof(string));
           
            Assert.AreEqual(stringRepresentation, time.ToString());
            Assert.AreEqual(stringRepresentation, time2.ToString());
            Assert.AreEqual(stringRepresentation, time3.ToString());
        }

        [TestMethod]
        public void Time_EqualsMethod()
        {
            var timeString = new Time("17:10:23");
            var time = new Time(17,10,23);
            var diffTime = new Time("12:10:23");

            Assert.AreEqual(timeString, time);
            Assert.AreNotEqual(timeString, diffTime);
            Assert.AreNotEqual(time, diffTime);

            Assert.IsTrue(timeString.Equals(time));
            Assert.IsTrue(time.Equals(timeString));

            Assert.IsFalse(time.Equals(diffTime));
            Assert.IsFalse(diffTime.Equals(time));
            Assert.IsFalse(diffTime.Equals(timeString));
            Assert.IsFalse(timeString.Equals(diffTime));
        }


        [TestMethod]
        public void Time_EqualityOperator()
        {
            var time = new Time(23,59,1);
            var timeString = new Time("23:59:01");
            var diffTime = new Time(22);

            Assert.IsTrue(time == timeString);
            Assert.IsFalse(time == diffTime);
            Assert.IsFalse(timeString == diffTime);
        }

        [TestMethod]
        public void Time_InequalityOperator()
        {
            var time = new Time(23, 59, 1);
            var timeString = new Time("23:59:01");
            var diffTime = new Time(22);

            Assert.IsFalse(time != timeString);
            Assert.IsTrue(time != diffTime);
            Assert.IsTrue(timeString != diffTime);
        }

        [TestMethod]
        public void Time_GreaterThenOperator()
        {
            var time = new Time(10);
            var time1 = new Time(1, 30);
            var time2 = new Time("15:25:54");

            Assert.IsTrue(time > time1);
            Assert.IsTrue(time2 > time1);
            Assert.IsTrue(time2 > time);
            Assert.IsFalse(time1 > time2);

        }

        [TestMethod]
        public void Time_GreaterThenOrEqualsOperator()
        {
            var time = new Time(23, 30, 0);
            var time1 = new Time(23, 30);
            var time2 = new Time("23:42:14");

            Assert.IsTrue(time2 >= time1);
            Assert.IsTrue(time2 >= time);
            Assert.IsTrue(time >= time1);
            Assert.IsFalse(time1 >= time2);
        }

        [TestMethod]
        public void Time_LessThenOperator()
        {
            var time = new Time(0);
            var time1 = new Time(12, 34, 56);
            var time2 = new Time(8, 2, 2);

            Assert.IsTrue(time < time1);
            Assert.IsTrue(time < time2);
            Assert.IsFalse(time1 < time2);
            Assert.IsTrue(time2 < time1);
        }

        [TestMethod]
        public void Time_LessThenOrEqualsOperator()
        {
            var time = new Time(0);
            var time1 = new Time(12, 34);
            var time2 = new Time(8, 2, 2);
            var time3 = new Time("12:34:00");

            Assert.IsTrue(time <= time1);
            Assert.IsTrue(time <= time2);
            Assert.IsTrue(time1 <= time3);
            Assert.IsTrue(time2 <= time1);
            Assert.IsFalse(time1 <= time2);
            Assert.IsFalse(time2 <= time);
        }

        [TestMethod]
        public void Time_PlusOperator()
        {
            var time1 = new Time(23, 59, 1);
            var time2 = new Time(1);
            var time3 = new Time("12:12:21");

            Assert.AreEqual((time1 + time2).ToString(), "00:59:01");
            Assert.AreEqual((time1 + time3).ToString(), "12:11:22");
            Assert.AreEqual((time2 + time3).ToString(), "13:12:21");
        }


        [TestMethod]
        public void Time_PlusMethod()
        {
            var time = new Time(20,0,26);
            var timeP = new TimePeriod(13, 23, 56);

            Assert.AreEqual(time.Plus(timeP).ToString(), "09:24:22");
        }

        [TestMethod]
        public void Time_Static_PlusMethod()
        {
            var time = new Time(10, 43, 7);
            var timeP = new TimePeriod(6, 1, 16);

            Assert.AreEqual(Time.Plus(time, timeP).ToString(), "16:44:23");
        }

        [TestMethod]
        public void Time_MinusOperator()
        {
            var time = new Time(23,59,59);
            var time1 = new Time(9,15,15);

            Assert.AreEqual((time - time1).ToString(), "14:44:44");
        }

        [DataTestMethod]
        [DataRow((byte)3, (byte)44, (byte)29, 1, "03:44:29")]
        [DataRow((byte)13, (byte)42, (byte)0, 5, "20:30:00")]
        [DataRow((byte)6, (byte)5, (byte)10, 10, "12:51:40")]
        public void Time_MultiplicationOperator(byte h, byte m, byte s, int multiplier, string expectedTime)
        {
            var time = new Time(h,m,s);

            Assert.AreEqual(expectedTime,(time*multiplier).ToString());
            Assert.AreEqual(expectedTime,(multiplier * time).ToString());
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-23)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Time_MultiplicationOperator_NegativeNumber_ArgumentOutOfRangeExeption(int multiplier)
        {
            var time = new Time(2);
            var newTime = time * multiplier;
        }
    }

    [TestClass]
    public class TimePeriodUnitTest
    {
        [DataTestMethod]
        [DataRow((byte)123, (byte)49, (byte)22, "123:49:22")]
        [DataRow((byte)222, (byte)21, (byte)42, "222:21:42")]
        [DataRow((byte)75, (byte)41, (byte)55, "75:41:55")]
        [DataRow((byte)177, (byte)54, (byte)16, "177:54:16")]
        [DataRow((byte)1, (byte)1, (byte)1, "1:01:01")]
        [DataRow((byte)0, (byte)0, (byte)0, "0:00:00")]
        public void TimePeriod_Constructor_3params_Hours_Minutes_Seconds(byte a, byte b, byte c, string expectedTimePeriod)
        {
            var tp = new TimePeriod(a, b, c);

            Assert.AreEqual(expectedTimePeriod, tp.ToString());
        }

        [DataTestMethod]
        [DataRow((byte)0,(byte)0, "0:00:00")]
        [DataRow((byte)0,(byte)1, "0:01:00")]
        [DataRow((byte)1,(byte)0, "1:00:00")]
        [DataRow((byte)219,(byte)26, "219:26:00")]
        [DataRow((byte)40, (byte)39, "40:39:00")]
        [DataRow((byte)190, (byte)47, "190:47:00")]
        [DataRow((byte)223, (byte)58, "223:58:00")]
        public void TimePeriod_Constructor_2params_Hours_Minutes(byte a, byte b, string expectedTimePeriod)
        {
            var tp = new TimePeriod(a, b);

            Assert.AreEqual(expectedTimePeriod, tp.ToString());
        }

        [DataTestMethod]
        [DataRow(0,"0:00:00")]
        [DataRow(1, "0:00:01")]
        [DataRow(39, "0:00:39")]
        [DataRow(456, "0:07:36")]
        [DataRow(2012, "0:33:32")]
        [DataRow(11841, "3:17:21")]
        [DataRow(669017, "185:50:17")]
        public void TimePeriod_Constructor_1param_Seconds(long s, string expectedTimePeriod)
        {
            var tp = new TimePeriod(s);

            Assert.AreEqual(expectedTimePeriod, tp.ToString());
        }

        [DataTestMethod]
        [DataRow((byte)5, (byte)24, (byte)40, (byte)20, (byte)35, (byte)0, "15:10:20")]
        [DataRow((byte)13, (byte)2, (byte)25, (byte)23, (byte)59, (byte)1, "10:56:36")]
        [DataRow((byte)9, (byte)28, (byte)20, (byte)13, (byte)31, (byte)27, "4:03:07")]
        [DataRow((byte)11, (byte)56, (byte)31, (byte)1, (byte)28, (byte)13, "10:28:18")]
        [DataRow((byte)22, (byte)3, (byte)40, (byte)16, (byte)22, (byte)16, "5:41:24")]
        [DataRow((byte)0, (byte)0, (byte)0, (byte)0, (byte)0, (byte)0, "0:00:00")]
        public void TimePeriod_Constructor_2params_Time(byte a1, byte b1, byte c1, 
                                                        byte a2, byte b2, byte c2, 
                                                        string expectedTimePeriod)
        {
            var t1 = new Time(a1, b1, c1);
            var t2 = new Time(a2, b2, c2);
            var tp = new TimePeriod(t1, t2);

            Assert.AreEqual(expectedTimePeriod, tp.ToString());
        }

        [DataTestMethod]
        [DataRow("1:00:00", "1:00:00")]
        [DataRow("0:01:00", "0:01:00")]
        [DataRow("0:00:01", "0:00:01")]
        [DataRow("1:01:01", "1:01:01")]
        [DataRow("11:00:00", "11:00:00")]
        [DataRow("0:11:00", "0:11:00")]
        [DataRow("0:00:11", "0:00:11")]
        [DataRow("11:11:11", "11:11:11")]
        [DataRow("111:11:11", "111:11:11")]
        public void TimePeriod_Constructor_String(string timePeriod, string expectedTimePeriod)
        {
            var tp = new TimePeriod(timePeriod);

            Assert.AreEqual(expectedTimePeriod, tp.ToString());
        }

        [DataTestMethod]
        [DataRow("1:60:01")]
        [DataRow("1:01:60")]
        [DataRow("1:60:60")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TimePeriod_Constructor_String_ArgumentOutOfRangeExeption(string text)
        {
            var tp = new TimePeriod(text);
        }

        [TestMethod]
        public void TimePeriod_EqualsMethod()
        {
            var tpString = new TimePeriod("17:10:23");
            var tp = new TimePeriod(17, 10, 23);
            var diffTp = new TimePeriod("122:10:23");
 
            Assert.AreEqual(tpString, tp);
            Assert.AreNotEqual(tpString, diffTp);
            Assert.AreNotEqual(tp, diffTp);

            Assert.IsTrue(tpString.Equals(tp));
            Assert.IsTrue(tp.Equals(tpString));

            Assert.IsFalse(tp.Equals(diffTp));
            Assert.IsFalse(diffTp.Equals(tp));
            Assert.IsFalse(diffTp.Equals(tpString));
            Assert.IsFalse(tpString.Equals(diffTp));
        }


        [TestMethod]
        public void TimePeriod_EqualityOperator()
        {
            var tp = new TimePeriod(23, 59, 1);
            var tpString = new TimePeriod("23:59:01");
            var diffTp = new TimePeriod(22);

            Assert.IsTrue(tp == tpString);
            Assert.IsFalse(tp == diffTp);
            Assert.IsFalse(tpString == diffTp);
        }

        [TestMethod]
        public void TimePeriod_InequalityOperator()
        {
            var tp = new TimePeriod(23, 59, 1);
            var tpString = new TimePeriod("23:59:01");
            var diffTp = new TimePeriod(22);

            Assert.IsFalse(tp != tpString);
            Assert.IsTrue(tp != diffTp);
            Assert.IsTrue(tpString != diffTp);
        }

        [TestMethod]
        public void TimePeriod_GreaterThenOperator()
        {
            var tp = new TimePeriod(10);
            var tp1 = new TimePeriod(1, 30);
            var tp2 = new TimePeriod("15:25:54");

            Assert.IsTrue(tp2 > tp);
            Assert.IsTrue(tp2 > tp);
            Assert.IsFalse(tp > tp2);
            Assert.IsFalse(tp1 > tp2);

        }

        [TestMethod]
        public void TimePeriod_GreaterThenOrEqualsOperator()
        {
            var tp = new TimePeriod(23, 30, 0);
            var tp1 = new TimePeriod(23, 30);
            var tp2 = new TimePeriod("23:42:14");

            Assert.IsTrue(tp2 >= tp1);
            Assert.IsTrue(tp2 >= tp);
            Assert.IsTrue(tp >= tp1);
            Assert.IsFalse(tp1 >= tp2);
        }

        [TestMethod]
        public void TimePeriod_LessThenOperator()
        {
            var tp = new TimePeriod(0);
            var tp1 = new TimePeriod(12, 34, 56);
            var tp2 = new TimePeriod(8, 2, 2);

            Assert.IsTrue(tp < tp1);
            Assert.IsTrue(tp < tp2);
            Assert.IsFalse(tp1 < tp2);
            Assert.IsTrue(tp2 < tp1);
        }

        [TestMethod]
        public void TimePeriod_LessThenOrEqualsOperator()
        {
            var tp = new TimePeriod(0);
            var tp1 = new TimePeriod(12, 34);
            var tp2 = new TimePeriod(8, 2, 2);
            var tp3 = new TimePeriod("12:34:00");

            Assert.IsTrue(tp <= tp1);
            Assert.IsTrue(tp <= tp2);
            Assert.IsTrue(tp1 <= tp3);
            Assert.IsTrue(tp2 <= tp1);
            Assert.IsFalse(tp1 <= tp2);
            Assert.IsFalse(tp2 <= tp);
        }

        [TestMethod]
        public void TimePeriod_PlusOperator()
        {
            var tp1 = new TimePeriod(23, 59, 1);
            var tp2 = new TimePeriod(7201);
            var tp3 = new TimePeriod("12:12:21");

            Assert.AreEqual((tp1 + tp2).ToString(), "25:59:02");
            Assert.AreEqual((tp1 + tp3).ToString(), "36:11:22");
            Assert.AreEqual((tp2 + tp3).ToString(), "14:12:22");
        }


        [TestMethod]
        public void TimePeriod_PlusMethod()
        {
            var tp = new TimePeriod(20, 0, 26);
            var tp2 = new TimePeriod(13, 23, 56);

            Assert.AreEqual(tp.Plus(tp2).ToString(), "33:24:22");
        }

        [TestMethod]
        public void TimePeriod_Static_PlusMethod()
        {
            var tp1 = new TimePeriod(10, 43, 7);
            var tp2 = new TimePeriod(6, 1, 16);

            Assert.AreEqual(TimePeriod.Plus(tp1, tp2).ToString(), "16:44:23");
        }

        [TestMethod]
        public void TimePeriod_MinusOperator()
        {
            var tp = new TimePeriod(23, 59, 59);
            var tp1 = new TimePeriod(9, 15, 15);

            Assert.AreEqual((tp - tp1).ToString(), "14:44:44");
        }

        [DataTestMethod]
        [DataRow((byte)3, (byte)44, (byte)29, 1, "3:44:29")]
        [DataRow((byte)13, (byte)42, (byte)0, 5, "68:30:00")]
        [DataRow((byte)6, (byte)5, (byte)10, 10, "60:51:40")]
        [DataRow((byte)21, (byte)14, (byte)34, 10, "212:25:40")]
        public void TimePeriod_MultiplicationOperator(byte h, byte m, byte s, int multiplier, string expectedTime)
        {
            var tp = new TimePeriod(h, m, s);

            Assert.AreEqual(expectedTime, (tp * multiplier).ToString());
            Assert.AreEqual(expectedTime, (multiplier * tp).ToString());
        }

        [DataTestMethod]
        [DataRow(0)]
        [DataRow(-1)]
        [DataRow(-23)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TimePeriod_MultiplicationOperator_NegativeNumber_ArgumentOutOfRangeExeption(int multiplier)
        {
            var tp = new TimePeriod(2);
            var newTimePeriod = tp * multiplier;
        }
    }
}


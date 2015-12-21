using System;
using System.Collections.Generic;

namespace Metronome.Models
{
    public class TimeSignature
    {
        public String Name { get; set; }
        public int LowNumber { get; set; }
        public int UppNumber { get; set; }

    }
    public class TimeSignatureManager
    {
        public static List<TimeSignature> GetTimeSignatures()
        {
            var TimeSignatures = new List<TimeSignature>();

            TimeSignatures.Add(new TimeSignature { Name = "2/4", UppNumber = 2, LowNumber = 4 });
            TimeSignatures.Add(new TimeSignature { Name = "3/4", UppNumber = 3, LowNumber = 4 });
            TimeSignatures.Add(new TimeSignature { Name = "4/4", UppNumber = 4, LowNumber = 4 });
            TimeSignatures.Add(new TimeSignature { Name = "5/4", UppNumber = 5, LowNumber = 4 });
            TimeSignatures.Add(new TimeSignature { Name = "6/4", UppNumber = 6, LowNumber = 4 });
            TimeSignatures.Add(new TimeSignature { Name = "7/4", UppNumber = 7, LowNumber = 4 });
            TimeSignatures.Add(new TimeSignature { Name = "6/8", UppNumber = 6, LowNumber = 8 });
            TimeSignatures.Add(new TimeSignature { Name = "9/8", UppNumber = 9, LowNumber = 8 });
            TimeSignatures.Add(new TimeSignature { Name = "12/8", UppNumber = 12, LowNumber = 8 });

            return TimeSignatures;
        }
    }
}

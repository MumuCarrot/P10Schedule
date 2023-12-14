using System;

namespace Schedule
{
    /// <summary>
    /// Time conversion class
    /// </summary>
    class TimeS
    {
        /// <summary>
        /// Constructor with single parameter
        /// </summary>
        /// <param name="time">
        /// time must be in format of HH:MM
        /// that field will be converted behind the terms of the task
        /// </param>
        public TimeS(string time)
        {
            this.time = time;

            bool sw = false;
            foreach (var item in time)
            {
                if (item == ':')
                {
                    sw = true;
                    continue;
                }
                if (sw) minutes += item;
                else hours += item;
            }
        }

        /// <summary>
        /// HH will be saved here
        /// </summary>
        private readonly string hours = string.Empty;
        /// <summary>
        /// MM will be saved here
        /// </summary>
        private readonly string minutes = string.Empty;
        /// <summary>
        /// Non-Parsed time
        /// </summary>
        public readonly string time = string.Empty;

        /// <summary>
        /// Parsing time according the task
        /// </summary>
        /// <returns>
        /// Will return ToString in safe mode.
        /// </returns>
        /// <exception cref="Exception">
        /// Will occure time can't be converted at needed format
        /// </exception>
        public string GetTime() => (hours != string.Empty && minutes != string.Empty) ? this : throw new Exception("This time can't exist.");
        /// <summary>
        /// Parsing time according the task [HH * 60 + MM]
        /// </summary>
        /// <returns>
        /// Text string [HH * 60 + MM]
        /// </returns>
        public override string ToString() => (int.Parse(hours) * 60 + int.Parse(minutes)).ToString();

        /// <summary>
        /// Overloading implicit operator string
        /// TimeS -> String
        /// </summary>
        /// <param name="ts">
        /// That one will be parsed
        /// </param>
        public static implicit operator string(TimeS ts) => ts.ToString();
    }
}

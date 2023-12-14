using System.Collections.Generic;

namespace Schedule
{
    /// <summary>
    /// Class of the transport
    /// Can be inheritated
    /// </summary>
    class Transport
    {

        /// <summary>
        /// Name/Line of the Transport
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Schedule for the weekdays
        /// </summary>
        public List<TimeS> TimeScheduleWeekdays { get; set; }
        /// <summary>
        /// Schedule for the weekends
        /// </summary>
        public List<TimeS> TimeScheduleWeekends { get; set; }

        /// <summary>
        /// Parse time according the task
        /// </summary>
        /// <returns>
        /// [ [HH * 60 + MM]0 [HH * 60 + MM]1 ... [HH * 60 + MM]n ]
        /// </returns>
        public string ParseSchedule() => Name + "\n" + RFE(TimeScheduleWeekdays) + "\n" + RFE(TimeScheduleWeekends) + "\n";
        /// <summary>
        /// Recursive function foreach,
        /// made it to just make my life harder, lol
        /// </summary>
        /// <param name="ts">
        /// TimeSchedule List where we will itterate 
        /// </param>
        /// <param name="iter">
        /// Itterator for correct recursion work
        /// </param>
        /// <returns>
        /// Returns string with all elements in List<TimeS> one by one 
        /// </returns>
        protected static string RFE(List<TimeS> ts, int iter = 0) => (iter != ts.Count) ? ts[iter].ToString() + " " + RFE(ts, ++iter) : string.Empty;
    }
}

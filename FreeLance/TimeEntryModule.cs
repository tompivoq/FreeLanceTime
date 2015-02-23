using Nancy;

namespace FreeLance
{
    /**
     * This module is responsible for recording what time is spent on which project.
     */
    public class TimeEntryModule : NancyModule
    {
        public TimeEntryModule() : base("/recordTime")
        {
            Get["/"] = x => "Hello World, ready to enter time!";
        }
    }

}
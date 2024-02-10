using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class CPHInline
{
    public bool Execute()
    {
        var message = args["message"].ToString();
        JObject theRun = JObject.Parse(message);
        IList<JToken> events = theRun["run"]["events"].Children().ToList();
        IList<Event> eventsObject = new List<Event>();
        foreach (JToken r in events)
        {
            Event eventResult = r.ToObject<Event>();
            eventsObject.Add(eventResult);
        }

        foreach (var e in eventsObject)
        {
            if (e.type == "run_started_event")
            {
            	//d.data.personalBest 
				//d.data.expectedEndTime
            	CPH.SetArgument("personalBest", MsToTime(e.data.personalBest, "time"));
            	CPH.SetArgument("expectedEndTime", MsToTime(e.data.expectedEndTime, "time"));
            	CPH.RunAction("[TheRun] Run Started Event", true);
            
            }

            if (e.type == "gold_split_event")
            {
            	//e.data.splitName
				//e.data.newGold
				//e.data.previousGold
				//e.data.delta
				//e.data.finishedSplitAttemptCount
            	CPH.SetArgument("splitName", e.data.splitName);
            	CPH.SetArgument("newGold", MsToTime(e.data.newGold, "time"));
            	CPH.SetArgument("previousGold", MsToTime(e.data.previousGold, "time"));
            	CPH.SetArgument("delta", MsToTime(e.data.delta, "delta"));
            	CPH.SetArgument("finishedSplitAttemptCount", e.data.finishedSplitAttemptCount);
            	CPH.RunAction("[The Run] Gold Split Event", true);
            }

            if (e.type == "run_ended_event")
            {
				//e.data.predictedTime
				//e.data.deltaToPredictedTime
				//e.data.personalBest
				//e.data.deltaToPersonalBest
				//e.data.finalTime
            	CPH.SetArgument("predictedTime", MsToTime(e.data.predictedTime, "time"));
            	CPH.SetArgument("deltaToPredictedTime", MsToTime(e.data.deltaToPredictedTime, "delta"));
            	CPH.SetArgument("personalBest", MsToTime(e.data.personalBest, "time"));
            	CPH.SetArgument("deltaToPersonalBest", MsToTime(e.data.deltaToPersonalBest, "delta"));
            	CPH.SetArgument("finalTime", MsToTime(e.data.finalTime, "time"));
            	CPH.RunAction("[The Run] Run Ended Event", true);	
            }

            if (e.type == "best_run_ever_event")
            {
				//e.data.achievedTime
				//e.data.splitName
				//e.data.targetTime
				CPH.SetArgument("achievedTime", MsToTime(e.data.achievedTime, "time"));
				CPH.SetArgument("splitName", e.data.splitName);
				CPH.SetArgument("targetTime", e.data.targetTime);
				CPH.RunAction("[The Run] Best Run Ever Event", true);
            }
        }

        return true;
    }

    public static string MsToTime(float ms, string type)
    {
        TimeSpan time = TimeSpan.FromMilliseconds(Math.Abs(ms));
        string t;
        if (time.Hours > 0)
        {
            t = time.ToString(@"hh\:mm\:ss\.ff");
        }
        else if (time.Minutes > 0)
        {
            t = time.ToString(@"mm\:ss\.ff");
        }
        else
        {
            t = time.ToString(@"ss\.ff");
        }

        if (type == "delta")
        {
            if (ms < 0)
            {
                t = "-" + t;
            }
            else
            {
                t = "+" + t;
            }
        }

        return t;
    }

    public class Rootobject
    {
        public Run run { get; set; }
    }

    public class Run
    {
        public Event[] events { get; set; }
    }

    public class Event
    {
        public string game { get; set; }
        public Data data { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime time { get; set; }
        public string type { get; set; }
        public string category { get; set; }
        public string username { get; set; }
    }

    public class Data
    {
        public float personalBest { get; set; }
        public float expectedEndTime { get; set; }
        public string splitName { get; set; }
        public float newGold { get; set; }
        public float previousGold { get; set; }
        public float delta { get; set; }
        public int finishedSplitAttemptCount { get; set; }
        public float predictedTime { get; set; }
        public float deltaToPredictedTime { get; set; }
        public float deltaToPersonalBest { get; set; }
        public float finalTime { get; set; }
        public float achievedTime { get; set; }
        public string targetTime { get; set; }
    }
}

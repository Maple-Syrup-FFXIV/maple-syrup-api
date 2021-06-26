﻿using System;

namespace maple_syrup_api.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EventType EventType { get; set; }
        public EventStatus EventStatus { get; set; }
    }


    public enum EventType 
    { 
        Generic  = 0,
        Raid = 1,
        Treasure = 2
    }

    public enum EventStatus
    {
        Draft = 0,
        Published = 1,
        Canceled = 2
    }

}
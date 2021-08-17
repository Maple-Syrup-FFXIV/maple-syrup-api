using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace maple_syrup_api.Models
{
    public class Event
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EventType EventType { get; set; }
        public EventStatus EventStatus { get; set; }
        //public User Users List<User>;
        [ForeignKey("RequirementId")]
        public int? RequirementId { get; set; }
        public virtual EventRequirement Requirement { get; set; }
        public string FightName { get; set; }
        public int OwnerId { get; set; }//UserId of Creator of this Event
        public string Description { get; set; }
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

﻿using maple_syrup_api.Models;
using maple_syrup_api.Dto;
using System;
using System.Collections.Generic;


namespace maple_syrup_api.Services.IService
{
    public interface IEventService
    {
        List<Event> GetAllFromStartDate(DateTime pFilterStartDate);

        void AddPlayer(Player newPlayer,int EventId);

        void RemovePlayer(Player Player, int EventId);

        Event UpdateEvent(UpdateEventIn NewEvent);

        void CreateEvent(Event nEvent, EventRequirement pRequirement);
        int GetIdOwner(int EventId);
        void DeleteEvent(int EventId);
    }
}

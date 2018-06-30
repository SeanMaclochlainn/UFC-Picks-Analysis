﻿using FightData.Domain;
using FightData.Domain.Entities;
using FightDataProcessor.WikipediaParser;
using System;
using System.Collections.Generic;
using System.Text;

namespace FightDataProcessor
{
    public class CommandController
    {
        private AppCommand appCommand;
        private UfcEventCollector ufcEventCollector;
        private DataRemover dataRemover;

        public CommandController(AppCommand appCommand)
        {
            this.appCommand = appCommand;
            ufcEventCollector = new UfcEventCollector();
            dataRemover = new DataRemover();
        }

        public void HandleCommand()
        {
            DataUtilities dataUtilities = new DataUtilities();
            
            if (appCommand.Command == Commands.Collect)
            {
                ufcEventCollector.ContinuouslyCollectEvents();
            }
            else if(appCommand.Command == Commands.Process)
            {
                if(appCommand.Arguments.Contains(Arguments.ClearExisting))
                {
                    dataRemover.RemoveAllPicks();
                }

                CollectAllUfcEventsData();
            }
        }

        private void CollectAllUfcEventsData()
        {
            UfcEventsParser ufcEventsParser = new UfcEventsParser();
            ufcEventsParser.ParseAllEvents();
        }
    }
}

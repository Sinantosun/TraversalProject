﻿namespace TraversalProject.CQRS.Commands.DestinationCommands
{
    public class UpdateDestinationCommand
    {
        public int destinationID { get; set; }
        public string city { get; set; }
        public string daynight { get; set; }
        public double price { get; set; }
    }
}

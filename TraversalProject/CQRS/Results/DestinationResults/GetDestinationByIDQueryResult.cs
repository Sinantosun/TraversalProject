namespace TraversalProject.CQRS.Results.DestinationResults
{
    public class GetDestinationByIDQueryResult
    {
        public int destinationID { get; set; }
        public string city { get; set; }
        public string daynight { get; set; }
        public double price { get; set; }
    }
}

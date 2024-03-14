namespace TraversalProject.Dtos.BookHotelDtos
{
    public class ResultBookingHotelDto
    {
        public int count { get; set; }
        public Mappagefields mapPageFields { get; set; }
        public Result[] results { get; set; }

        public class Mappagefields
        {
            public Boundingbox boundingBox { get; set; }
            public Propertyannotation[] propertyAnnotations { get; set; }
        }

        public class Boundingbox
        {
            public float neLat { get; set; }
            public float neLon { get; set; }
            public float swLon { get; set; }
            public float swLat { get; set; }
        }

        public class Propertyannotation
        {
            public float latitude { get; set; }
            public float longitude { get; set; }
        }

        public class Result
        {
            public int id { get; set; }
            public string name { get; set; }
            public int mainPhotoId { get; set; }
            public string photoMainUrl { get; set; }
            public string[] photoUrls { get; set; }
            public int position { get; set; }
            public int rankingPosition { get; set; }
            public string countryCode { get; set; }
            public float latitude { get; set; }
            public float longitude { get; set; }
            public Pricebreakdown priceBreakdown { get; set; }
            public string currency { get; set; }
            public Checkin checkin { get; set; }
            public Checkout checkout { get; set; }
            public string checkoutDate { get; set; }
            public string checkinDate { get; set; }
            public float reviewScore { get; set; }
            public string reviewScoreWord { get; set; }
            public int reviewCount { get; set; }
            public int qualityClass { get; set; }
            public bool isFirstPage { get; set; }
            public int accuratePropertyClass { get; set; }
            public int propertyClass { get; set; }
            public int ufi { get; set; }
            public string wishlistName { get; set; }
            public int optOutFromGalleryChanges { get; set; }
            public Wishlisttoggle wishlistToggle { get; set; }
            public object propertyType { get; set; }
            public string[] proposedAccommodation { get; set; }
            public Pricedetails priceDetails { get; set; }
            public string[] additionalLabels { get; set; }
        }

        public class Pricebreakdown
        {
            public object[] benefitBadges { get; set; }
            public Grossprice grossPrice { get; set; }
            public Excludedprice excludedPrice { get; set; }
            public object[] taxExceptions { get; set; }
        }

        public class Grossprice
        {
            public string currency { get; set; }
            public float value { get; set; }
        }

        public class Excludedprice
        {
            public string currency { get; set; }
            public float value { get; set; }
        }

        public class Checkin
        {
            public string untilTime { get; set; }
            public string fromTime { get; set; }
        }

        public class Checkout
        {
            public string untilTime { get; set; }
            public string fromTime { get; set; }
        }

        public class Wishlisttoggle
        {
            public int propertyId { get; set; }
            public string wishlistName { get; set; }
            public string destinationId { get; set; }
        }

        public class Pricedetails
        {
            public string info { get; set; }
            public object strikethrough { get; set; }
            public string gross { get; set; }
            public string taxInfo { get; set; }
        }

    }
}

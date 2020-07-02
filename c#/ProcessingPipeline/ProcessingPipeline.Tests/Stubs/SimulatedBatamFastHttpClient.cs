using ProcessingPipeline.Tests.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace ProcessingPipeline.Tests.Stubs
{
    /// <summary>
    /// this class is spy which providers instance of httpClient to the real API for testing purposes. It
    /// has canned answers for all the questions, without hitting real API of our partner we can test-run the integration
    /// </summary>
    public class SimulatedBatamFastHttpClient
    {
        //this guy will be provided to create BatamFastApi which is SUT
        public HttpClient Client { get; set; }

        public Uri LastUrl { get; private set; }

        public int NumberOfHitsForSearch { get; set; }

        public string ReserveNumber { get; set; }

        public bool ConfirmContainsReserveNumber { get; set; }

        public bool ConfirmShouldFail { get; set; }

        public SimulatedBatamFastHttpClient()
        {
            ReserveNumber = Guid.NewGuid().ToString();

            Client = new HttpClient(new TestMessageHandler
            {
                Sender = async request =>
                {
                    this.LastUrl = request.RequestUri;

                    if (request.Method == HttpMethod.Post && request.RequestUri.ToString().Contains("token"))
                    {
                        return String("{ 'access_token':'eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJ1bmlxdWVfbmFtZSI6IkQwMDExIiwic3ViIjoiRDAwMTEiLCJyb2xlIjpbIkFnZW50IiwiVXNlciJdLCJpc3MiOiJodHRwOi8vbG9jYWxob3N0OjUzOTg2LyIsImF1ZCI6IkFueSIsImV4cCI6MTUyMDYwOTg5MSwibmJmIjoxNTIwNTAxODkxfQ.VP1-5nTkBOvvlAYFUUAFXgImTtUeTdpGjLtN601dm1k', 'token_type': 'bearer', 'expires_in': 107999}");
                    }

                    if (request.Method == HttpMethod.Post && request.RequestUri.ToString().Contains("ports"))
                    {
                        return String("[ { 'id': 'HFC', 'name': 'HarbourFront', 'countryId': 'SG', 'countryName': 'SINGAPORE' }, { 'id': 'TMF', 'name': 'TanahMerah', 'countryId': 'SG', 'countryName': 'SINGAPORE' }]");
                    }

                    if (request.Method == HttpMethod.Get && request.RequestUri.ToString().Contains("sectors"))
                    {
                        return String("[ { 'id': 'HFC-BTC', 'name': 'HarbourFront - Batam Center', 'portOrigin': { 'id': 'HFC', 'name': 'HarbourFront', 'countryId': 'SG', 'countryName': 'SINGAPORE' }, 'portDestination': { 'id': 'BTC', 'name': 'Batam Center', 'countryId': 'ID', 'countryName': 'INDONESIA' } }, { 'id': 'BTC-HFC', 'name': 'Batam Center - HarbourFront', 'portOrigin': { 'id': 'BTC', 'name': 'Batam Center', 'countryId': 'ID', 'countryName': 'INDONESIA' }, 'portDestination': { 'id': 'HFC', 'name': 'HarbourFront', 'countryId': 'SG', 'countryName': 'SINGAPORE' } }]");
                    }

                    if (request.Method == HttpMethod.Post && request.RequestUri.ToString().Contains("search"))
                    {
                        this.NumberOfHitsForSearch++;
                        return String("{	'departDate': '2018-03-12',	'departTimeRegion': 'SG',	'departPortOriginId': 'HFC',	'departPortOriginName': 'HarbourFront',	'departPortDestinationId': 'BTC',	'departPortDestinationName': 'Batam Center',	'isRoundTrip': true,	'isOpenReturnTrip': false, 'returnDate': '2018-03-13',	'returnTimeRegion': 'WIB',	'returnPortOriginID': 'SKP',	'returnPortOriginName': 'Sekupang',	'returnPortDestinationID': 'HFC',	'returnPortDestinationName': 'HarbourFront',	'cabinClass': 'ECONOMY',	'adultQty': 1,	'childQty': 0,	'infantQty': 0,	'departTrips': [{		'id': '180312HFCBTC1910',		'ticketFareTypeId': 'ER',		'isOpenTrip': false,		'portOriginId': 'HFC',		'portOriginName': 'HarbourFront',		'portDestinationId': 'BTC',		'portDestinationName': 'Batam Center',		'departDate': '2018-03-12',		'etd': '19:10',		'arrivalDate': '2018-03-12',		'eta': '20:20',		'timeRegion': 'SG',		'vesselID': 'JF2',		'vesselName': 'JET FLYTE 2',		'cabinClass': 'ECONOMY',		'currencyID': 'S$',		'adultFare': 9,		'childFare': 5.5,		'infantFare': 5.5, 'fee1Name': 'Surcharge',		'fee2Name': 'PDF - SG',		'fee3Name': 'Terminal Fee - BTM',		'fee1': 7,		'fee2': 7,		'fee3': 0,		'availableSeats': 166,		'capacitySeats': 166	}],	'returnTrips': [{		'id': '180313SKPHFC0600',		'ticketFareTypeId': 'ER',		'isOpenTrip': false,		'portOriginId': 'SKP',		'portOriginName': 'Sekupang',		'portDestinationId': 'HFC',		'portDestinationName': 'HarbourFront',		'departDate': '2018-03-13',		'etd': '06:00',		'arrivalDate': '2018-03-13',		'eta': '06:45',		'timeRegion': 'WIB',		'vesselID': 'JF1',		'vesselName': 'JET FLYTE 1',		'cabinClass': 'ECONOMY',		'currencyID': 'S$',		'adultFare': 11,		'childFare': 11.5,		'infantFare': 11.5,		'fee1Name': 'Surcharge',		'fee2Name': 'PDF - SG',		'fee3Name': 'Terminal Fee - BTM',		'fee1': 7,		'fee2': 0,		'fee3': 8,		'availableSeats': 166,		'capacitySeats': 166	}]}");
                    }

                    if (request.Method == HttpMethod.Post && request.RequestUri.ToString().Contains("reserve"))
                    {
                        return String($"{{'id': '{ReserveNumber}'}}");
                    }

                    if (request.Method == HttpMethod.Post && request.RequestUri.ToString().Contains("confirm"))
                    {
                        this.ConfirmContainsReserveNumber = (await request.Content.ReadAsStringAsync()).Contains(ReserveNumber);

                        return String("{'id': 1}");
                    }

                    return new HttpResponseMessage(HttpStatusCode.NotFound);
                },
            })
            { BaseAddress = new Uri("https://api.pingen.com/") };
        }

        private static HttpResponseMessage String(string result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new HttpResponseMessage(statusCode) { Content = new StringContent(result) };
        }

        private static HttpResponseMessage Json(object result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new HttpResponseMessage(statusCode) { Content = new StringContent(JsonConvert.SerializeObject(result)) };
        }
    }
}

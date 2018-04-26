using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using PCWCodeExamplesCSharpGeocoding.Models;

/*

	UK Geocoding with PHP
	Simple demo which passes postcode to the API on form submit and returns latitude and longitude.

	Full geocoding API documentation:-
	https://developers.alliescomputing.com/postcoder-web-api/geocoding/position
    
*/

namespace PCWCodeExamplesCSharpGeocoding.Controllers
{
    public class GeocodingLookupController : ApiController
    {
		[HttpGet]
		[Route("PCWCodeExamples/GeocodingLookup")]
		public string GeocodingLookup()
		{
			return "Pass a postcode by appending /NR147PZ";
		}

		[HttpGet]
		[Route("PCWCodeExamples/GeocodingLookup/{postcode}")]
		public async Task<GeocodingReturn> GeocodingLookup(string postcode)
		{
			// Replace with your API key, test key is locked to NR14 7PZ postcode search
			string apiKey = "PCW45-12345-12345-1234X";

			// Grab the input text and trim any whitespace
			postcode = postcode.Trim();

			// URL encode our input string
			postcode = HttpUtility.UrlEncode(postcode);

			// Create empty containers for our output
			List<GeocodingLookup> geocodingResp = new List<GeocodingLookup>();
			GeocodingReturn output = new GeocodingReturn();

			if (String.IsNullOrEmpty(postcode))
			{
				// Respond without calling API if no input supplied
				output.error_message = "No input supplied";
			}
			else
			{
				// Create the URL to API including API key and encoded postcode
				string geocodingUrl = $"https://ws.postcoder.com/pcw/{apiKey}/position/uk/{postcode}";

				// Create a disposable HTTP client
				using (HttpClient client = new HttpClient())
				{
					// Specify "application/json" in content-type header to request json return values
					client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
					
					// Execute our get request
					using (HttpResponseMessage resp = await client.GetAsync(geocodingUrl))
					{
						// Triggered if API does not return 200 HTTP code
						// More info - https://developers.alliescomputing.com/postcoder-web-api/error-handling

						// Here we will output a basic message with HTTP code
						if (!resp.IsSuccessStatusCode)
						{
							output.error_message = $"An error occurred - {resp.StatusCode.ToString()}";
						}
						else
						{
							// Store JSON response in our list of GeocodingLookup objects
							geocodingResp = JsonConvert.DeserializeObject<List<GeocodingLookup>>(await resp.Content.ReadAsStringAsync());

							if (geocodingResp.Count > 0)
							{
								// Store the results of our lookup in our return wrapper
								output.geocoding = geocodingResp;
							}
							else
							{
								// Postcode could not be located with suitable precision or at all
								output.error_message = "Postcode not found";
							}
						}
					}
				}
			}
			
			return output;
		}
    }
}
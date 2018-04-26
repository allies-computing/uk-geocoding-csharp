using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace PCWCodeExamplesCSharpGeocoding.Models
{
	/// <summary>
	/// For storing the results of a geocoding lookup
	/// </summary>
	[JsonObject]
	public class GeocodingLookup
    {
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public float? latitude { get; set; }
		
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public float? longitude { get; set; }
		
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int? grideasting { get; set; }
		
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public int? gridnorthing { get; set; }
    }

	/// <summary>
	/// A wrapper class for returning the results of a geocoding lookup
	/// </summary>
	[JsonObject]
	public class GeocodingReturn
	{
		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public string error_message { get; set; }

		[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
		public List<GeocodingLookup> geocoding { get; set; }
	}
}
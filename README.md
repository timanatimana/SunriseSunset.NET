# SunriseSunset.NET

This is a small ASP.NET Core 7 web API backend that fetches data from two external APIs. 
It uses API key authentication as a secure line between the API and clients.
Tests are written with (NSubstitute)[https://nsubstitute.github.io/help/getting-started/].


## External API  [positionstack](https://positionstack.com/)
Is used to get longitude and latitude coordinates for a given location name.

## External API [sunrisesunset.io](https://sunrisesunset.io/api/)
Is used to retrieve sunrise and sunset times for a specific longitude and latitude.

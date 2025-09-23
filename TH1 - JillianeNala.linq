<Query Kind="SQL">
  <Connection>
    <ID>d5528301-aed4-4b20-a0fc-3edd1b65a872</ID>
    <NamingServiceVersion>2</NamingServiceVersion>
    <Persist>true</Persist>
    <Server>RAESPC\SQLEXPRESS01</Server>
    <AllowDateOnlyTimeOnly>true</AllowDateOnlyTimeOnly>
    <Database>StartTed-2025-Sept</Database>
    <DriverData>
      <LegacyMFA>false</LegacyMFA>
    </DriverData>
  </Connection>
</Query>

// Q1 
// rentals w/ vacancies + only Oliver, Westmount, Forest Heights
Rentals
    .Where(x => x.Vacancies > 0 
             && (x.Community == "Oliver" 
              || x.Community == "Westmount" 
              || x.Community == "Forest Heights"))
	
	.Select(x => new
    {
        x.RentalID,
        x.MonthlyRent,
        x.Vacancies,
        x.Community,
        // if no rental type, mark as U/K
        Description = x.RentalType == null ? "U/K" : x.RentalType.Description
    })
	
	  .OrderBy(x => x.Community)   // community A-Z
    .ThenByDescending(x => x.MonthlyRent) // rent high -> low
    .Dump();
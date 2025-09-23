<Query Kind="Statements">
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

// Q1 – rentals with vacancies in Oliver, Westmount, or Forest Heights
var results =
    Rentals
        .Select(x => new
        {
            x.RentalID,
            x.MonthlyRent,
            Vacancies = x.MaxTenants - x.Renters.Count(),
            Community = x.Address.Community,
            Description = x.RentalType?.Description ?? "U/K"
        })
	
.Where(r => r.Vacancies > 0 &&
                   (r.Community == "Oliver" ||
                    r.Community == "Westmount" ||
                    r.Community == "Forest Heights"))
        .OrderBy(r => r.Community)            // A–Z by community
        .ThenByDescending(r => r.MonthlyRent) // within community, higher rent first
        .ToList();

results.Dump();  // sanity-check filter + ordering
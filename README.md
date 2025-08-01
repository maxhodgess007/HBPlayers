If you get exceptions when you try and make a booking this should fix it. Regards, Max.

++++++++++++++++++++

Why it happens... When you clone/fork a project, the *.db file is not included in Git (ignored by .gitignore).

EF Core migrations that create tables aren’t automatically applied after cloning.

How to fix it... 

1. Open Visual Studio

2. Open Package Manager Console in Visual Studio

	Tools > NuGet Package Manager > Package Manager Console

2. Run these commands in Powershell

	Add-Migration InitialCreate   # only if Migrations folder is empty

	Update-Database

This will create the SQLite database and build the Bookings table.
